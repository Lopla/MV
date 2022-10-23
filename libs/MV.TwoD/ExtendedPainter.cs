using MV.Skia;
using Pla.Lib;
using Pla.Lib.UI;
using SkiaSharp;

namespace MV.TwoD;

public class ExtendedPainter : IPainter
{
    private readonly Manager _manager;
    public Skia2dEnviorment Environment = new();

    public ExtendedPainter(Manager manager)
    {
        _manager = manager;
    }

    public void Paint(SKImageInfo info, SKSurface surface)
    {
        // first paint verse
        Environment.Painter?.Invoke(info, surface);
        // then on top paint UI
        _manager.Paint(info, surface);
    }
}