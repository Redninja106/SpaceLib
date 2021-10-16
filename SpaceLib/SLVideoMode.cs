using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLib;

public record SLVideoMode
{
    public int Width { get; init; }
    public int Height { get; init; }
    public float RefreshRate { get; init; }
}