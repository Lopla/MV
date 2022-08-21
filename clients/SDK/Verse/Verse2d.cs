using MV.Forms;
using MV.Interfaces;
using MV.Skia;

namespace SDK.Verse;

public class Verse2d : IVerse2d<Skia2dEnviorment>
{
    private IMetaVerse _context;

    public async Task InitEngine(Skia2dEnviorment env)
    {
    }

    public async Task Start()
    {
        _context.Show(new Label("1d 2d who cares"));
    }

    public async Task Init(IMetaVerse context)
    {
        _context = context;
    }
}