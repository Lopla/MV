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
        /// Started one before <see cref="Start"/> is called. This method allows to setup this verse and
        /// comunicate with existing context.
        /// </summary>
        /// <param name="context">context to which this verse has landed</param>
        /// <returns></returns>
        Task Init(IMetaVerse context);
    }
}