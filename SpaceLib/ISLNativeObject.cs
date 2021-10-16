namespace SpaceLib;

public interface ISLNativeObject : IDisposable
{
    IntPtr NativePointer { get; }
    
    bool IsDisposed => NativePointer == IntPtr.Zero;
}