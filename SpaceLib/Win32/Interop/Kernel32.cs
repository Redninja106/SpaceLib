using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLib.Win32.Interop;

internal static unsafe class Kernel32
{
    private const string MODULE_NAME = "kernel32";

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr GetModuleHandleW(char* moduleName);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr LoadLibrary(string name);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern bool FreeLibrary(IntPtr module);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr GetProcAddress(IntPtr module, string name);
}