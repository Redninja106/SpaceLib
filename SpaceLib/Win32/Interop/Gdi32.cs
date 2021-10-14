using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLib.Win32.Interop;

internal static class Gdi32
{
    private const string MODULE_NAME = "gdi32.dll";

    public struct PIXELFORMATDESCRIPTOR
    {
        public ushort Size;
        public ushort Version;
        public PixelFormatDescriptorFlags Flags;
        public byte PixelType;
        public byte ColorBits;
        public byte RedBits;
        public byte RedShift;
        public byte GreenBits;
        public byte GreenShift;
        public byte BlueBits;
        public byte BlueShift;
        public byte AlphaBits;
        public byte AlphaShift;
        public byte AccumBits;
        public byte AccumRedBits;
        public byte AccumGreenBits;
        public byte AccumBlueBits;
        public byte AccumAlphaBits;
        public byte DepthBits;
        public byte StencilBits;
        public byte AuxBuffers;
        public byte LayerType;
        private byte Reserved;
        public int LayerMask;
        public int VisibleMask;
        public int DamageMask;
    }

    public enum PixelFormatDescriptorFlags : uint
    {
        DrawToWindow = 0x4,
        DrawToBitmap = 0x8,
        SupportGdi = 0x10,
        SupportOpenGL = 0x20,
        GenericAccelerated = 0x1000,
        GenericFormat = 0x40,
        NeedPalette = 0x80,
        NeedSystemPalette = 0x100,
        DoubleBuffer = 0x1,
        Stereo = 0x2,
        SwapLayerBuffers = 0x800
    }


    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern int GetPixelFormat(IntPtr dc);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern int ChoosePixelFormat(IntPtr dc, ref PIXELFORMATDESCRIPTOR formatDesc);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern bool SetPixelFormat(IntPtr dc, int format, ref PIXELFORMATDESCRIPTOR formatDesc);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern int DescribePixelFormat(IntPtr dc, int pixelFormat, uint bytes, ref PIXELFORMATDESCRIPTOR formatDesc);

    [PreserveSig, DllImport(MODULE_NAME)]
    public static extern bool SwapBuffers(IntPtr dc);

}