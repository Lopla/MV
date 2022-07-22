using MV.Interfaces;

namespace MV.Forms;

public abstract class Frame : IFrame
{
    public Frame()
    {
        this.Elements = new Dictionary<string, IElement>();
    }

    public Dictionary<string, IElement> Elements { get; protected set;}

    public void Add(IElement element)
    {
        this.Elements.Add(Guid.NewGuid().ToString(), element);
    }
}