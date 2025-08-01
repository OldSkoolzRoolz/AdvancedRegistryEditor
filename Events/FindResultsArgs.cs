// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: FindResultsArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;
using System.Collections.Generic;

namespace Windows.RegistryEditor.Events;
/// <summary>
///     Provides data for the event that contains the results of a search operation.
/// </summary>
public class FindResultsArgs : EventArgs
{

    /// <summary>
    ///     Initializes a new instance of the <see cref="FindResultsArgs" /> class with the specified matches.
    /// </summary>
    /// <param name="matches">The list of matches found during the search operation.</param>
    public FindResultsArgs(List<string> matches)
    {
        Matches = matches;
    }






    /// <summary>
    ///     Gets or sets the list of matches found during the search operation.
    /// </summary>
    public List<string> Matches { get; set; }

}