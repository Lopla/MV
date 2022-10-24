using System.Threading.Tasks;

namespace MV.Interfaces
{
    public interface IMetaVerseRunner : IMetaVerseContext
    {
        /// <summary>
        /// Started only once
        /// </summary>
        Task Start();

        Task Init();

        Task InitVerse(IVerse verse);
    }
}