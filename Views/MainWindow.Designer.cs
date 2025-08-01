namespace Windows.RegistryEditor.Views
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Computer");
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            loadHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            unloadHiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            modifyBinaryDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            keyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            stringValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            binaryValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dWORD32BitValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dWORD64bitValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            multiStringValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            expandableStringValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            permissionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            copyKeyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            favoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            removeFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fileWatcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            treeView = new System.Windows.Forms.TreeView();
            splitContainer = new System.Windows.Forms.SplitContainer();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            cbxScrollToCaret = new System.Windows.Forms.CheckBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            cbxSelectAll = new System.Windows.Forms.CheckBox();
            btnDelete = new System.Windows.Forms.Button();
            lblResultsCount = new System.Windows.Forms.Label();
            columnCbx = new System.Windows.Forms.ColumnHeader();
            columnHkey = new System.Windows.Forms.ColumnHeader();
            cbx = new System.Windows.Forms.ColumnHeader();
            HKEY = new System.Windows.Forms.ColumnHeader();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, favoritesToolStripMenuItem, helpToolStripMenuItem, monitorToolStripMenuItem, fileWatcherToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(1115, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator2, loadHideToolStripMenuItem, unloadHiveToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            importToolStripMenuItem.Text = "Import...";
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            exportToolStripMenuItem.Text = "Export...";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // loadHideToolStripMenuItem
            // 
            loadHideToolStripMenuItem.Name = "loadHideToolStripMenuItem";
            loadHideToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            loadHideToolStripMenuItem.Text = "Load Hive...";
            // 
            // unloadHiveToolStripMenuItem
            // 
            unloadHiveToolStripMenuItem.Name = "unloadHiveToolStripMenuItem";
            unloadHiveToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            unloadHiveToolStripMenuItem.Text = "Unload Hive...";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { modifyToolStripMenuItem, modifyBinaryDataToolStripMenuItem, toolStripSeparator3, newToolStripMenuItem, toolStripSeparator5, permissionsToolStripMenuItem, toolStripSeparator6, deleteToolStripMenuItem, renameToolStripMenuItem, toolStripSeparator7, copyKeyNameToolStripMenuItem, toolStripSeparator8, findToolStripMenuItem, findNextToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // modifyToolStripMenuItem
            // 
            modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            modifyToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            modifyToolStripMenuItem.Text = "Modify...";
            // 
            // modifyBinaryDataToolStripMenuItem
            // 
            modifyBinaryDataToolStripMenuItem.Name = "modifyBinaryDataToolStripMenuItem";
            modifyBinaryDataToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            modifyBinaryDataToolStripMenuItem.Text = "Modify Binary Data...";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { keyToolStripMenuItem, toolStripSeparator4, stringValueToolStripMenuItem, binaryValueToolStripMenuItem, dWORD32BitValueToolStripMenuItem, dWORD64bitValueToolStripMenuItem, multiStringValueToolStripMenuItem, expandableStringValueToolStripMenuItem });
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            newToolStripMenuItem.Text = "New";
            // 
            // keyToolStripMenuItem
            // 
            keyToolStripMenuItem.Name = "keyToolStripMenuItem";
            keyToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            keyToolStripMenuItem.Text = "Key";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(196, 6);
            // 
            // stringValueToolStripMenuItem
            // 
            stringValueToolStripMenuItem.Name = "stringValueToolStripMenuItem";
            stringValueToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            stringValueToolStripMenuItem.Text = "String Value";
            // 
            // binaryValueToolStripMenuItem
            // 
            binaryValueToolStripMenuItem.Name = "binaryValueToolStripMenuItem";
            binaryValueToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            binaryValueToolStripMenuItem.Text = "Binary Value";
            // 
            // dWORD32BitValueToolStripMenuItem
            // 
            dWORD32BitValueToolStripMenuItem.Name = "dWORD32BitValueToolStripMenuItem";
            dWORD32BitValueToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            dWORD32BitValueToolStripMenuItem.Text = "DWORD (32-bit) Value";
            // 
            // dWORD64bitValueToolStripMenuItem
            // 
            dWORD64bitValueToolStripMenuItem.Name = "dWORD64bitValueToolStripMenuItem";
            dWORD64bitValueToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            dWORD64bitValueToolStripMenuItem.Text = "QWORD (64-bit) Value";
            // 
            // multiStringValueToolStripMenuItem
            // 
            multiStringValueToolStripMenuItem.Name = "multiStringValueToolStripMenuItem";
            multiStringValueToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            multiStringValueToolStripMenuItem.Text = "Multi-String Value";
            // 
            // expandableStringValueToolStripMenuItem
            // 
            expandableStringValueToolStripMenuItem.Name = "expandableStringValueToolStripMenuItem";
            expandableStringValueToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            expandableStringValueToolStripMenuItem.Text = "Expandable String Value";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(181, 6);
            // 
            // permissionsToolStripMenuItem
            // 
            permissionsToolStripMenuItem.Name = "permissionsToolStripMenuItem";
            permissionsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            permissionsToolStripMenuItem.Text = "Permissions...";
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new System.Drawing.Size(181, 6);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            deleteToolStripMenuItem.Text = "Delete";
            // 
            // renameToolStripMenuItem
            // 
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            renameToolStripMenuItem.Text = "Rename";
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new System.Drawing.Size(181, 6);
            // 
            // copyKeyNameToolStripMenuItem
            // 
            copyKeyNameToolStripMenuItem.Name = "copyKeyNameToolStripMenuItem";
            copyKeyNameToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            copyKeyNameToolStripMenuItem.Text = "Copy Key Name";
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new System.Drawing.Size(181, 6);
            // 
            // findToolStripMenuItem
            // 
            findToolStripMenuItem.Name = "findToolStripMenuItem";
            findToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            findToolStripMenuItem.Text = "Find...";
            findToolStripMenuItem.Click += FindToolStripMenuItem_Click;
            // 
            // findNextToolStripMenuItem
            // 
            findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            findNextToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            findNextToolStripMenuItem.Text = "Find Next";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            viewToolStripMenuItem.Click += viewToolStripMenuItem_Click;
            // 
            // favoritesToolStripMenuItem
            // 
            favoritesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { addToFavoritesToolStripMenuItem, removeFavoritesToolStripMenuItem });
            favoritesToolStripMenuItem.Name = "favoritesToolStripMenuItem";
            favoritesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            favoritesToolStripMenuItem.Text = "Favorites";
            // 
            // addToFavoritesToolStripMenuItem
            // 
            addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
            addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            addToFavoritesToolStripMenuItem.Text = "Add to Favorites...";
            // 
            // removeFavoritesToolStripMenuItem
            // 
            removeFavoritesToolStripMenuItem.Name = "removeFavoritesToolStripMenuItem";
            removeFavoritesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            removeFavoritesToolStripMenuItem.Text = "Remove Favorites...";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            aboutToolStripMenuItem.Text = "About Registry Editor...";
            // 
            // monitorToolStripMenuItem
            // 
            monitorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { startToolStripMenuItem, stopToolStripMenuItem });
            monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            monitorToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            monitorToolStripMenuItem.Text = "Monitor";
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            startToolStripMenuItem.Text = "Start";
            startToolStripMenuItem.ToolTipText = "Start monitoring the registry for changes.\r\n";
            startToolStripMenuItem.Click += startToolStripMenuItem_Click;
            // 
            // stopToolStripMenuItem
            // 
            stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            stopToolStripMenuItem.Text = "Stop";
            stopToolStripMenuItem.ToolTipText = "Stop Registry change monitor.";
            stopToolStripMenuItem.Click += stopToolStripMenuItem_Click;
            // 
            // fileWatcherToolStripMenuItem
            // 
            fileWatcherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { addToolStripMenuItem, toolStripMenuItem1 });
            fileWatcherToolStripMenuItem.Name = "fileWatcherToolStripMenuItem";
            fileWatcherToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            fileWatcherToolStripMenuItem.Text = "File Watcher";
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            addToolStripMenuItem.Text = "Add";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            // 
            // treeView
            // 
            treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView.Location = new System.Drawing.Point(0, 0);
            treeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            treeView.Name = "treeView";
            treeNode1.Name = "Computer";
            treeNode1.Text = "Computer";
            treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode1 });
            treeView.Size = new System.Drawing.Size(235, 622);
            treeView.TabIndex = 0;
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.Location = new System.Drawing.Point(0, 24);
            splitContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(treeView);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(splitContainer1);
            splitContainer.Size = new System.Drawing.Size(1115, 622);
            splitContainer.SplitterDistance = 235;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 2;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(cbxScrollToCaret);
            splitContainer1.Panel1.Controls.Add(label3);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(cbxSelectAll);
            splitContainer1.Panel1.Controls.Add(btnDelete);
            splitContainer1.Panel1.Controls.Add(lblResultsCount);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            splitContainer1.Size = new System.Drawing.Size(875, 622);
            splitContainer1.SplitterDistance = 54;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // cbxScrollToCaret
            // 
            cbxScrollToCaret.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            cbxScrollToCaret.AutoSize = true;
            cbxScrollToCaret.Checked = true;
            cbxScrollToCaret.CheckState = System.Windows.Forms.CheckState.Checked;
            cbxScrollToCaret.Location = new System.Drawing.Point(687, 35);
            cbxScrollToCaret.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbxScrollToCaret.Name = "cbxScrollToCaret";
            cbxScrollToCaret.Size = new System.Drawing.Size(167, 19);
            cbxScrollToCaret.TabIndex = 5;
            cbxScrollToCaret.Text = "Automatically scroll down.";
            cbxScrollToCaret.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(255, 27);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(307, 15);
            label3.TabIndex = 4;
            label3.Text = "Right Click on one of the results lines to see some magic.";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(255, 6);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(177, 15);
            label2.TabIndex = 4;
            label2.Text = "CTRL + F to search for registries.";
            // 
            // cbxSelectAll
            // 
            cbxSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            cbxSelectAll.AutoSize = true;
            cbxSelectAll.Checked = true;
            cbxSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            cbxSelectAll.Location = new System.Drawing.Point(8, 34);
            cbxSelectAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbxSelectAll.Name = "cbxSelectAll";
            cbxSelectAll.Size = new System.Drawing.Size(74, 19);
            cbxSelectAll.TabIndex = 1;
            cbxSelectAll.Text = "Select All";
            cbxSelectAll.UseVisualStyleBackColor = true;
            cbxSelectAll.CheckedChanged += CbxSelectAll_CheckedChanged;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnDelete.Location = new System.Drawing.Point(680, 6);
            btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(191, 27);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete All Selected Registries";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            // 
            // lblResultsCount
            // 
            lblResultsCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblResultsCount.AutoSize = true;
            lblResultsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblResultsCount.Location = new System.Drawing.Point(6, 9);
            lblResultsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblResultsCount.Name = "lblResultsCount";
            lblResultsCount.Size = new System.Drawing.Size(128, 17);
            lblResultsCount.TabIndex = 1;
            lblResultsCount.Text = "Results Count: 0";
            // 
            // columnCbx
            // 
            columnCbx.Text = "";
            columnCbx.Width = 24;
            // 
            // columnHkey
            // 
            columnHkey.Text = "HKEY";
            columnHkey.Width = 1980;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1115, 646);
            Controls.Add(splitContainer);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            Icon = Properties.Resources.app;
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainWindow";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Registry Editor";
            Load += MainWindow_Load;
            KeyDown += MainWindow_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        private void InitializeRegistryTree()
        {
            treeView.TopNode.Nodes.Add(Microsoft.Win32.Registry.ClassesRoot.Name, Microsoft.Win32.Registry.ClassesRoot.Name);
            treeView.TopNode.Nodes.Add(Microsoft.Win32.Registry.CurrentUser.Name, Microsoft.Win32.Registry.CurrentUser.Name);
            treeView.TopNode.Nodes.Add(Microsoft.Win32.Registry.LocalMachine.Name, Microsoft.Win32.Registry.LocalMachine.Name);
            treeView.TopNode.Nodes.Add(Microsoft.Win32.Registry.Users.Name, Microsoft.Win32.Registry.Users.Name);
            treeView.TopNode.Nodes.Add(Microsoft.Win32.Registry.CurrentConfig.Name, Microsoft.Win32.Registry.CurrentConfig.Name);

            treeView.TopNode.Nodes[Microsoft.Win32.Registry.ClassesRoot.Name].Nodes.Add(System.String.Empty);
            treeView.TopNode.Nodes[Microsoft.Win32.Registry.CurrentUser.Name].Nodes.Add(System.String.Empty);
            treeView.TopNode.Nodes[Microsoft.Win32.Registry.LocalMachine.Name].Nodes.Add(System.String.Empty);
            treeView.TopNode.Nodes[Microsoft.Win32.Registry.Users.Name].Nodes.Add(System.String.Empty);
            treeView.TopNode.Nodes[Microsoft.Win32.Registry.CurrentConfig.Name].Nodes.Add(System.String.Empty);

            treeView.TopNode.Nodes[Microsoft.Win32.Registry.ClassesRoot.Name].Tag = Microsoft.Win32.Registry.ClassesRoot;
            treeView.TopNode.Nodes[Microsoft.Win32.Registry.CurrentUser.Name].Tag = Microsoft.Win32.Registry.CurrentUser;
            treeView.TopNode.Nodes[Microsoft.Win32.Registry.LocalMachine.Name].Tag = Microsoft.Win32.Registry.LocalMachine;
            treeView.TopNode.Nodes[Microsoft.Win32.Registry.Users.Name].Tag = Microsoft.Win32.Registry.Users;
            treeView.TopNode.Nodes[Microsoft.Win32.Registry.CurrentConfig.Name].Tag = Microsoft.Win32.Registry.CurrentConfig;

            treeView.TopNode.Expand();

            treeView.BeforeExpand += TreeViewBeforeExpand;
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem loadHideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unloadHiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyBinaryDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem stringValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binaryValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dWORD32BitValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dWORD64bitValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiStringValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandableStringValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem permissionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem copyKeyNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findNextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFavoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblResultsCount;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader cbx;
        private System.Windows.Forms.ColumnHeader HKEY;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.CheckBox cbxSelectAll;
        private Windows.RegistryEditor.Views.Controls.ListViewEx lvwResults;
        private System.Windows.Forms.ColumnHeader columnCbx;
        private System.Windows.Forms.ColumnHeader columnHkey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbxScrollToCaret;
        private System.Windows.Forms.ToolStripMenuItem monitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileWatcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}