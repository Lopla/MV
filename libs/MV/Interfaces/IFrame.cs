using System.Collections.Generic;

namespace MV.Interfaces
{
    public interface IFrame : IElement
    {
        /// <summary>
        /// Frame can have multiple elements, not all elements in frame can be frames
        /// </summary>
        Dictionary<string, IElement> Elements {get;}
    }
}