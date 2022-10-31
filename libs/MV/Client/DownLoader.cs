using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using MV.Interfaces;
using MV.Loader;

namespace MV.Client
{
    public class DownLoader
    {
        private readonly IAssemblyContext _assemblyContext;
        private readonly bool _useFilesInsteadOfStream;

        public DownLoader(IAssemblyContext assemblyContext, bool useFilesInsteadOfStream)
        {
            _assemblyContext = assemblyContext;
            _useFilesInsteadOfStream = useFilesInsteadOfStream;
        }

        /// <summary>
        ///     Downloads remote verse definition
        /// </summary>
        public async Task<IManifest> DownloadDefinition(string gitHubArtifactPath)
        {
            //var u = new UriBuilder("https://raw.githubusercontent.com");
            //u.Path += repo.TrimEnd('/');
            //u.Path += "/verse.json";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://github.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("User-Agent", "Revuo home DownloadArtifact");

                //TODO: fix a bug in system console

                var data = client.GetByteArrayAsync(gitHubArtifactPath).Result;

                if (_useFilesInsteadOfStream)
                {
                    var path = SaveVerseDllToFile(data);

                    return await GetManifestData(path);
                }

                return await GetManifestData(data);
            }
        }

        private string SaveVerseDllToFile(byte[] bytes, string fileName = null)
        {
            var tempPath =
                Path.Combine(
                    Path.GetTempPath(),
                    fileName ?? Guid.NewGuid() + "-verse.dll");

            using (var bw = new BinaryWriter(File.Create(tempPath)))
            {
                bw.Write(bytes);
                bw.Flush();
                bw.Close();
            }

            return tempPath;
        }

        private Task<IManifest> GetManifestData(byte[] data)
        {
            var loader = GetLoader();
            var a = loader.LoadFromBytes<IManifest>(data);

            return Task.FromResult(a);
        }

        private Task<IManifest> GetManifestData(string fileName)
        {
            var loader = GetLoader();
            var a = loader.LoadFromFile<IManifest>(fileName);

            return Task.FromResult(a);
        }

        private Loader.Loader GetLoader()
        {
            return new Loader.Loader(_assemblyContext);
        }
    }
}