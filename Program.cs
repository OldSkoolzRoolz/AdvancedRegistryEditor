// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: Program.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;
using System.Windows.Forms;

using Windows.RegistryEditor.Views;

namespace Windows.RegistryEditor;
internal static class Program
{

    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainWindow());
    }

}