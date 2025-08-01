// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: CheckBoxes.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




#region

namespace Windows.RegistryEditor.Views.Controls;


using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;



/// <summary>
///     Represents a custom UserControl that provides a CheckedListBox with additional features,
///     such as "Select All" functionality and extended property support.
/// </summary>
public partial class CheckBoxes : UserControl
{

    /// <summary>
    ///     Initializes a new instance of the <see cref="CheckBoxes" /> class.
    /// </summary>
    public CheckBoxes()
    {
        InitializeComponent();
        CheckOnClick = true;
        lbxAll.Dock = DockStyle.Fill;

        foreach (Control control in panelMain.Controls) _ = typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, control, new object[] { true });
    }






    /// <summary>
    ///     Gets the creation parameters for the control, enabling double-buffering for smoother rendering.
    /// </summary>
    [Browsable(false)]
    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED

            return cp;
        }
    }

    /// <summary>
    ///     Gets the collection of items in this CheckedListBox.
    /// </summary>
    [MergableProperty(false)]
    [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Category("Data")]
    [Description("Gets the collection of items in this CheckedListBox.")]
    public CheckedListBox.ObjectCollection Items
    {
        get
        {
            CbxAll_CheckedChanged(null, null);

            return lbxAll.Items;
        }
    }

    /// <summary>
    ///     Gets or sets the data source for the CheckedListBox.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("CheckedListBox DataSource.")]
    public object DataSource
    {
        get => lbxAll.DataSource;
        set => lbxAll.DataSource = value;
    }

    /// <summary>
    ///     Gets or sets the background image for the control. Deprecated.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Obsolete("Deprecated", false)]
    public new Image BackgroundImage { get; set; }

    /// <summary>
    ///     Gets or sets the background image layout for the control. Deprecated.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new ImageLayout BackgroundImageLayout { get; set; }

    /// <summary>
    ///     Gets or sets the background color of the CheckedListBox.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    public new Color BackColor
    {
        get => lbxAll.BackColor;
        set => UpdateControl(() => lbxAll.BackColor = value);
    }

    /// <summary>
    ///     Gets or sets the border style of the CheckedListBox.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    public new BorderStyle BorderStyle
    {
        get => lbxAll.BorderStyle;
        set => UpdateControl(() => lbxAll.BorderStyle = value);
    }

    /// <summary>
    ///     Gets or sets the foreground color of the CheckedListBox.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    public new Color ForeColor
    {
        get => lbxAll.ForeColor;
        set => UpdateControl(() => lbxAll.ForeColor = value);
    }

    /// <summary>
    ///     Gets or sets a value indicating whether the checkboxes should appear as flat or 3D.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Category("Appearance")]
    [Description("Indicates if the CheckBoxes should show up as flat or 3D in appearance.")]
    public bool ThreeDCheckBoxes
    {
        get => lbxAll.ThreeDCheckBoxes;
        set => UpdateControl(() => lbxAll.ThreeDCheckBoxes = value);
    }

    /// <summary>
    ///     Gets or sets a value indicating whether the checkbox should be toggled with the first click on an item.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Category("Behavior")]
    [Description("Indicates if the check box should be toggled with the first click on an item.")]
    public bool CheckOnClick
    {
        get => lbxAll.CheckOnClick;
        set => lbxAll.CheckOnClick = value;
    }

    /// <summary>
    ///     Gets the collection of checked items in this CheckedListBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Collection of checked items in this CheckedListBox.")]
    public CheckedListBox.CheckedItemCollection CheckedItems => lbxAll.CheckedItems;

    /// <summary>
    ///     Gets or sets a value indicating whether all checkboxes are checked in the CheckedListBox.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Browsable(true)]
    [Description("Indicates if all check boxes are checked in the CheckedListBox.")]
    [Category("Appearance")]
    public bool SelectAllChecked
    {
        get => cbxAll.Checked;
        set => UpdateControl(() => cbxAll.Checked = value);
    }






    /// <summary>
    ///     Handles the CheckedChanged event for the "Select All" checkbox.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void CbxAll_CheckedChanged(object sender, EventArgs e)
    {
        for (var i = 0; i < lbxAll.Items.Count; i++) lbxAll.SetItemChecked(i, cbxAll.Checked);
        OnSelectAllCheckedChanged();
    }






    /// <summary>
    ///     Handles the Load event for the control.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void CheckBoxes_Load(object sender, EventArgs e)
    {
    }






    /// <summary>
    ///     Indicates if the given item is checked (fully or indeterminately).
    /// </summary>
    /// <param name="index">The index of the item.</param>
    /// <returns><c>true</c> if the item is checked; otherwise, <c>false</c>.</returns>
    public bool GetItemChecked(int index)
    {
        return lbxAll.GetItemChecked(index);
    }






    /// <summary>
    ///     Indicates if the item with the specified name is checked.
    /// </summary>
    /// <param name="value">The name of the item.</param>
    /// <returns><c>true</c> if the item is checked; otherwise, <c>false</c>.</returns>
    /// <exception cref="InstanceNotFoundException">Thrown if the item is not found.</exception>
    public bool GetItemCheckedByName(string value)
    {
        for (var i = 0; i < lbxAll.Items.Count; i++)
            if (lbxAll.Items[i].ToString().Equals(value))
                return lbxAll.GetItemChecked(i);

        throw new InstanceNotFoundException($"[GetItemCheckedByName] - Couldn't find the specified value -> {value}.");
    }






    /// <summary>
    ///     Gets the check state of the specified item.
    /// </summary>
    /// <param name="index">The index of the item.</param>
    /// <returns>The <see cref="CheckState" /> of the item.</returns>
    public CheckState GetItemCheckState(int index)
    {
        return lbxAll.GetItemCheckState(index);
    }






    /// <summary>
    ///     Occurs when the check state of an item changes.
    /// </summary>
    public event ItemCheckEventHandler ItemCheck;






    /// <summary>
    ///     Handles the ItemCheck event for the CheckedListBox.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void LbxAll_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (e.NewValue == CheckState.Unchecked)
        {
            cbxAll.CheckedChanged -= CbxAll_CheckedChanged;
            cbxAll.Checked = false;
            cbxAll.CheckedChanged += CbxAll_CheckedChanged;
        }
        else if (lbxAll.Items.Count == lbxAll.CheckedItems.Count + 1 && e.NewValue == CheckState.Checked)
        {
            cbxAll.CheckedChanged -= CbxAll_CheckedChanged;
            cbxAll.Checked = true;
            cbxAll.CheckedChanged += CbxAll_CheckedChanged;
        }

        OnItemCheck(e);
    }






    /// <summary>
    ///     Raises the <see cref="ItemCheck" /> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnItemCheck(ItemCheckEventArgs e)
    {
        ItemCheck?.Invoke(this, e);
    }






    /// <summary>
    ///     Raises the <see cref="SelectAllCheckedChanged" /> event.
    /// </summary>
    protected virtual void OnSelectAllCheckedChanged()
    {
        SelectAllCheckedChanged?.Invoke(this, EventArgs.Empty);
    }






    /// <summary>
    ///     Occurs when the "Select All" checkbox state changes.
    /// </summary>
    public event EventHandler SelectAllCheckedChanged;






    /// <summary>
    ///     Sets the checked value of the specified item.
    /// </summary>
    /// <param name="index">The index of the item.</param>
    /// <param name="value">The checked value to set.</param>
    public void SetItemChecked(int index, bool value)
    {
        lbxAll.SetItemChecked(index, value);
    }






    /// <summary>
    ///     Sets the checked value of the item with the specified name.
    /// </summary>
    /// <param name="value">The name of the item.</param>
    /// <param name="flag">The checked value to set.</param>
    /// <exception cref="InstanceNotFoundException">Thrown if the item is not found.</exception>
    public void SetItemCheckedByName(string value, bool flag)
    {
        for (var i = 0; i < lbxAll.Items.Count; i++)
            if (lbxAll.Items[i].ToString().Equals(value))
                lbxAll.SetItemChecked(i, flag);

        throw new InstanceNotFoundException($"[SetItemCheckedByName] - Couldn't find the specified value -> {value}.");
    }






    /// <summary>
    ///     Sets the check state of the specified item.
    /// </summary>
    /// <param name="index">The index of the item.</param>
    /// <param name="value">The <see cref="CheckState" /> to set.</param>
    public void SetItemCheckState(int index, CheckState value)
    {
        lbxAll.SetItemCheckState(index, value);
    }






    /// <summary>
    ///     Updates the control by invoking the specified action and invalidates the control in design mode.
    /// </summary>
    /// <param name="action">The action to perform.</param>
    public void UpdateControl(Action action)
    {
        action.Invoke();
        if (DesignMode) Invalidate();
    }

}


/// <summary>
///     Represents an exception that is thrown when an instance is not found.
/// </summary>
public class InstanceNotFoundException : Exception
{

    /// <summary>
    ///     Initializes a new instance of the <see cref="InstanceNotFoundException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public InstanceNotFoundException(string message) : base(message)
    {
    }

}