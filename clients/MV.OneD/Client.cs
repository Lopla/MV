using Home;
using MV.Models;

namespace MV.OneD;

public class Client
{
    private HomeManifest plugin;

    public Client(VerseDefinition def)
    {
    }

    public void Start()
    {
    }

    public void Init()
    {
        this.plugin = new Home.HomeManifest();
    }

    public List<VerseReference> Exits()
    {
        List<VerseReference> result = new List<VerseReference>();

        return result;
    }
}