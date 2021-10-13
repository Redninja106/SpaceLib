using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLib;

public record SLWindowDescription(string Title, int Width = 1280, int Height = 720)
{
    public int X { get; set; } = -1;
    public int Y { get; set; } = -1;
}