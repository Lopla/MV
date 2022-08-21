using MV.Models;

namespace MV.Interfaces
{
    /// <summary>
    /// Manifest describes this world (<see cref="Verse()"/>) and informs about metadata
    /// </summary>
    public interface IManifest
    {
        VerseDefinition Definition();

        IVerse Verse();
    }
}