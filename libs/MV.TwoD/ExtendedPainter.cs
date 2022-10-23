using Pla.Lib;
using Pla.Lib.UI;
using SkiaSharp;

namespace MV.TwoD;

public class ExtendedPainter : IPainter
{
    private readonly Manager _manager;

    public ExtendedPainter(Manager manager)
    {
        _manager = manager;
    }

    public void Paint(SKImageInfo info, SKSurface surface)
    {
        _manager.Paint(info, surface);
    }
}