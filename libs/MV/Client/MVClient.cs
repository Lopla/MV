using MV.Models;
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
                GH="llaagg/mv-home",
                Name = new I18NString("Home world")
            };
        }

        this.Def = await LoadDefinition(reference.GH);
    }

    public async Task<VerseDefinition> LoadDefinition(string repo)
    {
        var u = new UriBuilder("https://raw.githubusercontent.com");
        u.Path+=repo;
                
        using HttpClient client = new()
        {
            BaseAddress = u.Uri
        };

        return await client.GetFromJsonAsync<VerseDefinition>("verse.json");
    }

    private void setupDefaults()
    {
        this.Def = new VerseDefinition();
        this.Def.Name = new I18NString("Meta verse");

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