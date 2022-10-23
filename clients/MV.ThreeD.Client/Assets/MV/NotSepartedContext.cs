using System.IO;
using System.Reflection;
using MV.Loader;

internal class NotSepartedContext : IAssemblyContext
{
    public Assembly LoadFromAssemblyPath(string path)
    {
        return System.Reflection.Assembly.LoadFile(path);
    }

    public Assembly LoadAssemblyFromStream(MemoryStream memoryStream)
    {
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
     
    }
}