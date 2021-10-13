namespace SpaceLib;

public interface INativeObject : IDisposable
{
    IntPtr NativePointer { get; }
    
    bool IsDisposed => NativePointer == IntPtr.Zero;
}