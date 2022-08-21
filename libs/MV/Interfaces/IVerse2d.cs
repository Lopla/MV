using System.Threading.Tasks;

namespace MV.Interfaces
{
    public interface IVerse2d<T> : IVerse
        where T : I2dEnviorment
    {
        Task InitEngine(T env);
    }
}