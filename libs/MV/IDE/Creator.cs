using MV.Models;

namespace MV.IDE.Creator;

public class Creator
{
    public Creator()
    {

    }

    public string DefaultVerse()
    {
        var verse = new VerseDefinition()
        {
            E = new List<VerseReference>()
            {
                new VerseReference()
                {
                    GH="user/repo/branch",
                    N='1',
                    Name=new I18NString("Hello world")
                    {
                        Translations=new Dictionary<string, string>()
                        {
                            {"pl-PL","Cześć świecie"}
                        }
                    }
                }
            },
            Name =  new I18NString("Home")
            {
                Translations = new Dictionary<string, string>()
                {
                    {"pl-PL","Dom"}
                }
            }
        };

        return
            System.Text.Json.JsonSerializer.Serialize(verse);
    }
}