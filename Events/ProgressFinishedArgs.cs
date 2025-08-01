// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: ProgressFinishedArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




namespace Windows.RegistryEditor.Events;


/// <summary>
/// </summary>
public class ProgressFinishedArgs : ProgressArgs
{

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    public ProgressFinishedArgs(string message) : base(message)
    {
    }

}