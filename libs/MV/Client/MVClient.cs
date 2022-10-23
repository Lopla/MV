using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using MV.Interfaces;
using MV.Loader;
using MV.Models;

namespace MV.Client
{
    public class MVClient
    {
        private readonly IMetaVerse _metaVerse;
        private readonly bool _useFilesInsteadOfStream;
        private IVerse _verse;
        private readonly IAssemblyContext _assemblyContext;

        public MVClient(
            IMetaVerse metaVerse,
            IAssemblyContext context = null,
            bool useFilesInsteadOfStream = false)
        {
            _metaVerse = metaVerse;
            _useFilesInsteadOfStream = useFilesInsteadOfStream;
            _assemblyContext = context ?? new SeparatedDomainContext();
        }

        public async Task Start()
        {
            await _verse.Start();
            await _metaVerse.Start();
        }

        public async Task Load(VerseReference reference)
        {
            var def = await DownloadDefinition(reference.GH);

            _verse = def.Verse();
            await _verse.Init(_metaVerse);
        }

        public async Task Load(IManifest reference)
        {
            _verse = reference.Verse();
            await _verse.Init(_metaVerse);
        }

        public async Task Init()
        {
            await _metaVerse.Init();

            //// load home
            await Load(new VerseReference
            {
                N = '0',
                GH = "llaagg/mv-home/releases/download/v0.92.1/Home.dll",
                Name = new I18NString("Home")
            });
        }

        /// <summary>
        ///     Downloads remote verse definition
        /// </summary>
        /// <returns></returns>
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
                    var path = SaveVerse(data);

                    return await GetManifestData(path);
                }

                return await GetManifestData(data);
            }
        }

        private string SaveVerse(byte[] bytes, string fileName = null)
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
            return new Loader.Loader(this._assemblyContext);
        }

    }
}