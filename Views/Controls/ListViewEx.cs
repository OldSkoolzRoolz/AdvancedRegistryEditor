// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: ListViewEx.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Windows.RegistryEditor.Views.Controls;
/// <summary>
///     Represents an extended ListView control with additional utility methods for item selection and text retrieval.
/// </summary>
public class ListViewEx : ListView
{

    /// <summary>
    ///     Initializes a new instance of the <see cref="ListViewEx" /> class with default settings.
    /// </summary>
    public ListViewEx()
    {
        CheckBoxes = true;
        FullRowSelect = true;
        GridLines = true;
    }






    /// <summary>
    ///     Checks or unchecks all items in the ListView.
    /// </summary>
    /// <param name="value">True to check all items; false to uncheck all items.</param>
    public void CheckAllItems(bool value)
    {
        foreach (ListViewItem item in Items)
        {
            item.Checked = value;
        }
    }






    /// <summary>
    ///     Deselects all items in the ListView.
    /// </summary>
    public void DeselectAllItems()
    {
        Utility.DeselectAllItems(this);
    }






    /// <summary>
    ///     Gets a list of text values from the specified subitem index for all checked items.
    /// </summary>
    /// <param name="index">The subitem index to retrieve text from.</param>
    /// <returns>A list of text values from checked items at the specified subitem index.</returns>
    public List<string> GetAllCheckedSubItemsTextList(int index)
    {
        List<string> newList = [];
        foreach (ListViewItem item in CheckedItems)
        {
            newList.Add(item.SubItems[index].Text);
        }

        return newList;
    }






    /// <summary>
    ///     Gets a concatenated string of text values from the specified subitem index for all selected items.
    /// </summary>
    /// <param name="index">The subitem index to retrieve text from.</param>
    /// <returns>A string containing the text values of selected items at the specified subitem index, separated by new lines.</returns>
    public string GetAllSelectedSubItemsText(int index)
    {
        string data = string.Empty;
        foreach (ListViewItem item in SelectedItems)
        {
            data += item.SubItems[index].Text + Environment.NewLine;
        }

        data = data.TrimEnd();

        return data;
    }






    /// <summary>
    ///     Gets a list of text values from the specified subitem index for all selected items.
    /// </summary>
    /// <param name="index">The subitem index to retrieve text from.</param>
    /// <returns>A list of text values from selected items at the specified subitem index.</returns>
    public List<string> GetAllSelectedSubItemsTextList(int index)
    {
        List<string> newList = [];
        foreach (ListViewItem item in SelectedItems)
        {
            newList.Add(item.SubItems[index].Text);
        }

        return newList;
    }






    /// <summary>
    ///     Selects all items in the ListView.
    /// </summary>
    public void SelectAllItems()
    {
        Utility.SelectAllItems(this);
    }

}