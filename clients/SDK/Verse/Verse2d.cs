using MV.Forms;
using MV.Interfaces;
using MV.Skia;
using SkiaSharp;

namespace SDK.Verse;

public class Verse2dSkia : IVerse2d<Skia2dEnviorment>
{
    private IMetaVerseRunner? _context = null!;

    public Task InitEngine(Skia2dEnviorment env)
    {
        env.Painter = this.PaintVerse;

        return Task.CompletedTask;
    }

    private void PaintVerse(SKImageInfo imageInfo, SKSurface surface)
    {
        using SKPaint paint = new SKPaint()
        {
            Color = new SKColor(0,0,0)
        };
        surface.Canvas.DrawLine(0,0, 1024,1024, paint);
    }

    public Task Start()
    {
        _context.Show(new Label("1d 2d who cares"));
        return Task.CompletedTask;
    }

    public Task Init(IMetaVerseRunner? context)
    {
        _context = context;
        return Task.CompletedTask;
    }
}