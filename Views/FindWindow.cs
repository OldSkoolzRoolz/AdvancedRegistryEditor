// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: FindWindow.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




namespace Windows.RegistryEditor.Views;


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Events;

using Utils;

using Stopwatch = Utils.Stopwatch;



/// <summary>
///     https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/reg-query
/// </summary>
public partial class FindWindow : Form
{

    private CancellationTokenSource cancelSrc;
    private CancellationToken cancelToken;
    private int procsExecuted;

    private List<Process> procsList;

    private AutoResetEvent procToken;
    private NetworkWindow refNetworkWindow;
    private bool searchInProgress;






    /// <summary>
    ///     Initializes a new instance of the <see cref="FindWindow" /> class.
    /// </summary>
    public FindWindow()
    {
        InitializeComponent();
    }






    /// <summary>
    ///     Gets the instance of <see cref="NetworkWindow" /> used for remote machine selection.
    /// </summary>
    public NetworkWindow NetworkInstance
    {
        get
        {
            if (refNetworkWindow != null && !refNetworkWindow.IsDisposed)
            {
                refNetworkWindow.Activate();

                return refNetworkWindow;
            }

            refNetworkWindow = new NetworkWindow();
            refNetworkWindow.MachineSelected += e => tbxRemoteComputer.Text = e;
            refNetworkWindow.FormClosing += delegate { tbxSearch.Focus(); };

            return refNetworkWindow;
        }
    }






    /// <summary>
    ///     Handles the Cancel button click event to stop the ongoing search operation.
    /// </summary>
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (!searchInProgress) return;

        cancelSrc.Cancel();

        for (var i = 0; i < procsExecuted; i++)
        {
            procsList[i].CancelErrorRead();
            procsList[i].CancelOutputRead();
            if (!procsList[i].HasExited) procsList[i].Kill();
        }

