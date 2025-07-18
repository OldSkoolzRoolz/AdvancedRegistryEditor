﻿// Project Name: RegistryEditor
// File Name: RegistryUtils.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




#region

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

#endregion



namespace Windows.RegistryEditor.Utils;


public static class RegistryUtils
{

    public static void DeleteHive(string hivePath)
    {
        var procInfo = new ProcessStartInfo
        {
                    FileName = "REG",
                    Arguments = $"DELETE \"{hivePath}\" /f",
                    CreateNoWindow = true,
                    UseShellExecute = false
        };

        Process.Start(procInfo);
    }






    public static void ExportHive(string hivePath, string dstDir)
    {
        if (!Directory.Exists(dstDir))
            Directory.CreateDirectory(dstDir);

        var fileName = Utility.ValidateDirOrFileName(hivePath.Split('\\').Last());
        var dstFile = Path.Combine(dstDir, fileName + ".reg");
        var procInfo = new ProcessStartInfo
        {
                    FileName = "REG",
                    Arguments = $"EXPORT \"{hivePath}\" \"{dstFile}\" /y",
                    CreateNoWindow = true,
                    UseShellExecute = false
        };

        Process.Start(procInfo);
    }






    public static string GetHiveFullName(string hive)
    {
        switch (hive)
        {
            case SHKCR: return HKCR;

            case SHKCU: return HKCU;

            case SHKLM: return HKLM;

            case SHKU: return HKU;

            case SHKCC: return HKCC;

            default:

                throw new ArgumentException($"[GetHiveFullName] - Invalid HKEY input -> {hive}");
        }
    }






    public static string GetHiveShortcut(string hive)
    {
        switch (hive)
        {
            case HKCR: return SHKCR;

            case HKCU: return SHKCU;

            case HKLM: return SHKLM;

            case HKU: return SHKU;

            case HKCC: return SHKCC;

            default:

                throw new ArgumentException($"[GetHiveShortcut] - Invalid HKEY input -> {hive}");
        }
    }






    public static string GetRemoteHiveFullName(string hive)
    {
        return GetHiveFullName(hive.Split('\\').Last());
    }






    public static string GetRemoteShortcut(string hive)
    {
        return GetHiveShortcut(hive.Split('\\').Last());
    }






    public const string HKCC = "HKEY_CURRENT_CONFIG";
    public const string HKCR = "HKEY_CLASSES_ROOT";
    public const string HKCU = "HKEY_CURRENT_USER";

    private const string HKEY_REGEDIT = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit";
    public const string HKLM = "HKEY_LOCAL_MACHINE";
    public const string HKU = "HKEY_USERS";






    public static void OpenRegistryLocation(string path)
    {
        var args = $"{HKEY_REGEDIT} /v LastKey /t REG_SZ /d \"{path}\" /f";
        var procInfo = new ProcessStartInfo
        {
                    FileName = "REG",
                    Arguments = $"ADD {args}",
                    CreateNoWindow = true,
                    UseShellExecute = false
        };

        Process.Start(procInfo);

        Utility.KillProcess("regedit");
        Process.Start("regedit.exe");
    }






    public const string REG_BINARY = "REG_BINARY";
    public const string REG_DWORD = "REG_DWORD";
    public const string REG_DWORD_BIG_ENDIAN = "REG_DWORD_BIG_ENDIAN";
    public const string REG_DWORD_LITTLE_ENDIAN = "REG_DWORD_LITTLE_ENDIAN";
    public const string REG_EXPAND_SZ = "REG_EXPAND_SZ";
    public const string REG_LINK = "REG_LINK";
    public const string REG_MULTI_SZ = "REG_MULTI_SZ";
    public const string REG_NONE = "REG_NONE";
    public const string REG_QWORD = "REG_QWORD";
    public const string REG_QWORD_LITTLE_ENDIAN = "REG_QWORD_LITTLE_ENDIAN";
    public const string REG_RESOURCE_LIST = "REG_RESOURCE_LIST";

    public const string REG_SZ = "REG_SZ";
    public const string SHKCC = "HKCC";

    public const string SHKCR = "HKCR";
    public const string SHKCU = "HKCU";
    public const string SHKLM = "HKLM";
    public const string SHKU = "HKU";

}