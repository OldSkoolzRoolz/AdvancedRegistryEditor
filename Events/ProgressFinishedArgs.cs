// Project Name: RegistryEditor
// File Name: ProgressFinishedArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




namespace Windows.RegistryEditor.Events;


public class ProgressFinishedArgs : ProgressArgs
{

    public ProgressFinishedArgs(string message) : base(message)
    {
    }

}