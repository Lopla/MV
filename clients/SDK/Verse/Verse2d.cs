using MV.Forms;
using MV.Interfaces;
using MV.Skia;

namespace SDK.Verse;

public class Verse2d : IVerse2d<Skia2dEnviorment>
{
    private IMetaVerse _context = null!;

    public Task InitEngine(Skia2dEnviorment env)
    {
        return Task.CompletedTask;
    }

    public Task Start()
    {
        _context.Show(new Label("1d 2d who cares"));
        return Task.CompletedTask;
    }

    public Task Init(IMetaVerse context)
    {
        _context = context;
        return Task.CompletedTask;
    }
}