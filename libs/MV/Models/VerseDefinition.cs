using System.Collections.Generic;

namespace MV.Models
{
    public class VerseDefinition
    {
        /// <summary>
        /// All exits to other worlds from this metaverse
        /// </summary>
        /// <value></value>
        public List<VerseReference> E { get; set; } = new List<VerseReference>();

        /// <summary>
        /// Name for this verse definition
        /// </summary>
        public I18NString Name{get;set;} = new I18NString();
    }
}