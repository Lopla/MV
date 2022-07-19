namespace MV.Models
{
    public class VerseDefinition
    {
        /// <summary>
        /// All exits to other worlds from this metavers
        /// </summary>
        /// <value></value>
        public List<VerseReference> E { get; set; } = new List<VerseReference>();
    }
}