// Project Name: RegistryEditor
// File Name: FindSingleResultArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




#region

using System;

#endregion



namespace Windows.RegistryEditor.Events;


public class FindSingleResultArgs : EventArgs
{

    public FindSingleResultArgs(string match)
    {
        Match = match;
    }






    public string Match { get; set; }

}