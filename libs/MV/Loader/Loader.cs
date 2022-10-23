using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MV.Loader
{
    public class Loader
    {
        private IAssemblyContext _pluginAssemblyCache;

        public Loader(IAssemblyContext pluginAssemblyCache)
        {
            _pluginAssemblyCache = pluginAssemblyCache;
        }

        public IEnumerable<T> LoadFromBase64<T>(string base64)
        {
            var asm = LoadFromBase64(base64);

            foreach (var logic in GetTypes<T>(asm)) yield return logic;
        }

        public T LoadFromBytes<T>(byte[] bytes)
        {
            var asm = LoadFromBytes(bytes);

            return GetTypes<T>(asm).FirstOrDefault();
        }

        public T LoadFromFile<T>(string path)
        {
            var asm = LoadFromFile(path);

            return GetTypes<T>(asm).FirstOrDefault();
        }

        public Assembly LoadFromBase64(string base64)
        {
            var memory = Convert.FromBase64String(base64);
            var asm = LoadFromBytes(memory);

            return asm;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Assembly LoadFromBytes(byte[] assemblySource)
        {
            var assembly = _pluginAssemblyCache.LoadAssemblyFromStream(new MemoryStream(assemblySource));

            Trace.WriteLine($"Loader: Loading assembly {assembly.FullName} by demand.");

            return assembly;
        }
        
        public Assembly LoadFromFile(string path)
        {
            System.Reflection.Assembly.LoadFile(@"C:\Users\Gal\AppData\Local\Temp\3cacf86b-8269-41de-b9cf-0774c1bcf082-verse.dll");

            var assembly = _pluginAssemblyCache.LoadFromAssemblyPath(path);

            Trace.WriteLine($"Loader: Loading assembly {assembly.FullName} by demand.");

            return assembly;
        }

        public IEnumerable<T> GetTypes<T>(Assembly assembly)
        {
            var count = 0;

            foreach (var type in assembly.GetTypes())
            {
                Console.WriteLine($"{assembly.FullName} {type.Name}");
                if (typeof(T).IsAssignableFrom(type))
                    if (Activator.CreateInstance(type) is T result)
                    {
                        count++;
                        yield return result;
                    }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Reset()
        {
            _pluginAssemblyCache.Reset();
            _pluginAssemblyCache = null;
        }
    }
}