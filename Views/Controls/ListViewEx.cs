// Project Name: RegistryEditor
// File Name: ListViewEx.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




#region

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Windows.RegistryEditor.Utils;

#endregion



namespace Windows.RegistryEditor.Views.Controls;


public class ListViewEx : ListView
{

    public ListViewEx()
    {
        CheckBoxes = true;
        FullRowSelect = true;
        GridLines = true;
    }






    public void CheckAllItems(bool value)
    {
        foreach (ListViewItem item in Items)
            item.Checked = value;
    }






    public void DeselectAllItems()
    {
        Utility.DeselectAllItems(this);
    }






    public List<string> GetAllCheckedSubItemsTextList(int index)
    {
        var newList = new List<string>();
        foreach (ListViewItem item in CheckedItems)
            newList.Add(item.SubItems[index].Text);

        return newList;
    }






    public string GetAllSelectedSubItemsText(int index)
    {
        var data = string.Empty;
        foreach (ListViewItem item in SelectedItems)
            data += item.SubItems[index].Text + Environment.NewLine;

        data = data.TrimEnd();

        return data;
    }






    public List<string> GetAllSelectedSubItemsTextList(int index)
    {
        var newList = new List<string>();
        foreach (ListViewItem item in SelectedItems)
            newList.Add(item.SubItems[index].Text);

        return newList;
    }






    public void SelectAllItems()
    {
        Utility.SelectAllItems(this);
    }

}