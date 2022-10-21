using System;
using System.Net.Http;
using System.Threading.Tasks;
using MV.Interfaces;
using MV.Models;

namespace MV.Client
{
    public class MVClient
    {
        private IMetaVerse metaverse;
        private IManifest manifest;
        private IVerse verse;

        public MVClient(IMetaVerse metaVerse)
        {
            this.metaverse = metaVerse;
        }

        public async Task Start()
        {
            await verse.Start();
            await metaverse.Start();
        }

        /// <summary>
        /// Load remote verse and initilized it
        /// </summary>
        /// <param name="reference"></param>
        public Task Init(VerseReference reference = null)
        {
            if(reference == null)
            {
                reference = new VerseReference()
                {
                    N='0',
                    GH="llaagg/mv-home/main/",
                    Name = new I18NString("Home world")
                };
            }

            //this.Def = DownloadDefinition(reference.GH).Result;

            return Task.CompletedTask;    
        }


        /// <summary>
        /// Initilize verse from definition
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public async Task Init(IManifest manifest)
        {
            this.manifest = manifest;
            this.verse = manifest.Verse();

            await verse.Init(this.metaverse);
            await metaverse.Init();
        }

        /// <summary>
        /// Downloads remote verse definition 
        /// </summary>
        /// <param name="repo"></param>
        /// <returns></returns>
        public async Task<VerseDefinition> DownloadDefinition(string repo)
        {
            var u = new UriBuilder("https://raw.githubusercontent.com");
            u.Path+=repo.TrimEnd('/');
            u.Path+="/verse.json";

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("User-Agent", "Revuo home DownloadArtefact");

                //TODO: fix a bug in system console
                //remove Result
                var url = u.ToString();
                return null;
                //return await client.GetFromJsonAsync<VerseDefinition>(url);
            }
        }
    }
}