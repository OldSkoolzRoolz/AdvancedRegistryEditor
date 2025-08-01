// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: ProgressArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;

namespace Windows.RegistryEditor.Events;
/// <summary>
/// </summary>
public class ProgressArgs : EventArgs
{

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    public ProgressArgs(string message)
    {
        Message = message;
    }






    /// <summary>
    /// </summary>
    public string Message { get; set; }

}