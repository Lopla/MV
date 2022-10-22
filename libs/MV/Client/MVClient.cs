using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MV.Interfaces;
using MV.Loader;
using MV.Models;

namespace MV.Client
{
    public class MVClient
    {
        private readonly IMetaVerse metaverse;
        private IVerse _verse;

        public MVClient(IMetaVerse metaVerse)
        {
            metaverse = metaVerse;
        }

        public async Task Start()
        {
            await _verse.Start();
            await metaverse.Start();
        }

        public async Task Load(VerseReference reference)
        {
            var def = await DownloadDefinition(reference.GH);

            _verse = def.Verse();
            await _verse.Init(metaverse);
        }

        public async Task Init()
        {
            await metaverse.Init();

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

                return await GetManfiestData(data);
            }
        }

        private Task<IManifest> GetManfiestData(byte[] data)
        {
            var loader = new Loader.Loader(new CustomDomain());
            var a = loader.Load<IManifest>(data);

            return Task.FromResult(a);
        }
    }
}