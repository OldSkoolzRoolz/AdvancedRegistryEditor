// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: FindSingleResultArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;

namespace Windows.RegistryEditor.Events;
/// <summary>
/// </summary>
public class FindSingleResultArgs : EventArgs
{

    /// <summary>
    /// </summary>
    /// <param name="match"></param>
    public FindSingleResultArgs(string match)
    {
        Match = match;
    }






    /// <summary>
    /// </summary>
    public string Match { get; set; }

}