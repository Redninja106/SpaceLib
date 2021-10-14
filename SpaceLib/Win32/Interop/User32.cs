using System.Drawing;
using System.Runtime.InteropServices;

namespace SpaceLib.Win32.Interop;

internal static unsafe class User32
{
    private const string MODULE_NAME = "user32.dll";

    public const int CW_USEDEFAULT = unchecked((int)0x80000000);

    public enum WindowStyles : uint
    {
        Caption     = 0x00C00000,
        SysMenu     = 0x00080000,
        MaximizeBox = 0x00010000,
        MinimizeBox = 0x00020000,
        ThickFrame  = 0x00040000,
        Overlapped  = 0x00000000,
        OverlappedWindow = (Overlapped | Caption | SysMenu | ThickFrame | MinimizeBox | MaximizeBox),
        Visible     = 0x10000000,
    }

    public enum PeekMessageRemove
    {
        NoRemove,
        Remove,
        NoYield
    }

    public struct WNDCLASSEX
    {
        public uint cbSize;
        public uint style;
        public IntPtr lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public char* lpszMenuName;
        public char* lpszClassName;
        public IntPtr hIconSm;
    }
    
    public struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public int time;
        public Point pt;
        public int lPrivate;
    }

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern short RegisterClassExW(WNDCLASSEX* wndclass);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern short UnregisterClassW(char* className, IntPtr hInstance);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr CreateWindowExW(uint exStyle, char* className, char* windowName, WindowStyles style, int x, int y, int width, int height, IntPtr parent, IntPtr menu, IntPtr hInstance, IntPtr param);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern void DestroyWindow(IntPtr hWnd);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr WindowFromDC(IntPtr dc);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr GetWindowDC(IntPtr hWnd);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern bool PeekMessage(MSG* msg, IntPtr hwnd, uint msgFilterMin, uint msgFilterMax, PeekMessageRemove removeMsg);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern bool TranslateMessage(MSG* msg);
    
    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern int DispatchMessage(MSG* msg);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern IntPtr DefWindowProcW(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);
}