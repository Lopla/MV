using MV.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MV.Client;

public class MVClient
{
    public VerseDefinition Def { get; protected set;}

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

        this.Def = DownloadDefinition(reference.GH).Result;

        return Task.CompletedTask;    }

    public Task Init(VerseDefinition definition)
    {
        this.Def = definition;

        return Task.CompletedTask;
    }

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