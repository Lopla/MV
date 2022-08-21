using System.Threading.Tasks;

namespace MV.Interfaces
{
    public interface IVerse
    {
        /// <summary>
        /// Started only once
        /// </summary>
        Task Start();

        Task Init(IMetaVerse context);
    }
}