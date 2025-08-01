// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: NetworkWindow.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows.RegistryEditor.Views;
/// <summary>
/// </summary>
public partial class NetworkWindow : Form
{

    /// <summary>
    /// </summary>
    private Process proc;






    /// <summary>
    /// </summary>
    public NetworkWindow()
    {
        InitializeComponent();
    }






    /// <summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnSelect_Click(object sender, EventArgs e)
    {
        string machineName = lbxMachines.SelectedItem.ToString();
        OnMachineSelected(machineName.Replace(@"\\", string.Empty));
        Close();
    }






    /// <summary>
    /// </summary>
    /// <param name="proc"></param>
    private void Execute(Process proc)
    {
        _ = proc.Start();
        proc.BeginErrorReadLine();
        proc.BeginOutputReadLine();
        proc.WaitForExit();
    }






    /// <summary>
    /// </summary>
    public event Action<string> MachineSelected;






    /// <summary>
    ///     Handles the <see cref="Form.FormClosing" /> event for the NetworkWindow. Prevents the form from closing
    ///     immediately if a process is running, ensuring proper cleanup.
    /// </summary>
    /// <remarks>
    ///     If a process is active, the method cancels the closing operation, performs cleanup tasks such
    ///     as stopping error and output reading, killing the process, and disposing of resources. After cleanup, the form
    ///     is closed.
    /// </remarks>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs" /> that contains the event data.</param>
    private async void NetworkWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (proc != null)
        {
            e.Cancel = true;
            proc.CancelErrorRead();
            proc.CancelOutputRead();
            await Task.Run(() => Thread.Sleep(100));
            proc.Kill();
            proc.Dispose();
            proc = null;
            await Task.Run(() => Thread.Sleep(500));

            Close();
        }
    }






    private async void NetworkWindow_Load(object sender, EventArgs e)
    {
        lbxMachines.Items.Clear();

        loader.Show();
        ProcessStartInfo procInfo = new()
        {
            FileName = "NET",
            Arguments = "VIEW",
            RedirectStandardError = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            UseShellExecute = false
        };

        proc = new Process
        {
            StartInfo = procInfo,
            EnableRaisingEvents = true
        };

        proc.ErrorDataReceived += OnErrorDataReceived;
        proc.OutputDataReceived += OnOutputDataReceived;
        proc.Exited += OnFindEnd;

        await Task.Run(() => Execute(proc));
    }






    private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        Console.WriteLine("ERROR occured while searching for network devices -> " + e.Data);
        loader.Hide();
    }






    private void OnFindEnd(object sender, EventArgs e)
    {
        Console.WriteLine("Finished searching for network devices.");
        loader.Hide();

        proc = null;
    }






    /// <summary>
    ///     Invoked when a machine is selected, triggering the <see cref="MachineSelected" /> event.
    /// </summary>
    /// <remarks>
    ///     This method is designed to be overridden in derived classes to provide custom behavior  when
    ///     a machine is selected. Ensure that <paramref name="e" /> is not null before invoking  this method to avoid
    ///     runtime exceptions.
    /// </remarks>
    /// <param name="e">The identifier or details of the selected machine. Cannot be null.</param>
    protected virtual void OnMachineSelected(string e)
    {
        MachineSelected?.Invoke(e);
    }






    private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (e.Data == null)
        {
            return;
        }

        if (!e.Data.StartsWith(@"\\"))
        {
            return;
        }

        _ = lbxMachines.InvokeSafe(() => lbxMachines.Items.Add(e.Data.Trim()));
    }

}