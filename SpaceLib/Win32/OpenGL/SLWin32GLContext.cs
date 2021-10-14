using SpaceLib.OpenGL;
using SpaceLib.Win32.Interop;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SpaceLib.Win32.OpenGL;

internal class SLWin32GLContext : ISLGLContext
{
    private readonly ISLWindow window;

    public IntPtr NativePointer { get; private set; }
    public bool IsCurrent => Wgl.wglGetCurrentContext() == NativePointer;

    private static readonly Dictionary<IntPtr, SLWin32GLContext> activeContexts = new();
    private static IntPtr opengl;

    public SLWin32GLContext(ISLWindow window)
    {
        this.window = window;

        var dc = User32.GetWindowDC(this.window.NativePointer);
        Gdi32.PIXELFORMATDESCRIPTOR desc;
        
        unsafe
        {
            desc = new Gdi32.PIXELFORMATDESCRIPTOR
            {
                Size = (ushort)sizeof(Gdi32.PIXELFORMATDESCRIPTOR),
                Version = 1,
                Flags = Gdi32.PixelFormatDescriptorFlags.DoubleBuffer | Gdi32.PixelFormatDescriptorFlags.SupportOpenGL | Gdi32.PixelFormatDescriptorFlags.DrawToWindow,
                PixelType = 0,
                ColorBits = 24,
                AlphaBits = 8,
                AccumBits = 0,
                DepthBits = 32,
                StencilBits = 0,
                AuxBuffers = 0,
            };
        }

        var format = Gdi32.ChoosePixelFormat(dc, ref desc);
        if (!Gdi32.SetPixelFormat(dc, format, ref desc))
        {
            throw new Exception("unable to set pixel fmt");
        }

        this.NativePointer = Wgl.wglCreateContext(User32.GetWindowDC(window.NativePointer));
        
        if (this.NativePointer == IntPtr.Zero)
        {
            throw new Exception($"Error creating OpenGL context! Windows last error: 0x{Marshal.GetLastWin32Error():x4}");
        }
        
        activeContexts.Add(this.NativePointer, this);
    }

    public unsafe IntPtr GetProcAddress(string procName)
    {
        ISLGLContext oldCxt = null;
        if (!this.IsCurrent)
        {
            oldCxt = GetCurrent();
            MakeCurrent();
        }
            
        var result = Wgl.wglGetProcAddress(procName);

        if (result == IntPtr.Zero)
        {
            result = Kernel32.GetProcAddress(opengl, procName);
        }

        if (oldCxt != null)
        {
            oldCxt.MakeCurrent();
        }

        return result;
    }

    public bool MakeCurrent()
    {
        if (window.IsDisposed)
            return false;

        return Wgl.wglMakeCurrent(User32.GetWindowDC(window.NativePointer), this.NativePointer);
    }

    public bool SwapBuffers()
    {
        return Gdi32.SwapBuffers(User32.GetWindowDC(this.window.NativePointer));
    }

    public void Dispose()
    {
        Wgl.wglDeleteContext(this.NativePointer);
        activeContexts.Remove(this.NativePointer);
        this.NativePointer = IntPtr.Zero;
    }

    public static SLWin32GLContext GetCurrent()
    {
        return activeContexts[Wgl.wglGetCurrentContext()];
    }

    public static bool RemoveCurrent()
    {
        return Wgl.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
    }

    public static void LoadDependencies()
    {
        opengl = Kernel32.LoadLibrary("opengl32.dll");

        if (opengl == IntPtr.Zero)
            throw new Exception();
    }

    public static void FreeDependencies()
    {
        Kernel32.FreeLibrary(opengl);
    }
}