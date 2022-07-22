using MV.Interfaces;

namespace MV.Forms;

public class Frame : IFrame
{
    public Frame()
    {
        this.Elements = new Dictionary<string, IElement>();
    }

    public Dictionary<string, IElement> Elements { get; protected set;}

    public void Add(string name, IElement element)
    {
        this.Elements.Add(Guid.NewGuid().ToString(), element);
    }
}