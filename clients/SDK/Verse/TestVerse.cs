using MV.Forms;
using MV.Interfaces;
using MV.Models;

namespace SDK.Verse;

internal class TestVerse : IManifest
{
    public VerseDefinition Definition()
    {
        return new VerseDefinition();
    }

    public IVerse Verse()
    {
        return new Verse1d();
    }
}