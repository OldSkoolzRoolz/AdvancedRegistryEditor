// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: ProgressStartedArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




namespace Windows.RegistryEditor.Events;


/// <summary>
/// </summary>
public class ProgressStartedArgs : ProgressArgs
{

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    /// <param name="hive"></param>
    public ProgressStartedArgs(string message, string hive) : base(message)
    {
        Hive = hive;
    }






    /// <summary>
    /// </summary>
    public string Hive { get; set; }

}