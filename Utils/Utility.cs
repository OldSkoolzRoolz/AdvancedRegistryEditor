// Project Name: RegistryEditor
// File Name: Utility.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Windows.RegistryEditor.Utils.Native;

#endregion



namespace Windows.RegistryEditor.Utils;


public static class Utility
{

    public static void BeginInvokeSafe(this ISynchronizeInvoke caller, Action method)
    {
        if (caller.InvokeRequired)
            caller.BeginInvoke(method, null);
        else
            method.Invoke();
    }






    public static void DeselectAllItems(this ListView listView)
    {
        NativeMethods.SetItemState(listView, -1, 2, 0);
    }






    public static void InvokeSafe(this ISynchronizeInvoke caller, Action method)
    {
        if (caller.InvokeRequired)
            caller.Invoke(method, null);
        else
            method.Invoke();
    }






    public static T InvokeSafe<T>(this ISynchronizeInvoke caller, Func<T> method)
    {
        if (caller.InvokeRequired)
            return (T)caller.Invoke(method, null);

        return method.Invoke();
    }






    public static bool KillProcess(string proc)
    {
        foreach (var process in Process.GetProcesses())
        {
            if (process.ProcessName.ToLower() != proc) continue;

            process.Kill();

            return true;
        }

        return false;
    }






    public static void OpenFolderInExplorer(string path)
    {
        Process.Start("explorer.exe", path);
    }






    public static void SelectAllItems(this ListView listView)
    {
        NativeMethods.SetItemState(listView, -1, 2, 2);
    }






    /// <summary>
    ///     Characters that are not allowed on windows:
    ///     < > : " / \ | ?
    /// </summary>
    public static string ValidateDirOrFileName(string name)
    {
        return Regex.Replace(name, "<|>|:|\"|/|\\\\|\\||\\?|\\*", " ");
    }

}