        _ = procToken.Set();
    }






    /// <summary>
    ///     Handles the Find All button click event to start searching the registry based on selected filters.
    /// </summary>
    private async void BtnFindAll_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(tbxSearch.Text)) return;

        if (cbxRemoteSearch.Checked)
        {
            var answer = MessageBox.Show("Scanning registries through the network may take a while depending on the" + "chosen filter. Are you sure you want to continue?", "INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            if (answer != DialogResult.Yes) return;
        }

        Started();
        var hives = GetHkeysFilters();
        procsList = GenerateRegQueryProcs(hives);

        Stopwatch stopwatch = new();
        stopwatch.Start();
        OnProgressStarted(this, new ProgressStartedArgs("Starting to search for registries...", hives.First()));

        searchInProgress = true;
        await ExecuteProcs(procsList);
        searchInProgress = false;
        _ = procToken.Set();

        stopwatch.Stop();
        OnProgressFinished(this, new ProgressFinishedArgs("Finished searching for registries."));
        Stopped();

        Activate();
        MessageBox.Show("Finished searching. Time Taken:\n" + stopwatch.Elapsed.ToString("g"), "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }






    /// <summary>
    ///     Handles the Find Available Machines button click event to show the network machine selection dialog.
    /// </summary>
    private void BtnFindAvailableMachines_Click(object sender, EventArgs e)
    {
        _ = NetworkInstance.ShowDialog(this);
    }






    /// <summary>
    ///     Handles the Find Single button click event to search for a single registry entry.
    /// </summary>
    private void BtnFindSingle_Click(object sender, EventArgs e)
    {
    }






    /// <summary>
    ///     Builds the registry search query string based on the selected hive and filters.
    /// </summary>
    /// <param name="hive">The registry hive to search.</param>
    /// <returns>The constructed search query string.</returns>
    private string BuildSearchQuery(string hive)
    {
        var query = cbxRemoteSearch.Checked ? $"\"\\\\{tbxRemoteComputer.Text}\\{hive}\" /f \"{tbxSearch.Text}\" /s" : $"\"{hive}\" /f \"{tbxSearch.Text}\" /s";
        if (cbxKeys.Checked) query += " /k";

        if (cbxValues.Checked) query += " /v";

        if (cbxData.Checked) query += " /d";

        if (cbxCaseSensitive.Checked) query += " /c";

        if (cbxMatchString.Checked) query += " /e";

        if (cbxsDataTypes.GetItemCheckedByName("REG_MULTI_SZ")) query += " /se +";

        if (cbxsDataTypes.CheckedItems.Count <= 0 || cbxsDataTypes.SelectAllChecked) return query;

        // ------------------------ Adding Data types filters ------------------------
        query += " /t \"";
        foreach (string checkedItem in cbxsDataTypes.CheckedItems) query += checkedItem + ",";

        query = query.TrimEnd(',');
        query += "\"";

        // https://msdn.microsoft.com/en-us/library/windows/desktop/bb773476(v=vs.85).aspx
        // Unfortunately these are not supported by reg query when filtering...
        query = Regex.Replace(query, ",?REG_QWORD_LITTLE_ENDIAN", string.Empty);
        query = Regex.Replace(query, ",?REG_LINK", string.Empty);
        query = Regex.Replace(query, ",?REG_RESOURCE_LIST", string.Empty);

        return query;
    }






    /// <summary>
    ///     Handles the CheckedChanged event for remote search checkbox, enabling/disabling related controls.
    /// </summary>
    private void CbxSearchInRemote_CheckedChanged(object sender, EventArgs e)
    {
        tbxRemoteComputer.Enabled = cbxRemoteSearch.Checked;
        btnFindAvailableMachines.Enabled = cbxRemoteSearch.Checked;
        gbxHkeys.Enabled = !cbxRemoteSearch.Checked;
        _ = cbxRemoteSearch.Checked ? tbxRemoteComputer.Focus() : tbxSearch.Focus();
    }






    /// <summary>
    ///     Creates a <see cref="Process" /> configured to execute a registry query with the specified search query.
    /// </summary>
    /// <param name="searchQuery">The search query string.</param>
    /// <returns>The configured <see cref="Process" /> instance.</returns>
    private Process CreateRegQueryProc(string searchQuery)
    {
        ProcessStartInfo procInfo = new()
        {
                    FileName = "REG",
                    Arguments = $"QUERY {searchQuery}",
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
        };

        Process proc = new()
        {
                    StartInfo = procInfo,
                    EnableRaisingEvents = true
        };

        proc.ErrorDataReceived += OnErrorDataReceived;
        proc.OutputDataReceived += OnOutputDataReceived;
        proc.Exited += OnFindEnd;

        return proc;
    }






    /// <summary>
    ///     Executes the specified registry query process and waits for its completion.
    /// </summary>
    /// <param name="proc">The process to execute.</param>
    private void ExecuteProc(Process proc)
    {
        _ = proc.Start();
        proc.BeginErrorReadLine();
        proc.BeginOutputReadLine();

        Console.WriteLine("[Process::ExecuteProc] - Waiting for exit...");
        proc.WaitForExit();
        Console.WriteLine("[Process::ExecuteProc] - Executed Finished.");
    }






    /// <summary>
    ///     Asynchronously executes a collection of registry query processes.
    /// </summary>
    /// <param name="procs">The processes to execute.</param>
    private async Task ExecuteProcs(IEnumerable<Process> procs)
    {
        foreach (var proc in procs)
        {
            procsExecuted++;

            var hiveShort = proc.StartInfo.Arguments.Split('"')[1];
            var hive = cbxRemoteSearch.Checked ? RegistryUtils.GetRemoteHiveFullName(hiveShort) : RegistryUtils.GetHiveFullName(hiveShort);
            var message = $"Searching in {hive}...Patient :)";
            Text = $"Find | {message}";

            OnProgressChanged(this, new ProgressChangedArgs(message, hive));
            await Task.Run(() => ExecuteProc(proc));

            if (cancelToken.IsCancellationRequested) break;
        }
    }






    /// <summary>
    ///     Handles the FormClosing event, ensuring search operations are properly cancelled before closing.
    /// </summary>
    private async void FindWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (searchInProgress)
        {
            e.Cancel = true;
            btnCancel.PerformClick();
            await Task.Run(() => Thread.Sleep(500));
            Close();
        }
    }






    /// <summary>
    ///     Handles the KeyPress event for the form, providing keyboard shortcuts for closing and searching.
    /// </summary>
    private void FindWindow_KeyPress(object sender, KeyPressEventArgs e)
    {
        var key = (Keys)e.KeyChar;

        switch (key)
        {
            case Keys.Escape:

                Close();

                break;

            case Keys.Return:

                btnFindAll.PerformClick();

                break;
        }
    }






    /// <summary>
    ///     Handles the Load event for the form, initializing default UI state.
    /// </summary>
    private void FindWindow_Load(object sender, EventArgs e)
    {
        cbxsHkeys.SetItemChecked(1, true);
    }






    /// <summary>
    ///     Occurs when multiple registry search results are found.
    /// </summary>
    public event Action<object, FindResultsArgs> FoundResults;

    /// <summary>
    ///     Occurs when a single registry search result is found.
    /// </summary>
    public event Action<object, FindSingleResultArgs> FoundSingleResult;






    /// <summary>
    ///     Generates a list of registry query processes for the specified hives.
    /// </summary>
    /// <param name="hives">The registry hives to search.</param>
    /// <returns>A list of configured <see cref="Process" /> instances.</returns>
    private List<Process> GenerateRegQueryProcs(List<string> hives)
    {
        List<Process> procs = [];

        foreach (var hive in hives)
        {
            var searchQuery = BuildSearchQuery(RegistryUtils.GetHiveShortcut(hive));
            var proc = CreateRegQueryProc(searchQuery);
            procs.Add(proc);
            Console.WriteLine($"{proc.StartInfo.FileName} {proc.StartInfo.Arguments}");
        }

        return procs;
    }






    /// <summary>
    ///     BIG NOTE: When we search through the registry remotely, we can't really access HKCR, HKCU, HKCC but at the same
    ///     time
    ///     we don't really need to, as they can be found under HKLM and HKU. In other words, they're just symbolic links. i.e:
    ///     ### HKEY_CLASSES_ROOT links to: ###
    ///     HKEY_CURRENT_USER\Software\Classes
    ///     HKEY_LOCAL_MACHINE\SOFTWARE\Classes
    ///     HKEY_USERS\{UserSID}\Software\Classes
    ///     HKEY_USERS\{UserSID}_Classes
    ///     ### HKEY_CURRENT_USER links to: ###
    ///     HKEY_USERS\{UserSID}
    ///     ### HKEY_CURRENT_CONFIG links to: ###
    ///     HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Hardware Profiles\0001
    ///     HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Hardware Profiles\Current
    ///     HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Hardware Profiles\0001
    ///     HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Hardware Profiles\Current
    ///     Getting user SID:
    ///     WindowsIdentity.GetCurrent().User
    ///     whoami /user
    /// </summary>
    private List<string> GetHkeysFilters()
    {
        List<string> hives = [];

        if (cbxRemoteSearch.Checked)
        {
            hives.Add(RegistryUtils.HKLM);
            hives.Add(RegistryUtils.HKU);

            #region experimental

            //string userSID = WindowsIdentity.GetCurrent().User?.Value;
            //Debug.Assert(userSID != null, "userSID != null");
            //foreach (string hive in cbxsHkeys.CheckedItems)
            //{
            //    if (hive.Equals(RegistryUtils.HKCR))
            //        hives.Add($@"HKU\{userSID}\Software\Classes");
            //    else if (hive.Equals(RegistryUtils.HKCU))
            //        hives.Add($@"HKU\{userSID}");
            //    else if (hive.Equals(RegistryUtils.HKLM))
            //        hives.Add($@"HKLM");
            //    else if (hive.Equals(RegistryUtils.HKU))
            //        hives.Add($@"HKU");
            //    else if (hive.Equals(RegistryUtils.HKCC))
            //        hives.Add($@"HKLM\SYSTEM\CurrentControlSet\Hardware Profiles\Current");
            //}

            #endregion experimental

        }
        else
        {
            foreach (string hive in cbxsHkeys.CheckedItems) hives.Add(hive);
        }

        return hives;
    }






    /// <summary>
    ///     Handles error data received from a registry query process.
    /// </summary>
    private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (e?.Data == null) return;

        if (!e.Data.StartsWith("ERROR")) return;

        if (e.Data.Trim().StartsWith("ERROR: The network path was not found."))
        {
            _ = MessageBox.Show($"{e.Data}\n" + "Make sure that the target machine has \"Remote Registry\" service running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            cancelSrc.Cancel();
        }
        else
        {
            _ = MessageBox.Show("Error occured. Please contact author of this app with this message.\n" + $"{e.Data}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }






    /// <summary>
    ///     Handles the end of a registry query process execution.
    /// </summary>
    private async void OnFindEnd(object sender, EventArgs e)
    {
        if (procsList.Count != procsExecuted || procsExecuted == 0) return;

        await Task.Run(() => Thread.Sleep(1000));
        Console.WriteLine("[Process::OnFindEnd] - Waiting for other thread to finished.");
        _ = procToken.WaitOne();
        Console.WriteLine("[Process::OnFindEnd] - Clearing up.");

        for (var i = 0; i < procsList.Count; i++)
        {
            procsList[i].Dispose();
            procsList[i] = null;
        }

        searchInProgress = false;
        loader.Hide();
    }






    /// <summary>
    ///     Raises the <see cref="FoundResults" /> event with the specified results.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments containing the results.</param>
    protected virtual void OnFoundResults(object sender, FindResultsArgs e)
    {
        FoundResults?.Invoke(sender, e);
    }






    /// <summary>
    ///     Raises the <see cref="FoundSingleResult" /> event with the specified result.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments containing the single result.</param>
    protected virtual void OnFoundSingleResult(object sender, FindSingleResultArgs e)
    {
        FoundSingleResult?.Invoke(sender, e);
    }






    /// <summary>
    ///     Handles output data received from a registry query process and raises result events.
    /// </summary>
    private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (e?.Data == null) return;

        var hive = e.Data.TrimStart().StartsWith("HKEY_") ? e.Data : string.Empty;

        if (string.IsNullOrEmpty(hive)) return;

        if (cbxRemoteSearch.Checked) hive = $"{tbxRemoteComputer.Text}\\{hive}";

        OnFoundSingleResult(this, new FindSingleResultArgs(hive));
    }






    /// <summary>
    ///     Raises the <see cref="ProgressChanged" /> event with the specified progress information.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments containing progress information.</param>
    protected virtual void OnProgressChanged(object sender, ProgressChangedArgs e)
    {
        ProgressChanged?.Invoke(sender, e);
    }






    /// <summary>
    ///     Raises the <see cref="ProgressFinished" /> event when the search operation is finished.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments containing progress information.</param>
    protected virtual void OnProgressFinished(object sender, ProgressFinishedArgs e)
    {
        ProgressFinished?.Invoke(sender, e);
    }






    /// <summary>
    ///     Raises the <see cref="ProgressStarted" /> event when the search operation is started.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments containing progress information.</param>
    protected virtual void OnProgressStarted(object sender, ProgressStartedArgs e)
    {
        ProgressStarted?.Invoke(sender, e);
    }






    /// <summary>
    ///     Occurs when the progress of the search operation changes.
    /// </summary>
    public event Action<object, ProgressChangedArgs> ProgressChanged;

    /// <summary>
    ///     Occurs when the search operation is finished.
    /// </summary>
    public event Action<object, ProgressFinishedArgs> ProgressFinished;

    /// <summary>
    ///     Occurs when the search operation is started.
    /// </summary>
    public event Action<object, ProgressStartedArgs> ProgressStarted;






    /// <summary>
    ///     Prepares the UI and internal state for starting a search operation.
    /// </summary>
    private void Started()
    {
        btnFindAll.Enabled = false;
        btnCancel.Enabled = true;
        loader.Show();

        procsExecuted = 0;
        procToken = new AutoResetEvent(false);
        cancelSrc = new CancellationTokenSource();
        cancelToken = cancelSrc.Token;
    }






    /// <summary>
    ///     Restores the UI and internal state after a search operation is stopped.
    /// </summary>
    private void Stopped()
    {
        Text = "Find";
        btnFindAll.Enabled = true;
        btnCancel.Enabled = false;
        loader.Hide();
    }






    //using LogQuery = Interop.MSUtil.LogQueryClass;
    //using RegistryInputFormat = Interop.MSUtil.COMRegistryInputContextClass;
    //using RegRecordSet = Interop.MSUtil.ILogRecordset;

    //private void BtnFindSingle_Click(object sender, EventArgs e)
    //{
    //    matches = new List<string>();
    //    RegRecordSet rs = null;
    //    try
    //    {
    //        LogQuery qry = new LogQuery();
    //        RegistryInputFormat registryFormat = new RegistryInputFormat();
    //        string query = $@"SELECT Path FROM HKLM WHERE Value='{tbxSearch.Text}'";
    //        rs = qry.Execute(query, registryFormat);
    //        for (; !rs.atEnd(); rs.moveNext())
    //        {
    //            matches.Add(rs.getRecord().toNativeString(""));
    //        }
    //    }
    //    finally
    //    {
    //        rs.close();
    //    }

    //    ((MainWindow)Owner).OnFoundResults(this, new FindResultsArgs(matches));
    //    Close();
    //}

}