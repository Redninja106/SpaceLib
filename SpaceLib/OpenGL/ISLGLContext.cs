using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLib.OpenGL;

public interface ISLGLContext : ISLNativeObject
{
    public bool IsCurrent { get; }
    public bool SwapBuffers();
    public bool MakeCurrent();
    public IntPtr GetProcAddress(string procName);
}