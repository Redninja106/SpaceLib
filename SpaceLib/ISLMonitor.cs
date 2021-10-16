using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLib;

public interface ISLMonitor
{
    string Name { get; }
    SLVideoMode NativeMode { get; }

    bool IsVideoModeSupported(SLVideoMode videoMode);
}