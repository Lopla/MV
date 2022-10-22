using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;

namespace MV.Loader
{
    public class CustomDomain : AssemblyLoadContext
    {
        public CustomDomain() : base()
        {

        }

        protected override Assembly Load(AssemblyName name)
        {
            //// ignore all deps
            Trace.Write($"Request for {name}");
            return null;
        }

        public void Reset()
        {
            
        }
    }
}