// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: RegistryUtils.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Windows.RegistryEditor.Utils;
/// <summary>
///     Provides utility methods for interacting with the Windows Registry, including operations
///     such as exporting, deleting, and navigating registry hives.
/// </summary>
/// <remarks>
///     This class contains static methods and constants to facilitate registry management tasks.
///     It includes functionality for handling registry hives, shortcuts, and registry data types.
/// </remarks>
public static class RegistryUtils
{

    /// <summary>
    ///     Deletes the specified registry hive using the REG command-line utility.
    /// </summary>
    /// <param name="hivePath">The full path of the registry hive to delete.</param>
    public static void DeleteHive(string hivePath)
    {
        ProcessStartInfo procInfo = new()
        {
            FileName = "REG",
            Arguments = $"DELETE \"{hivePath}\" /f",
            CreateNoWindow = true,
            UseShellExecute = false
        };

        _ = Process.Start(procInfo);
    }






    /// <summary>
    ///     Exports the specified registry hive to a .reg file in the given directory.
    /// </summary>
    /// <param name="hivePath">The full path of the registry hive to export.</param>
    /// <param name="dstDir">The destination directory for the exported .reg file.</param>
    public static void ExportHive(string hivePath, string dstDir)
    {
        if (!Directory.Exists(dstDir))
        {
            _ = Directory.CreateDirectory(dstDir);
        }

        string fileName = Utility.ValidateDirOrFileName(hivePath.Split('\\').Last());
        string dstFile = Path.Combine(dstDir, fileName + ".reg");
        ProcessStartInfo procInfo = new()
        {
            FileName = "REG",
            Arguments = $"EXPORT \"{hivePath}\" \"{dstFile}\" /y",
            CreateNoWindow = true,
            UseShellExecute = false
        };

        _ = Process.Start(procInfo);
    }






    /// <summary>
    ///     Gets the full registry hive name for a given shortcut.
    /// </summary>
    /// <param name="hive">The shortcut name of the registry hive (e.g., "HKCU").</param>
    /// <returns>The full registry hive name (e.g., "HKEY_CURRENT_USER").</returns>
    /// <exception cref="ArgumentException">Thrown if the shortcut is invalid.</exception>
    public static string GetHiveFullName(string hive)
    {
        return hive switch
        {
            SHKCR => HKCR,
            SHKCU => HKCU,
            SHKLM => HKLM,
            SHKU => HKU,
            SHKCC => HKCC,
            _ => throw new ArgumentException($"[GetHiveFullName] - Invalid HKEY input -> {hive}")
        };
    }






    /// <summary>
    ///     Gets the shortcut name for a given full registry hive name.
    /// </summary>
    /// <param name="hive">The full registry hive name (e.g., "HKEY_CURRENT_USER").</param>
    /// <returns>The shortcut name (e.g., "HKCU").</returns>
    /// <exception cref="ArgumentException">Thrown if the hive name is invalid.</exception>
    public static string GetHiveShortcut(string hive)
    {
        return hive switch
        {
            HKCR => SHKCR,
            HKCU => SHKCU,
            HKLM => SHKLM,
            HKU => SHKU,
            HKCC => SHKCC,
            _ => throw new ArgumentException($"[GetHiveShortcut] - Invalid HKEY input -> {hive}")
        };
    }






    /// <summary>
    ///     Gets the full registry hive name for a remote registry path.
    /// </summary>
    /// <param name="hive">The remote registry path.</param>
    /// <returns>The full registry hive name.</returns>
    public static string GetRemoteHiveFullName(string hive)
    {
        return GetHiveFullName(hive.Split('\\').Last());
    }






    /// <summary>
    ///     Gets the shortcut name for a remote registry path.
    /// </summary>
    /// <param name="hive">The remote registry path.</param>
    /// <returns>The shortcut name.</returns>
    public static string GetRemoteShortcut(string hive)
    {
        return GetHiveShortcut(hive.Split('\\').Last());
    }






    /// <summary>
    ///     The full registry hive name for HKEY_CURRENT_CONFIG.
    /// </summary>
    public const string HKCC = "HKEY_CURRENT_CONFIG";

