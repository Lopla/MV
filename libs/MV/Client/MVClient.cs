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
        /// Load remote verse and initialized it
        /// </summary>
        /// <param name="reference"></param>
        public Task Init(VerseReference reference = null)
        {
            if(reference == null)
            {
                reference = new VerseReference()
                {
                    N='0',
                    GH= "https://github.com/llaagg/mv-home",
                    Name = new I18NString("Home world")
                };
            }

            this.Def = await DownloadDefinition(reference.GH).Result;

            
            this.verse = manifest.Verse();

            await verse.Init(this.metaverse);
            await metaverse.Init();
            
            return Task.CompletedTask;    
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