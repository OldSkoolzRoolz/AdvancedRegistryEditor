// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: ETWMonitor.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




namespace Windows.RegistryEditor.Events;


using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;



/// <summary>
///     Monitors Windows registry events using ETW (Event Tracing for Windows).
///     Provides filtering, logging, and event notification for registry changes.
/// </summary>
internal class RegistryEtwMonitor : IDisposable
{

    private readonly object syncRoot = new();
    private CancellationTokenSource cts;
    private Task monitorTask;

    /// <summary>
    ///     Gets or sets the ETW session name.
    /// </summary>
    public string SessionName { get; set; } = "RegistryMonitorSession";

    /// <summary>
    ///     Gets or sets the path to the log file for registry events.
    /// </summary>
    public string LogFilePath { get; set; }

    /// <summary>
    ///     Gets or sets the process name filter for registry events.
    /// </summary>
    public HashSet<string> ProcessNameFilter { get; set; }

    /// <summary>
    ///     Gets or sets the user filter for registry events.
    /// </summary>
    public HashSet<string> UserFilter { get; set; }

    /// <summary>
    ///     Gets or sets the registry key name filter for registry events.
    /// </summary>
    public HashSet<string> KeyNameFilter { get; set; }

    /// <summary>
    ///     Gets or sets the event type filter for registry events.
    /// </summary>
    public HashSet<string> EventTypeFilter { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether monitoring runs asynchronously.
    /// </summary>
    public bool RunAsync { get; set; } = true;






    /// <summary>
    ///     Disposes the monitor and stops monitoring.
    /// </summary>
    public void Dispose()
    {
        StopMonitoring();
        cts?.Dispose();
        Session?.Dispose();
    }






    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);