    /// <summary>
    ///     The full registry hive name for HKEY_CLASSES_ROOT.
    /// </summary>
    public const string HKCR = "HKEY_CLASSES_ROOT";

    /// <summary>
    ///     The full registry hive name for HKEY_CURRENT_USER.
    /// </summary>
    public const string HKCU = "HKEY_CURRENT_USER";

    /// <summary>
    ///     The registry path for Regedit settings in HKEY_CURRENT_USER.
    /// </summary>
    private const string HKEY_REGEDIT = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit";

    /// <summary>
    ///     The full registry hive name for HKEY_LOCAL_MACHINE.
    /// </summary>
    public const string HKLM = "HKEY_LOCAL_MACHINE";

    /// <summary>
    ///     The full registry hive name for HKEY_USERS.
    /// </summary>
    public const string HKU = "HKEY_USERS";






    /// <summary>
    ///     Opens the Registry Editor at the specified registry path.
    /// </summary>
    /// <param name="path">The registry path to open in Regedit.</param>
    public static void OpenRegistryLocation(string path)
    {
        string args = $"{HKEY_REGEDIT} /v LastKey /t REG_SZ /d \"{path}\" /f";
        ProcessStartInfo procInfo = new()
        {
            FileName = "REG",
            Arguments = $"ADD {args}",
            CreateNoWindow = true,
            UseShellExecute = false
        };

        _ = Process.Start(procInfo);

        _ = Utility.KillProcess("regedit");
        _ = Process.Start("regedit.exe");
    }






    /// <summary>
    ///     Registry value type: Binary data.
    /// </summary>
    public const string REG_BINARY = "REG_BINARY";

    /// <summary>
    ///     Registry value type: 32-bit number.
    /// </summary>
    public const string REG_DWORD = "REG_DWORD";

    /// <summary>
    ///     Registry value type: 32-bit number (big-endian).
    /// </summary>
    public const string REG_DWORD_BIG_ENDIAN = "REG_DWORD_BIG_ENDIAN";

    /// <summary>
    ///     Registry value type: 32-bit number (little-endian).
    /// </summary>
    public const string REG_DWORD_LITTLE_ENDIAN = "REG_DWORD_LITTLE_ENDIAN";

    /// <summary>
    ///     Registry value type: Expandable string value.
    /// </summary>
    public const string REG_EXPAND_SZ = "REG_EXPAND_SZ";

    /// <summary>
    ///     Registry value type: Symbolic link.
    /// </summary>
    public const string REG_LINK = "REG_LINK";

    /// <summary>
    ///     Registry value type: Multiple string values.
    /// </summary>
    public const string REG_MULTI_SZ = "REG_MULTI_SZ";

    /// <summary>
    ///     Registry value type: No defined value type.
    /// </summary>
    public const string REG_NONE = "REG_NONE";

    /// <summary>
    ///     Registry value type: 64-bit number.
    /// </summary>
    public const string REG_QWORD = "REG_QWORD";

    /// <summary>
    ///     Registry value type: 64-bit number (little-endian).
    /// </summary>
    public const string REG_QWORD_LITTLE_ENDIAN = "REG_QWORD_LITTLE_ENDIAN";

    /// <summary>
    ///     Registry value type: Resource list.
    /// </summary>
    public const string REG_RESOURCE_LIST = "REG_RESOURCE_LIST";

    /// <summary>
    ///     Registry value type: String value.
    /// </summary>
    public const string REG_SZ = "REG_SZ";

    /// <summary>
    ///     Registry hive shortcut for HKEY_CURRENT_CONFIG.
    /// </summary>
    public const string SHKCC = "HKCC";

    /// <summary>
    ///     Registry hive shortcut for HKEY_CLASSES_ROOT.
    /// </summary>
    public const string SHKCR = "HKCR";

    /// <summary>
    ///     Registry hive shortcut for HKEY_CURRENT_USER.
    /// </summary>
    public const string SHKCU = "HKCU";

    /// <summary>
    ///     Registry hive shortcut for HKEY_LOCAL_MACHINE.
    /// </summary>
    public const string SHKLM = "HKLM";

    /// <summary>
    ///     Registry hive shortcut for HKEY_USERS.
    /// </summary>
    public const string SHKU = "HKU";

}