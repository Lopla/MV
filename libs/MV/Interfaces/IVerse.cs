using System.Threading.Tasks;

namespace MV.Interfaces
{
    /// <summary>
    /// Base interface for all verses (worlds, and others)
    /// </summary>
    public interface IVerse
    {
        /// <summary>
        /// Started only once
        /// </summary>
        Task Start();

        /// <summary>
        /// Started once before <see cref="Start"/> is called. This method allows to setup this verse and
        /// communicate with existing context.
        /// </summary>
        /// <param name="context">context to which this verse has loaded</param>
        /// <returns></returns>
        Task Init(IMetaVerseRunner context);
    }
}