// Project Name: RegistryEditor
// File Name: NativeMethods.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




#region

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

#endregion



namespace Windows.RegistryEditor.Utils.Native;


[ComVisible(false)]
[SuppressUnmanagedCodeSecurity]
internal sealed class NativeMethods
{

    private const int LVM_FIRST = 0x1000;
    private const int LVM_SETITEMSTATE = LVM_FIRST + 43;






    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref NativeListViewItem lvi);






    internal static void SetItemState(ListView listView, int itemIndex, int mask, int value)
    {
        var lvItem = new NativeListViewItem { stateMask = mask, state = value };
        SendMessage(listView.Handle, LVM_SETITEMSTATE, itemIndex, ref lvItem);
    }

}