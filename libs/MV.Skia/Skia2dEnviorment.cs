using System;
using MV.Interfaces;
using Pla.Lib;
using SkiaSharp;

namespace MV.Skia
{
    public class Skia2dEnviorment : I2dEnviorment, IPainter
    {
        public Action<SKImageInfo, SKSurface> Painter;

        public void Paint(SKImageInfo info, SKSurface surface)
        {
            Painter?.Invoke(info, surface);
        }
    }
}
