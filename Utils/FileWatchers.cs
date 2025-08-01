// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: FileWatchers.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Windows.RegistryEditor.Utils;
internal class FileWatchers : IDisposable
{

    private readonly ConcurrentDictionary<string, Action<FileChangeInfo>> fileActions;

    // Holds watcher per directory, and maps filename to its action
    private readonly ConcurrentDictionary<string, FileSystemWatcher> watchers;






    /// <summary>
    ///     Initializes a new instance of the <see cref="FileWatchers" /> class.
    /// </summary>
    /// <remarks>
    ///     This constructor sets up the internal data structures required to manage file watchers
    ///     and their associated actions. It initializes dictionaries to track file actions and
    ///     <see cref="FileSystemWatcher" /> instances for monitoring file changes.
    /// </remarks>
    public FileWatchers()
    {
        watchers = new ConcurrentDictionary<string, FileSystemWatcher>(StringComparer.OrdinalIgnoreCase);
        fileActions = new ConcurrentDictionary<string, Action<FileChangeInfo>>(StringComparer.OrdinalIgnoreCase);
    }






    /// <summary>
    ///     Releases all resources used by the <see cref="FileWatchers" /> instance.
    /// </summary>
    /// <remarks>
    ///     This method disables and disposes all <see cref="FileSystemWatcher" /> instances managed by this class
    ///     and clears all associated file actions and watchers.
    /// </remarks>
    public void Dispose()
    {
        foreach (FileSystemWatcher watcher in watchers.Values)
        {
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
        }

        watchers.Clear();
        fileActions.Clear();
    }






    /// <summary>
    ///     Registers a file to be watched and assigns an action for its events.
    /// </summary>
    /// <param name="path">Full file path to monitor.</param>
    /// <param name="action">Action to invoke when the file changes.</param>
    public void AddFileWatcher(string path, Action<FileChangeInfo> action)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path cannot be null or whitespace.", nameof(path));
        }

        string directory = Path.GetDirectoryName(path);
        string filename = Path.GetFileName(path);

        if (directory == null || filename == null)
        {
            throw new ArgumentException("Invalid path format.", nameof(path));
        }

        // Add or get watcher for the directory
        FileSystemWatcher watcher = watchers.GetOrAdd(directory, dir =>
        {
            FileSystemWatcher fsw = new(dir)
            {
                EnableRaisingEvents = true,
                IncludeSubdirectories = false,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size
            };
            fsw.Changed += OnChanged;
            fsw.Created += OnChanged;
            fsw.Deleted += OnChanged;
            fsw.Renamed += OnRenamed;

            return fsw;
        });

        // Map filename to its action
        fileActions[path] = action ?? throw new ArgumentNullException(nameof(action));
    }






    /// <summary>
    ///     Handles the <see cref="FileSystemWatcher.Changed" />, <see cref="FileSystemWatcher.Created" />,
    ///     and <see cref="FileSystemWatcher.Deleted" /> events, triggered when a file or directory is modified, created, or
    ///     deleted.
    /// </summary>
    /// <param name="sender">
    ///     The source of the event, typically the <see cref="FileSystemWatcher" /> instance.
    /// </param>
    /// <param name="e">
    ///     A <see cref="FileSystemEventArgs" /> object containing information about the file or directory change,
    ///     including the full path and the type of change.
    /// </param>
    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        string fullPath = e.FullPath;

        if (fileActions.TryGetValue(fullPath, out Action<FileChangeInfo> action))
        {
            FileChangeInfo info = new()
            {
                Path = fullPath,
                ChangeType = e.ChangeType,
                OldPath = null
            };
            _ = Task.Run(() => action(info));
        }
    }






    /// <summary>
    ///     Handles the <see cref="FileSystemWatcher.Renamed" /> event, triggered when a file or directory is renamed.
    /// </summary>
    /// <param name="sender">
    ///     The source of the event, typically the <see cref="FileSystemWatcher" /> instance.
    /// </param>
    /// <param name="e">
    ///     A <see cref="RenamedEventArgs" /> object containing information about the renamed file or directory,
    ///     including the old and new paths.
    /// </param>
    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        string fullPath = e.FullPath;

        if (fileActions.TryGetValue(fullPath, out Action<FileChangeInfo> action))
        {
            FileChangeInfo info = new()
            {
                Path = fullPath,
                ChangeType = WatcherChangeTypes.Renamed,
                OldPath = e.OldFullPath
            };
            _ = Task.Run(() => action(info));
        }
    }






    /// <summary>
    ///     Removes a file watcher and its associated action.
    /// </summary>
    public void RemoveFileWatcher(string path)
    {
        _ = fileActions.TryRemove(path, out _);

        // Optionally remove watcher if no files in directory are being watched
        string directory = Path.GetDirectoryName(path);

        if (directory != null && !fileActions.Keys.Any(p => Path.GetDirectoryName(p).Equals(directory, StringComparison.OrdinalIgnoreCase)))
        {
            if (watchers.TryRemove(directory, out FileSystemWatcher watcher))
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
        }
    }

}


/// <summary>
///     Represents information about a file change event.
/// </summary>
public class FileChangeInfo
{

    /// <summary>
    ///     Gets or sets the full path of the file that changed.
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the type of change that occurred.
    /// </summary>
    public WatcherChangeTypes ChangeType { get; set; }

    /// <summary>
    ///     Gets or sets the old path of the file, if applicable (e.g., for rename events).
    /// </summary>
    public string OldPath { get; set; }

}