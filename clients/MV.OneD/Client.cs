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

    public List<VersReference> Exits()
    {
        List<VersReference> result = new List<VersReference>();

        return result;
    }
}