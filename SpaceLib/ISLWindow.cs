using System.Runtime.InteropServices;

namespace SpaceLib;

public interface ISLWindow : INativeObject
{
    bool CloseRequested { get; }
    bool ProcessEvents();
    void WaitEvents();
    bool WaitEvents(long timeout);
}
