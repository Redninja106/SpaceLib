using System.Runtime.InteropServices;

namespace SpaceLib;

public interface ISLWindow : ISLNativeObject
{

    /// <summary>
    /// Indicates if the window is visible to the user.
    /// </summary>
    bool IsVisble();

    /// <summary>
    /// Indicates if the window's content is visible to the user. This is typically used to determine if a new frame should be rendered.
    /// </summary>
    bool IsContentVisible();

    /// <summary>
    /// Sets if the window should be visible on the user's desktop. 
    /// </summary>
    /// <param name="visible"></param>
    /// <returns></returns>
    bool SetVisible(bool visible);

    // window state:

    /// <summary>
    /// Indicates if the application should destroy the window at it's next opportunity.
    /// </summary>
    /// <returns>A value indicating if the window should be destroyed./returns>
    bool IsCloseRequested();

    /// <summary>
    /// Sends a close request to the window. Listeners of the <see cref="Closed"/> event can call <see cref="CancelCloseRequest"/> to cancel this.
    /// </summary>
    void RequestClose();

    /// <summary>
    /// Cancels a request to close the window made by <see cref="RequestClose"/>.
    /// </summary>
    void CancelCloseRequest();

    // fullscreen:

    /// <summary>
    /// Configures a window to fill a monitor using it's native video mode.
    /// </summary>
    /// <param name="monitor"></param>
    /// <returns></returns>
    bool EnterFullscreen(ISLMonitor monitor);
    /// <summary>
    /// Exits from fullscreen mode if the window is in it.
    /// </summary>
    void ExitFullscreen();

    /// <summary>
    /// Gets a value that indicates if the window is in fullscreen.
    /// </summary>
    bool GetFullscreenState(out ISLMonitor monitor);
    
    // event processing:

    /// <summary>
    /// Processes all waiting events in the window's queue.
    /// </summary>
    /// <returns>A value indicating if any events were processed.</returns>
    bool ProcessEvents();
    /// <summary>
    /// Waits until the window has an event to process.
    /// </summary>
    void WaitEvents();
    /// <summary>
    /// Waits until the window has an event to process or a timeout expires.
    /// </summary>
    /// <param name="timeout">The number of milliseconds before the method times out.</param>
    /// <returns><see langword="true"/> if the window received an event, <see langword="false"/> if the method timed outccccc.</returns>
    bool WaitEvents(long timeout);


}
