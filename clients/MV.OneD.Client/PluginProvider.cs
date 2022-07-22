// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Reflection;
// using System.Runtime.Loader;
// using Revuo.Home.Abstractions;
// using Revuo.Home.Logic.Components;

// namespace Revuo.Home.Logic
// {
//     public class PluginProvider
//     {
//         private readonly List<(ILogic logic, MetaData meta)> _plugins 
//             =new List<(ILogic logic, MetaData meta)>();

//         public static PluginProvider Instance { get; set; }
//         public CustomDomain PluginAssemblyCache { get; protected set; }

//         public PluginProvider(bool loadBinaries = false)
//         {
//             PluginAssemblyCache = new CustomDomain();


//             Trace.WriteLine("Initializing plugin provider");
//             if (Instance != null)
//             {
//                 throw new Exception("Not allowed - duplicated plugin provider initialization");
//             }
//             Instance = this;

//             AddAssembly(typeof(ReadDLL).Assembly);
            
//             if (loadBinaries)
//             {
//                 Trace.WriteLine("Loading binaries");
//                 foreach (var c in Binaries.Code)
//                 {
//                     foreach(var lo in (GetLoader()).Load(c)){
//                         AddPlugin(lo.logic, lo.meta);
//                     }
//                 }
//             }
//         }

//         public Loader  GetLoader()
//         {
//             return new Loader(this.PluginAssemblyCache);
//         }

//         internal IEnumerable<string> GetPluginNames()
//         {
//             return this._plugins.Select(e=>PluginName(e.logic));
//         }

//         private void AddPlugin(ILogic logic, MetaData metaData)
//         {
//             _plugins.Add((logic, metaData));
//         }

//         public void AddPlugin(ILogic logic)
//         {
//             _plugins.Add((logic,  GetMetaInfo(logic)));
//         }

//         public (ILogic logic, MetaData meta) GetPlugin(string plugin)
//         {
//             foreach (var p in _plugins)
//             {
//                 var name = PluginName(p.logic);
//                 if (plugin.Equals(name, StringComparison.InvariantCultureIgnoreCase)) 
//                     return (p.logic, p.meta);
//             }

//             return (null, null);
//         }

//         public static string PluginName(ILogic plugin)
//         {
//             return plugin.GetType().FullName;
//         }

//         public List<KeyValuePair<string, string>> GetPluginMetadata(string pluginName)
//         {
//             var plugin = GetPlugin(pluginName);
//             return plugin.meta.Info;
//         }
        
//         public MetaData GetMetaInfo(ILogic plugin)
//         {
//             if (plugin is IInfo info)
//             {
//                 return new MetaData(info.GetInfo());
//             }

//             return new MetaData(new List<KeyValuePair<string, string>>());
//         }

//         public void AddAssembly(Assembly asm, bool replace = false)
//         {
//             var loader = this.GetLoader();
            
//             var logicList = loader.GetTypes<ILogic>(asm);
//             logicList?.ToList().ForEach(this.AddPlugin);

//             // types
//             var exportedTypes = asm.ExportedTypes.Select(r=>r.Name).ToArray();            
//         }

//         public void Reset()
//         {
//             this.PluginAssemblyCache.Unload();
//         }

//         public class CustomDomain : AssemblyLoadContext
//         {
//             public CustomDomain():base(isCollectible: true)
//             {

//             }

//             protected override Assembly Load(AssemblyName name)
//             {
//                 return null;
//             }
//         }
//     }
// }