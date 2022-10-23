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
        private readonly IMetaVerse _metaverse;
        private readonly bool _useFilesInsteadOfStream;
        private IVerse _verse;
        
        public MVClient(IMetaVerse metaVerse, bool useFilesInsteadOfStream = false)
        {
            _metaverse = metaVerse;
            _useFilesInsteadOfStream = useFilesInsteadOfStream;
        }

        public async Task Start()
        {
            await _verse.Start();
            await _metaverse.Start();
        }

        public async Task Load(VerseReference reference)
        {
            var def = await DownloadDefinition(reference.GH);

            _verse = def.Verse();
            await _verse.Init(_metaverse);
        }

        public async Task Init()
        {
            await _metaverse.Init();

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
        /// <param name="repo"></param>
        /// <returns></returns>
        public async Task<IManifest> DownloadDefinition(string gitHubAertefactPath)
        {
            //var u = new UriBuilder("https://raw.githubusercontent.com");
            //u.Path += repo.TrimEnd('/');
            //u.Path += "/verse.json";
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://github.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("User-Agent", "Revuo home DownloadArtefact");

                //TODO: fix a bug in system console
                
                var data =  client.GetByteArrayAsync( gitHubAertefactPath).Result;

                if (_useFilesInsteadOfStream)
                {
                    var path = SaveVerse(data);

                    return await GetManfiestData(path);
                }
                else
                {
                    return await GetManfiestData(data);
                }
            }
        }

        private string SaveVerse(byte[] bytes, string fileName = null)
        {
            var tempPath =
                System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(),
                    fileName ?? Guid.NewGuid()+"-verse.dll");

            
            using (var bw = new BinaryWriter(File.Create(tempPath)))
            {
                bw.Write(bytes);
                bw.Flush();
                bw.Close();
            }

            return tempPath;
        }

        private Task<IManifest> GetManfiestData(byte[] data)
        {
            var loader = new Loader.Loader(new CustomDomain());
            var a = loader.LoadFromBytes<IManifest>(data);

            return Task.FromResult(a);
        }

        private Task<IManifest> GetManfiestData(string fileName)
        {
            var loader = new Loader.Loader(new CustomDomain());
            var a = loader.LoadFromFile<IManifest>(fileName);

            return Task.FromResult(a);
        }
    }
}