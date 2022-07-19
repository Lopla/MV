using MV.Models;
using System.Net.Http.Json;

namespace MV.Client;

public class MVClient
{
    public VerseDefinition Def { get; protected set;}

    public MVClient()
    {
    }

    public void Init(VerseReference reference)
    {
        "https://raw.githubusercontent.com/Lopla/MV/main/0.json";
        LoadDefinition(reference.Url);
    }

    public async Task<VerseDefinition> LoadDefinition(string url)
    {
        using HttpClient client = new()
        {
            BaseAddress = new Uri(url)
        };

        return await client.GetFromJsonAsync<VerseDefinition>("mv.json");
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