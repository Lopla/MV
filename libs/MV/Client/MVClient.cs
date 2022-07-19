using MV.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MV.Client;

public class MVClient
{
    public VerseDefinition Def { get; protected set;}

    public async Task Init(VerseReference reference = null)
    {
        if(reference == null)
        {
            reference = new VerseReference()
            {
                N="0",
                GH="llaagg/mv-home/main/",
                Name = new I18NString("Home world")
            };
        }

        this.Def = await LoadDefinition(reference.GH);
    }

    public async Task<VerseDefinition> LoadDefinition(string repo)
    {
        var u = new UriBuilder("https://raw.githubusercontent.com");
        u.Path+=repo.TrimEnd('/');
        u.Path+="/verse.json";

        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Add("User-Agent", "Revuo home DownloadArtefact");

        //TODO: fix a bug in system console
        //remove Result
        return client.GetFromJsonAsync<VerseDefinition>(u.ToString()).Result;

    }

    /// <summary>
    /// list of other verses where you can go to
    /// </summary>
    /// <returns></returns>
    public List<VerseReference> Exits()
    {
        List<VerseReference> result = new List<VerseReference>();
        return result;
    }
}