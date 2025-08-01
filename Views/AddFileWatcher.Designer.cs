// "Open Source copyrights apply - All code can be reused DO NOT remove author tags"

namespace Windows.RegistryEditor.Views;

partial class AddFileWatcher
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
        components = new System.ComponentModel.Container();
        label1 = new System.Windows.Forms.Label();
        tb_FilePath = new System.Windows.Forms.TextBox();
        button1 = new System.Windows.Forms.Button();
        label2 = new System.Windows.Forms.Label();
        tb_action = new System.Windows.Forms.RichTextBox();
        errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
        folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
        openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        button2 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        label1.Location = new System.Drawing.Point(33, 66);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(109, 21);
        label1.TabIndex = 0;
        label1.Text = "Filename/Path";
        // 
        // tb_FilePath
        // 
        tb_FilePath.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tb_FilePath.Location = new System.Drawing.Point(148, 66);
        tb_FilePath.Name = "tb_FilePath";
        tb_FilePath.Size = new System.Drawing.Size(372, 27);
        tb_FilePath.TabIndex = 1;
        // 
        // button1
        // 
        button1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        button1.Location = new System.Drawing.Point(187, 264);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(214, 28);
        button1.TabIndex = 2;
        button1.Text = "Validate - Save";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        label2.Location = new System.Drawing.Point(14, 116);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(128, 21);
        label2.TabIndex = 3;
        label2.Text = "Action to be  run:";
        // 
        // tb_action
        // 
        tb_action.Location = new System.Drawing.Point(148, 116);
        tb_action.Name = "tb_action";
        tb_action.Size = new System.Drawing.Size(339, 115);
        tb_action.TabIndex = 4;
        tb_action.Text = "";
        // 
        // errorProvider1
        // 
        errorProvider1.ContainerControl = this;
        // 
        // openFileDialog1
        // 
        openFileDialog1.FileName = "openFileDialog1";
        // 
        // button2
        // 
        button2.Location = new System.Drawing.Point(536, 62);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(99, 29);
        button2.TabIndex = 5;
        button2.Text = "Browse...";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // AddFileWatcher
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        BackColor = System.Drawing.SystemColors.AppWorkspace;
        ClientSize = new System.Drawing.Size(647, 332);
        Controls.Add(button2);
        Controls.Add(tb_action);
        Controls.Add(label2);
        Controls.Add(button1);
        Controls.Add(tb_FilePath);
        Controls.Add(label1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AddFileWatcher";
        ShowInTaskbar = false;
        Text = "AddFileWatcher";
        TopMost = true;
        ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tb_FilePath;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.RichTextBox tb_action;
    private System.Windows.Forms.ErrorProvider errorProvider1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
}