    /// <summary>
    ///     Gets the user name for a process by its PID.
    /// </summary>
    /// <param name="pid">The process ID.</param>
    /// <returns>The user name or "N/A" if unavailable.</returns>
    private static string GetProcessUser(int pid)
    {
        const int PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;
        const uint TOKEN_QUERY = 0x0008;
        var hProc = IntPtr.Zero;
        var hToken = IntPtr.Zero;

        try
        {
            hProc = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, pid);

            if (hProc == IntPtr.Zero) return "N/A";

            if (!OpenProcessToken(hProc, TOKEN_QUERY, out hToken)) return "N/A";

            using WindowsIdentity wi = new(hToken);

            return wi.Name;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error getting process user for PID {pid}: {ex.Message}");

            return "N/A";
        }
        finally
        {
            if (hToken != IntPtr.Zero) _ = CloseHandle(hToken);

            if (hProc != IntPtr.Zero) _ = CloseHandle(hProc);
        }
    }






    /// <summary>
    ///     Determines whether the current process is running with elevated (administrator) privileges.
    /// </summary>
    /// <returns>True if elevated; otherwise, false.</returns>
    private static bool IsElevated()
    {
        using var identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);

        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }






    /// <summary>
    ///     Gets a value indicating whether registry monitoring is active.
    /// </summary>
    public static bool IsMonitoring { get; private set; }






    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);






    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);






    /// <summary>
    ///     Gets the current ETW trace event session.
    /// </summary>
    public static TraceEventSession Session { get; private set; }






    /// <summary>
    ///     Handles a registry event, applies filters, logs, and raises the <see cref="RegistryEvent" />.
    /// </summary>
    /// <param name="eventName">The registry event name.</param>
    /// <param name="data">The registry trace data.</param>
    private void HandleRegistryEvent(string eventName, RegistryTraceData data)
    {
        try
        {
            var pid = data.ProcessID;
            var processName = data.ProcessName;
            var keyName = data.KeyName;
            var userName = GetProcessUser(pid);

            // Filtering
            if (ProcessNameFilter != null && !ProcessNameFilter.Contains(processName)) return;

            if (UserFilter != null && !UserFilter.Contains(userName)) return;

            if (KeyNameFilter != null && !KeyNameFilter.Contains(keyName)) return;

            if (EventTypeFilter != null && !EventTypeFilter.Contains(eventName)) return;

            RegistryEventArgs args = new(eventName, processName, pid, keyName, userName, DateTime.Now);

            RegistryEvent?.Invoke(this, args);

            var logLine = $"{args.Timestamp:O} {eventName} → {keyName}\n    By: {processName} (PID {pid}), User: {userName}";
            Console.WriteLine(logLine);

            if (!string.IsNullOrEmpty(LogFilePath))
                try
                {
                    File.AppendAllText(LogFilePath, logLine + Environment.NewLine);
                }
                catch (Exception logEx)
                {
                    Console.Error.WriteLine($"Error writing to log file: {logEx.Message}");
                }
        }
        catch (Exception ex)
        {
            MonitoringError?.Invoke(this, ex);
            Console.Error.WriteLine($"Error handling registry event '{eventName}': {ex.Message}");
        }
    }






    /// <summary>
    ///     Monitors registry events using ETW, applies filters, and raises events.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    private void Monitor(CancellationToken token)
    {
        try
        {
            if (!IsElevated()) throw new InvalidOperationException("Administrator privileges are required for ETW registry monitoring.");

            using (Session = new TraceEventSession(SessionName))
            {
                Console.CancelKeyPress += (_, __) => Session.Stop();
                _ = Session.EnableKernelProvider(KernelTraceEventParser.Keywords.Registry);
                RegisterRegistryEventHandlers(Session);
                IsMonitoring = true;
                MonitoringStarted?.Invoke(this, EventArgs.Empty);
                Console.WriteLine("Listening for registry events. Press Ctrl+C to exit.");
                _ = Session.Source.Process();
            }
        }
        catch (Exception ex)
        {
            MonitoringError?.Invoke(this, ex);
            Console.Error.WriteLine($"Error starting ETW monitoring: {ex.Message}");
        }
        finally
        {
            IsMonitoring = false;
            MonitoringStopped?.Invoke(this, EventArgs.Empty);
        }
    }






    /// <summary>
    ///     Occurs when an error happens during monitoring.
    /// </summary>
    public event EventHandler<Exception> MonitoringError;

    /// <summary>
    ///     Occurs when monitoring is started.
    /// </summary>
    public event EventHandler MonitoringStarted;

    /// <summary>
    ///     Occurs when monitoring is stopped.
    /// </summary>
    public event EventHandler MonitoringStopped;






    /// <summary>
    ///     Registers handlers for registry ETW events.
    /// </summary>
    /// <param name="session">The ETW trace event session.</param>
    private void RegisterRegistryEventHandlers(TraceEventSession session)
    {
        var kernelSource = session.Source.Kernel;
        kernelSource.RegistryCreate += data => HandleRegistryEvent("CreateKey", data);
        kernelSource.RegistryDelete += data => HandleRegistryEvent("DeleteKey", data);
        kernelSource.RegistrySetValue += data => HandleRegistryEvent("SetValue", data);
        kernelSource.RegistryDeleteValue += data => HandleRegistryEvent("DeleteValue", data);
    }






    /// <summary>
    ///     Occurs when a registry event is detected.
    /// </summary>
    public event EventHandler<RegistryEventArgs> RegistryEvent;






    /// <summary>
    ///     Starts monitoring registry events.
    /// </summary>
    public void StartMonitoring()
    {
        lock (syncRoot)
        {
            if (IsMonitoring)
            {
                Console.WriteLine("Already monitoring registry events.");

                return;
            }

            cts = new CancellationTokenSource();
            if (RunAsync)
                monitorTask = Task.Run(() => Monitor(cts.Token), cts.Token);
            else
                Monitor(cts.Token);
        }
    }






    /// <summary>
    ///     Stops monitoring registry events.
    /// </summary>
    public void StopMonitoring()
    {
        lock (syncRoot)
        {
            if (!IsMonitoring) return;

            cts?.Cancel();
            _ = Session?.Stop();
            IsMonitoring = false;
            MonitoringStopped?.Invoke(this, EventArgs.Empty);
            Console.WriteLine("Stopped monitoring registry events.");
        }
    }

}


/// <summary>
///     Provides data for a registry event detected by <see cref="RegistryEtwMonitor" />.
/// </summary>
public class RegistryEventArgs : EventArgs
{

    /// <summary>
    ///     Initializes a new instance of the <see cref="RegistryEventArgs" /> class.
    /// </summary>
    /// <param name="eventName">The registry event name.</param>
    /// <param name="processName">The name of the process that triggered the event.</param>
    /// <param name="processId">The process ID.</param>
    /// <param name="keyName">The registry key name.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="timestamp">The timestamp of the event.</param>
    public RegistryEventArgs(string eventName, string processName, int processId, string keyName, string userName, DateTime timestamp)
    {
        EventName = eventName;
        ProcessName = processName;
        ProcessId = processId;
        KeyName = keyName;
        UserName = userName;
        Timestamp = timestamp;
    }






    /// <summary>
    ///     Gets the registry event name.
    /// </summary>
    public string EventName { get; }

    /// <summary>
    ///     Gets the name of the process that triggered the event.
    /// </summary>
    public string ProcessName { get; }

    /// <summary>
    ///     Gets the process ID.
    /// </summary>
    public int ProcessId { get; }

    /// <summary>
    ///     Gets the registry key name.
    /// </summary>
    public string KeyName { get; }

    /// <summary>
    ///     Gets the user name.
    /// </summary>
    public string UserName { get; }

    /// <summary>
    ///     Gets the timestamp of the event.
    /// </summary>
    public DateTime Timestamp { get; }

}