// Project Name: RegistryEditor
// File Name: ProgressChangedArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




namespace Windows.RegistryEditor.Events;


public class ProgressChangedArgs : ProgressArgs
{

    public ProgressChangedArgs(string message, string hive) : base(message)
    {
        Hive = hive;
    }






    public string Hive { get; set; }

}