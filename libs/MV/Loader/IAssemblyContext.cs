using System.IO;
using System.Reflection;

namespace MV.Loader
{
    public interface IAssemblyContext
    {
        Assembly LoadFromAssemblyPath(string path);
        Assembly LoadAssemblyFromStream(MemoryStream memoryStream);
        void Reset();
    }
}