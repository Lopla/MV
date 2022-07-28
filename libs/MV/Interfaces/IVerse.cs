using System.Threading.Tasks;

namespace MV.Interfaces
{
    public interface IVerse
    {
        /// <summary>
        /// Started only once
        /// </summary>
        /// <returns></returns>
        Task Start();

        /// <summary>
        /// Used in real time scenarios
        /// </summary>
        /// <returns></returns>
        Task Loop();

        Task Init(IMetaVerse context);
    }
}