// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: ProgressChangedArgs.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




namespace Windows.RegistryEditor.Events;


/// <summary>
///     Represents the arguments for the progress changed event in the registry editor.
/// </summary>
/// <remarks>
///     This class provides additional information about the progress of operations
///     performed on a specific registry hive. It extends the <see cref="ProgressArgs" /> class
///     to include details about the registry hive being processed.
/// </remarks>
public class ProgressChangedArgs : ProgressArgs
{

    /// <summary>
    ///     Initializes a new instance of the <see cref="ProgressChangedArgs" /> class with the specified message and registry
    ///     hive.
    /// </summary>
    /// <param name="message">The message describing the progress of the operation.</param>
    /// <param name="hive">The registry hive being processed.</param>
    public ProgressChangedArgs(string message, string hive) : base(message)
    {
        Hive = hive;
    }






    /// <summary>
    ///     Gets or sets the name of the registry hive being processed.
    /// </summary>
    /// <value>
    ///     A <see cref="string" /> representing the name of the registry hive.
    /// </value>
    /// <remarks>
    ///     This property provides information about the specific registry hive
    ///     involved in the operation, such as "HKEY_LOCAL_MACHINE" or "HKEY_CURRENT_USER".
    /// </remarks>
    public string Hive { get; set; }

}