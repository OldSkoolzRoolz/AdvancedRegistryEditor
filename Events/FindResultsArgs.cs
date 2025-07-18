// Project Name: RegistryEditor
// File Name: FindResultsArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




#region

using System;
using System.Collections.Generic;

#endregion



namespace Windows.RegistryEditor.Events;


public class FindResultsArgs : EventArgs
{

    public FindResultsArgs(List<string> matches)
    {
        Matches = matches;
    }






    public List<string> Matches { get; set; }

}