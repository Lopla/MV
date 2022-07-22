// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Reflection;
// using System.Runtime.CompilerServices;
// using Revuo.Home.Abstractions;
// using static Revuo.Home.Logic.PluginProvider;

// namespace Revuo.Home.Logic
// {
//     public class Loader
//     {
//         private CustomDomain pluginAssemblyCache;

//         public Loader(CustomDomain pluginAssemblyCache)
//         {
//             this.pluginAssemblyCache = pluginAssemblyCache;
//         }

//         public IEnumerable<(ILogic logic, MetaData meta)> Load(string base64)
//         {
//             var asm = LoadFromString(base64);

//             foreach (var logic in GetTypes<ILogic>(asm))
//             {
//                 yield return (logic, new MetaData(logic));
//             }
//         }

//         public Assembly LoadFromString(string base64)
//         {
//             var memeory = Convert.FromBase64String(base64);
//             var asm = LoadFromStream(memeory);

//             return asm;
//         }

//         [MethodImpl(MethodImplOptions.NoInlining)]
//         public void Reset()
//         {
//             pluginAssemblyCache.Unload();
//             pluginAssemblyCache = null;
//         }

//         [MethodImpl(MethodImplOptions.NoInlining)]
//         public Assembly LoadFromStream(byte[] assemblySource)
//         {
//             var assembly  = this.pluginAssemblyCache.LoadFromStream(new MemoryStream(assemblySource));
            
//             System.Diagnostics.Trace.WriteLine($"Loader: Loading assembly {assembly.FullName} by demmend.");

//             return assembly;
//         }

//         public IEnumerable<T> GetTypes<T>(Assembly assembly)
//         {
//             var count = 0;

//             foreach (var type in assembly.GetTypes())
//             {
//                 Console.WriteLine($"{assembly.FullName} {type.Name}");
//                 if (typeof(T).IsAssignableFrom(type))
//                     if (Activator.CreateInstance(type) is T result)
//                     {
//                         count++;
//                         yield return result;
//                     }
//             }
//         }
//     }
// }
