using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SpaceLib.OpenGL;
using SpaceLib.Win32.Interop;
using SpaceLib.Win32.OpenGL;

namespace SpaceLib.Win32;

internal sealed class SLWin32Factory : SLFactory
{
    public SLWin32Factory()
    {
        SLWin32Window.RegisterWin32Class();
        SLWin32GLContext.LoadDependencies();
    }

    public override ISLGLContext CreateGLContext(ISLWindow window)
    {
        return new SLWin32GLContext(window); 
    }

    public override ISLWindow CreateWindow(SLWindowDescription description)
    {
        return new SLWin32Window(description);
    }

    public override void Dispose()
    {
        SLWin32GLContext.FreeDependencies();
        SLWin32Window.UnregisterWin32Class();
        base.Dispose();
    }

    public override ISLGLContext GetCurrentGLContext()
    {
        return SLWin32GLContext.GetCurrent();
    }

    public override void RemoveCurrentGLContext()
    {
        SLWin32GLContext.RemoveCurrent();
    }
}