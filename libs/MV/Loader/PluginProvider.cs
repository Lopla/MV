using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace MV.Loader
{
    public class PluginProvider<T>
        where T : class
    {
        private readonly List<T> _plugins = new List<T>();

        public PluginProvider()
        {
            PluginAssemblyCache = new CustomDomain();

            Trace.WriteLine($@"Initializing plugin provider {typeof(T)}");
            if (Instance != null) throw new Exception("Not allowed - duplicated plugin provider initialization");
            Instance = this;
        }

        public static PluginProvider<T> Instance { get; set; }

        public CustomDomain PluginAssemblyCache { get; protected set; }

        public Loader GetLoader()
        {
            return new Loader(PluginAssemblyCache);
        }

        internal IEnumerable<string> GetPluginNames()
        {
            return _plugins.Select(e => PluginName(e));
        }

        public void AddPlugin(T logic)
        {
            _plugins.Add(logic);
        }

        public static string PluginName(T plugin)
        {
            return plugin.GetType().FullName;
        }

        public void AddAssembly(Assembly asm, bool replace = false)
        {
            var loader = GetLoader();

            var logicList = loader.GetTypes<T>(asm);
            logicList?.ToList().ForEach(AddPlugin);

            // types
            var exportedTypes = asm.ExportedTypes.Select(r => r.Name).ToArray();
        }

        public void Reset()
        {
            //PluginAssemblyCache.Unload();
        }
        
    }
}