// Project Name: RegistryEditor
// File Name: ProgressArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




#region

using System;

#endregion



namespace Windows.RegistryEditor.Events;


public class ProgressArgs : EventArgs
{

    public ProgressArgs(string message)
    {
        Message = message;
    }






    public string Message { get; set; }

}