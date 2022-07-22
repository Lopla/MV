using MV.Interfaces;
using MV.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MV.Client;

public class MVClient
{
    private IMetaVerse context;

    public MVClient(IMetaVerse context)
    {
        this.context = context;
    }

    /// <summary>
    /// Load remote verse and initilized it
    /// </summary>
    /// <param name="reference"></param>
    /// <returns></returns>
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

        return Task.CompletedTask;    }


    /// <summary>
    /// Initilize verse from definition
    /// </summary>
    /// <param name="definition"></param>
    /// <returns></returns>
    public async Task Init(IManifest manifest)
    {
        await manifest.Verse().Init(this.context);
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

        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Add("User-Agent", "Revuo home DownloadArtefact");

        //TODO: fix a bug in system console
        //remove Result
        var url = u.ToString();
        return await client.GetFromJsonAsync<VerseDefinition>(url);
    }
}