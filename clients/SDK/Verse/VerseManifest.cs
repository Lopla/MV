using MV.Interfaces;
using MV.Models;

namespace SDK.Verse;

internal class VerseManifest : IManifest
{
    public VerseDefinition Definition()
    {
        return new VerseDefinition()
        {
            Name = "Simple worlds"
        };
    }

    public IVerse Verse()
    {
        return new Verse2dSkia();
    }
}