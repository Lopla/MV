using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace MV.Loader
{
    public class SeparatedDomainContext : AssemblyLoadContext, IAssemblyContext
    {
        public SeparatedDomainContext() : base()
        {

        }

        protected override Assembly Load(AssemblyName name)
        {
            //// ignore all deps
            Trace.Write($"Request for {name}");
            return null;
        }

        public Assembly LoadAssemblyFromStream(MemoryStream memoryStream)
        {
            return this.LoadFromStream(memoryStream);
        }

        public void Reset()
        {
            
        }
    }
}