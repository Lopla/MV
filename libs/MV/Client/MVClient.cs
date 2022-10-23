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
        private readonly IAssemblyContext _assemblyContext;
        private readonly IMetaVerseRunner _metaVerse;
        private readonly bool _useFilesInsteadOfStream;

        public MVClient(
            IMetaVerseRunner metaVerse,
            IAssemblyContext context = null,
            bool useFilesInsteadOfStream = false)
        {
            _metaVerse = metaVerse;
            _useFilesInsteadOfStream = useFilesInsteadOfStream;
            _assemblyContext = context ?? new SeparatedDomainContext();
        }

        public async Task Start()
        {
            //// endless loop:
            await _metaVerse.Start();
        }

        public async Task Init()
        {
            await _metaVerse.Init();
        }

        public async Task LoadAndInit(VerseReference reference)
        {
            var downloadedVerseDefinition = await DownloadDefinition(reference.GH);

            await InitVerse(downloadedVerseDefinition);
        }

        public async Task LoadAndInit(IManifest reference)
        {
            await InitVerse(reference);
        }

        public async Task LoadDefault()
        {
            await LoadAndInit(new VerseReference
            {
                N = '0',
                GH = "llaagg/mv-home/releases/download/v0.92.1/Home.dll",
                Name = new I18NString("Home")
            });
        }

        private async Task InitVerse(IManifest downloadedVerseDefinition)
        {
            var verse = downloadedVerseDefinition.Verse();
            await _metaVerse.InitVerse(verse);
        }

        /// <summary>
        ///     Downloads remote verse definition
        /// </summary>
        /// <returns></returns>
        private async Task<IManifest> DownloadDefinition(string gitHubArtifactPath)
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
            return new Loader.Loader(_assemblyContext);
        }
    }
}