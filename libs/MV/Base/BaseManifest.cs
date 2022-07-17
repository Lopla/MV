using MV.Interfaces;

namespace MV.Base;
public abstract class BaseManifest : IManifest
{
    public string GetName()
    {
        return this.OnGetName();
    }

    public abstract string OnGetName();
}
