using SpaceLib.OpenGL;
using System.Runtime.InteropServices;

namespace SpaceLib;

public abstract class SLFactory : IDisposable
{
    public SLFactory()
    {
    }

    public abstract ISLWindow CreateWindow(SLWindowDescription description);
    public abstract ISLGLContext CreateGLContext(ISLWindow window);

    public abstract ISLGLContext GetCurrentGLContext();
    public abstract void RemoveCurrentGLContext();

    public abstract ISLMonitor[] GetMonitors();

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Creates an instance of the default <see cref="SLFactory"/> implementation for the current platform.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public static SLFactory Create()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return new Win32.SLWin32Factory();
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            throw new NotImplementedException();
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            throw new NotImplementedException();
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            throw new NotImplementedException();
        }

        throw new NotSupportedException();
    }
}