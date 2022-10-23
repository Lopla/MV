using System.Threading.Tasks;

namespace MV.Interfaces
{
    public interface IMetaVerse : IMetaVerseContext
    {
        /// <summary>
        /// Started only once
        /// </summary>
        Task Start();

        Task Init();

        Task InitVerse(IVerse verse);
    }
}