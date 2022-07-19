using MV.Models;

namespace MV.Client;

public class MVClient
{
    public VerseDefinition Def { get; protected set;}

    public MVClient(VerseDefinition def = null)
    {
        Def = def;
    }

    public void Init()
    {
        if(this.Def==null)
        {
            // let's fall back to defult
            this.setupDefaults();
        }
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