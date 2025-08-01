// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: MainWindow.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;

using static System.Environment;



namespace Windows.RegistryEditor.Views;
/// <summary>
///     Represents the main window of the Registry Editor application, providing UI and logic for registry operations.
/// </summary>
public partial class MainWindow : Form
{

    private FindWindow refFindWindow;






    /// <summary>
    ///     Initializes a new instance of the <see cref="MainWindow" /> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }






    /// <summary>
    ///     Gets the instance of <see cref="FindWindow" /> used for registry search operations.
    /// </summary>
    public FindWindow FindInstance
    {
        get
        {
            if (refFindWindow != null && !refFindWindow.IsDisposed)
            {
                refFindWindow.Activate();

                return refFindWindow;
            }

            refFindWindow = new FindWindow();
            refFindWindow.FoundSingleResult += SingleResultFound;
            refFindWindow.ProgressStarted += ProgressStarted;
            refFindWindow.ProgressChanged += ProgressChanged;
            refFindWindow.ProgressFinished += ProgressFinished;
            refFindWindow.FormClosed += delegate { Activate(); };

            return refFindWindow;
        }
    }

    /// <summary>
    ///     Gets the creation parameters for the form, enabling double-buffering for smoother rendering.
    /// </summary>
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED

            return cp;
        }
    }






    /// <summary>
    ///     Adds an item to the results ListView with specified text, checked state, and visibility.
    /// </summary>
    /// <param name="text">The text to display in the item.</param>
    /// <param name="isChecked">Whether the item should be checked.</param>
    /// <param name="isVisible">Whether the item should be made visible.</param>
    private void AddItem(string text, bool isChecked, bool isVisible)
    {
        if (lvwResults == null || lblResultsCount == null)
        {
            return;
        }

        ListViewItem item = new();
        _ = item.SubItems.Add(text);
        item.Checked = isChecked;
        _ = lvwResults.Items.Add(item);
        int count = lvwResults.Items.Count;
        lblResultsCount.Text = $"Results Count: {count}";
        if (isVisible)
        {
            lvwResults.EnsureVisible(count - 1);
        }
    }






    /// <summary>
    ///     Handles the click event for adding a file watcher.
    /// </summary>
    private void addToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddFileWatcher filewatch = new();
        _ = filewatch.ShowDialog(this);
    }






    /// <summary>
    ///     Handles the click event for deleting checked registry hives.
    /// </summary>
    private void BtnDelete_Click(object sender, EventArgs e)
    {
        if (lvwResults == null)
        {
            return;
        }

        System.Collections.Generic.List<string> hives = lvwResults.GetAllCheckedSubItemsTextList(1);

        if (hives == null || hives.Count < 1)
        {
            return;
        }

        if (!hives.First().StartsWith("HKEY_"))
        {
            _ = MessageBox.Show("Deleting or Exporting registry keys on remote machines not implemented.", "NOT IMPLEMENTED", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }

        DialogResult result = MessageBox.Show("All registries that are checked will be removed from registry including sub keys.\n" + "Are you sure you want to continue?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

        if (result != DialogResult.Yes)
        {
            return;
        }

        string dstDir = Path.Combine(GetFolderPath(SpecialFolder.Desktop), "Registry_Backup", $"{DateTime.Now:yy-MM-dd_hh-mm}");

        foreach (string hive in hives)
        {
            try
            {
                RegistryUtils.ExportHive(hive, dstDir);
                RegistryUtils.DeleteHive(hive);
                Console.WriteLine($"Registry Deleted: {hive}");
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Error deleting registry hive: {hive}\n{ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        _ = MessageBox.Show("Successfully removed checked registries.\n" + "Created emergency backup of the removed registries on your desktop.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);

        try
        {
            Utility.OpenFolderInExplorer(dstDir);
        }
        catch
        {
            /* ignore */
        }
    }






    /// <summary>
    ///     Handles the CheckedChanged event for the "Select All" checkbox, checking or unchecking all items.
    /// </summary>
    private void CbxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (lvwResults == null || cbxSelectAll == null)
        {
            return;
        }

        lvwResults.CheckAllItems(cbxSelectAll.Checked);
    }






    /// <summary>
    ///     Handles the click event for opening the Find window.
    /// </summary>
    private void FindToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FindWindow findInstance = FindInstance;
        if (findInstance != null && !findInstance.Visible)
        {
            findInstance.Show(this);
        }
    }






    /// <summary>
    ///     Handles mouse click events on the results ListView, opening registry location on right-click.
    /// </summary>
    private void LvwResults_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && lvwResults?.FocusedItem != null && lvwResults.FocusedItem.SubItems.Count > 1)
        {
            try
            {
                RegistryUtils.OpenRegistryLocation(lvwResults.FocusedItem.SubItems[1].Text);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Error opening registry location: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }






    /// <summary>
    ///     Handles key down events for the main window, providing shortcuts for find, copy, and select all.
    /// </summary>
    private void MainWindow_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.F)
        {
            FindWindow findInstance = FindInstance;
            if (findInstance != null && !findInstance.Visible)
            {
                findInstance.Show(this);
            }
        }
        else if (lvwResults != null && lvwResults.Focused && e.Control && e.KeyCode == Keys.C)
        {
            try
            {
                Clipboard.SetData(DataFormats.StringFormat, lvwResults.GetAllSelectedSubItemsText(1));
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Error copying to clipboard: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else if (lvwResults != null && lvwResults.Focused && e.Control && e.KeyCode == Keys.A)
        {
            lvwResults.SelectAllItems();
        }
    }






    /// <summary>
    ///     Handles the Load event for the main window, initializing the registry tree.
    /// </summary>
    private void MainWindow_Load(object sender, EventArgs e)
    {
        try
        {
            InitializeRegistryTree();
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show($"Error initializing registry tree: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }






    /// <summary>
    ///     Handles progress changed events, updating the UI and logging progress.
    /// </summary>
    private void ProgressChanged(object sender, ProgressChangedArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(() => ProgressChanged(sender, e));

            return;
        }

        Text = $"Registry Editor | Collecting Data from {e.Hive}";
        Console.WriteLine(e.Message);
    }






    /// <summary>
    ///     Handles progress finished events, resetting the UI and logging completion.
    /// </summary>
    private void ProgressFinished(object sender, ProgressFinishedArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(() => ProgressFinished(sender, e));

            return;
        }

        Text = "Registry Editor";
        Console.WriteLine(e.Message);
    }






    /// <summary>
    ///     Handles progress started events, clearing results and updating the UI.
    /// </summary>
    private void ProgressStarted(object sender, ProgressStartedArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(() => ProgressStarted(sender, e));

            return;
        }

        lvwResults?.Items.Clear();
        if (lblResultsCount != null)
        {
            lblResultsCount.Text = "Results Count: 0";
        }

        Console.WriteLine(e.Message);
    }






    /// <summary>
    ///     Handles the event when a single result is found, adding it to the results asynchronously.
    /// </summary>
    private async void SingleResultFound(object sender, FindSingleResultArgs e)
    {
        if (lvwResults == null || cbxScrollToCaret == null)
        {
            return;
        }

        await Task.Run(() => lvwResults.InvokeSafe(() => AddItem(e.Match, true, cbxScrollToCaret.Checked)));
    }






    /// <summary>
    ///     Handles the click event for starting ETW registry monitoring.
    /// </summary>
    private void startToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (RegistryEtwMonitor.IsMonitoring)
        {
            _ = MessageBox.Show("ETW Monitoring is already running.", "Already Running", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }

        RegistryEtwMonitor etw = new();
        etw.RegistryEvent += (s, args) =>
        {
            string itemText = $"{args.EventName} → {args.KeyName}";
            if (args.UserName != null)
            {
                itemText += $" by {args.ProcessName} (PID {args.ProcessId}), User: {args.UserName}";
            }

            if (lvwResults != null && cbxScrollToCaret != null)
            {
                lvwResults.InvokeSafe(() => AddItem(itemText, true, cbxScrollToCaret.Checked));
            }
        };
        if (startToolStripMenuItem != null)
        {
            startToolStripMenuItem.Text = "Stop Monitoring";
        }

        etw.MonitoringError += (s, ex) => { };
        etw.MonitoringStarted += (s, args) =>
        {
            if (startToolStripMenuItem != null)
            {
                startToolStripMenuItem.Text = "Stop Monitoring";
            }
        };
        etw.MonitoringStopped += (s, args) =>
        {
            if (startToolStripMenuItem != null)
            {
                startToolStripMenuItem.Text = "Start Monitoring";
            }
        };
        etw.SessionName = "RegistryEditorSession";
        etw.LogFilePath = Path.Combine(GetFolderPath(SpecialFolder.Desktop), "RegistryEditorLog.txt");
        etw.ProcessNameFilter = null; // Set to null or a list of process names to filter
        etw.UserFilter = null; // Set to null or a list of usernames to filter
        etw.KeyNameFilter = null; // Set to null or a list of key names to filter
        etw.EventTypeFilter = null; // Set to null or a list of event types to filter
        etw.RunAsync = true;
        etw.StartMonitoring();
    }






    /// <summary>
    ///     Handles the click event for stopping any ongoing operation, such as ETW monitoring.
    /// </summary>
    private void stopToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Logic to stop any ongoing operation
        // Assuming there is a method or property to handle stopping the operation
        if (RegistryEtwMonitor.IsMonitoring)
        {
            _ = RegistryEtwMonitor.Session?.Stop();
            _ = MessageBox.Show("Operation stopped successfully.", "Stop Operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            _ = MessageBox.Show("No operation is currently running.", "Stop Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }






    /// <summary>
    ///     Handles the BeforeExpand event for the registry tree view, loading subkeys dynamically.
    /// </summary>
    private void TreeViewBeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
        if (e.Node == null || e.Node.Nodes.Count != 1 || e.Node.Nodes[0].Text != string.Empty)
        {
            return;
        }

        e.Node.Nodes.Clear();

        if (e.Node.Tag is not RegistryKey key)
        {
            return;
        }

        foreach (string name in key.GetSubKeyNames())
        {
            _ = e.Node.Nodes.Add(name, name);

            if (name is "SECURITY" or "SAM")
            {
                continue;
            }

            RegistryKey subKey = key.OpenSubKey(name);
            e.Node.Nodes[name].Tag = subKey;
            if (subKey != null && subKey.SubKeyCount > 0)
            {
                _ = e.Node.Nodes[name].Nodes.Add(string.Empty);
            }
        }
    }






    /// <summary>
    ///     Handles the click event for toggling the view mode of the results ListView.
    /// </summary>
    private void viewToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lvwResults == null || viewToolStripMenuItem == null)
        {
            return;
        }

        if (lvwResults.View == View.Details)
        {
            lvwResults.View = View.List;
            viewToolStripMenuItem.Text = "View as Details";
        }
        else
        {
            lvwResults.View = View.Details;
            viewToolStripMenuItem.Text = "View as List";
        }
    }

}