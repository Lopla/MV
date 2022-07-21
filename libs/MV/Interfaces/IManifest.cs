using MV.Models;

namespace MV.Interfaces;

public interface IManifest
{
    VerseDefinition Definition();

    IVerse Verse();
}
