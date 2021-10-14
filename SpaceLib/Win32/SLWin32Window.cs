using SpaceLib.Win32.Interop;
using System.Runtime.InteropServices;

namespace SpaceLib.Win32;

internal unsafe sealed class SLWin32Window : ISLWindow
{
    private const string WINDOW_CLASS_NAME = "spacelib";
    private static readonly IntPtr hInstance = Kernel32.GetModuleHandleW(null);

    public bool CloseRequested { get; set; }
    public IntPtr NativePointer { get; private set; }

    private string title;

    public SLWin32Window(SLWindowDescription description)
    {
        this.title = description.Title;
        
        fixed (char* pTitle = title, pClassName = WINDOW_CLASS_NAME) 
        {
            NativePointer = User32.CreateWindowExW(
                0, 
                pClassName, 
                pTitle,
                User32.WindowStyles.OverlappedWindow | User32.WindowStyles.Visible, 
                User32.CW_USEDEFAULT, 
                User32.CW_USEDEFAULT, 
                description.Width, 
                description.Height, 
                IntPtr.Zero, 
                IntPtr.Zero, 
                hInstance, 
                IntPtr.Zero
                ); 

            if (NativePointer == IntPtr.Zero)
            {
                throw new Exception($"Window Creation Error! Win32 error: 0x{Marshal.GetLastWin32Error():x4}");
            }
        }
    }

    public bool ProcessEvents()
    {
        bool result = false;

        User32.MSG msg;
        while (User32.PeekMessage(&msg, this.NativePointer, 0, 0, User32.PeekMessageRemove.Remove))
        {
            User32.TranslateMessage(&msg);
            User32.DispatchMessage(&msg);
            result = true;
        }

        return result;
    }

    public void WaitEvents()
    {
    }

    public bool WaitEvents(long timeout)
    {
        return false;
    }


    public void Dispose()
    {
        User32.DestroyWindow(NativePointer);
        NativePointer = IntPtr.Zero;
    }

    public unsafe static void RegisterWin32Class()
    {
        fixed (char* pClassName = WINDOW_CLASS_NAME)
        {
            User32.WNDCLASSEX wndclass;
            
            wndclass = new User32.WNDCLASSEX
            {
                cbSize = (uint)sizeof(User32.WNDCLASSEX),
                hInstance = hInstance,
                lpfnWndProc = (IntPtr)(delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, IntPtr>)&WndProc,
                lpszClassName = pClassName,
            };

            var atom = User32.RegisterClassExW(&wndclass);
        
            if (atom == 0)
            {
                throw new Exception("Unable to register Win32 window class!");
            }
        }
    }

    [UnmanagedCallersOnly]
    public static IntPtr WndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        return User32.DefWindowProcW(hwnd, msg, wParam, lParam);
    }

    public static void UnregisterWin32Class()
    {
        fixed (char* pClassName = WINDOW_CLASS_NAME) 
        {
            User32.UnregisterClassW(pClassName, Kernel32.GetModuleHandleW(null));
        }
    }
}