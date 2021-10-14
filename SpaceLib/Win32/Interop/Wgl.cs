using System.Runtime.InteropServices;

namespace SpaceLib.Win32.Interop;

internal static class Wgl
{
    private const string MODULE_NAME = "Opengl32.dll";

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr wglCreateContext(IntPtr dc);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern void wglDeleteContext(IntPtr context);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr wglGetCurrentContext();
    
    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr wglGetCurrentDC();

    [PreserveSig, DllImport(MODULE_NAME, CharSet = CharSet.Ansi)]
    public static extern IntPtr wglGetProcAddress(string procName);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern bool wglMakeCurrent(IntPtr dc, IntPtr context);

}