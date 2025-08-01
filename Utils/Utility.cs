// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: Utility.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Windows.RegistryEditor.Utils;
/// <summary>
///     Provides utility extension methods and helpers for UI and process management in the RegistryEditor application.
/// </summary>
public static class Utility
{

    /// <summary>
    ///     Safely invokes the specified action asynchronously on the UI thread if required.
    /// </summary>
    /// <param name="caller">The object used to marshal calls to the correct thread.</param>
    /// <param name="method">The action to invoke.</param>
    public static void BeginInvokeSafe(this ISynchronizeInvoke caller, Action method)
    {
        if (caller.InvokeRequired)
        {
            _ = caller.BeginInvoke(method, null);
        }
        else
        {
            method.Invoke();
        }
    }






    /// <summary>
    ///     Deselects all items in the specified <see cref="ListView" /> control.
    /// </summary>
    /// <param name="listView">The ListView control whose items will be deselected.</param>
    public static void DeselectAllItems(this ListView listView)
    {
        NativeMethods.SetItemState(listView, -1, 2, 0);
    }






    /// <summary>
    ///     Safely invokes the specified action synchronously on the UI thread if required.
    /// </summary>
    /// <param name="caller">The object used to marshal calls to the correct thread.</param>
    /// <param name="method">The action to invoke.</param>
    public static void InvokeSafe(this ISynchronizeInvoke caller, Action method)
    {
        if (caller.InvokeRequired)
        {
            _ = caller.Invoke(method, null);
        }
        else
        {
            method.Invoke();
        }
    }






    /// <summary>
    ///     Safely invokes the specified function synchronously on the UI thread if required and returns its result.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="caller">The object used to marshal calls to the correct thread.</param>
    /// <param name="method">The function to invoke.</param>
    /// <returns>The result of the invoked function.</returns>
    public static T InvokeSafe<T>(this ISynchronizeInvoke caller, Func<T> method)
    {
        return caller.InvokeRequired ? (T)caller.Invoke(method, null) : method.Invoke();
    }






    /// <summary>
    ///     Attempts to kill a process by its name.
    /// </summary>
    /// <param name="proc">The name of the process to kill.</param>
    /// <returns><c>true</c> if the process was found and killed; otherwise, <c>false</c>.</returns>
    public static bool KillProcess(string proc)
    {
        foreach (Process process in Process.GetProcesses())
        {
            if (process.ProcessName.ToLower() != proc)
            {
                continue;
            }

            process.Kill();

            return true;
        }

        return false;
    }






    /// <summary>
    ///     Opens the specified folder in Windows Explorer.
    /// </summary>
    /// <param name="path">The path of the folder to open.</param>
    public static void OpenFolderInExplorer(string path)
    {
        _ = Process.Start("explorer.exe", path);
    }






    /// <summary>
    ///     Selects all items in the specified <see cref="ListView" /> control.
    /// </summary>
    /// <param name="listView">The ListView control whose items will be selected.</param>
    public static void SelectAllItems(this ListView listView)
    {
        NativeMethods.SetItemState(listView, -1, 2, 2);
    }






    /// <summary>
    ///     Validates a directory or file name by replacing invalid characters with a space.
    /// </summary>
    /// <param name="name">The directory or file name to validate.</param>
    /// <returns>A sanitized version of the input name with invalid characters replaced by spaces.</returns>
    /// <remarks>
    ///     Invalid characters include: <c>&lt;</c>, <c>&gt;</c>, <c>:</c>, <c>"</c>, <c>/</c>, <c>\</c>, <c>|</c>, <c>?</c>,
    ///     and <c>*</c>.
    /// </remarks>
    public static string ValidateDirOrFileName(string name)
    {
        return Regex.Replace(name, "<|>|:|\"|/|\\\\|\\||\\?|\\*", " ");
    }

}