using SpaceLib;
using SpaceLib.OpenGL;
using System.Runtime.InteropServices;

SLFactory factory = SLFactory.Create();

SLWindowDescription desc = new("SpaceLib Example");

ISLWindow window = factory.CreateWindow(desc);

ISLGLContext context = factory.CreateGLContext(window);

context.MakeCurrent();

var clearColor = Marshal.GetDelegateForFunctionPointer<GlClearColorProc>(context.GetProcAddress("glClearColor"));
var clear = Marshal.GetDelegateForFunctionPointer<GlClearProc>(context.GetProcAddress("glClear"));

while (!window.CloseRequested)
{
    window.ProcessEvents();
    
    clearColor(1, 0, 0, 1); // set clear color to red
    clear(0x00004000); // clear color buffer
    
    context.SwapBuffers();
}

context.Dispose();
window.Dispose();

delegate void GlClearColorProc(float red, float green, float blue, float alpha);
delegate void GlClearProc(uint mask);
