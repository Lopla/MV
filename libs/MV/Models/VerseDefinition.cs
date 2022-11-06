using System;
using System.Collections.Generic;
using System.Linq;

namespace MV.Models
{
    public class VerseDefinition
    {
        public VerseDefinition()
        {   
        }

        /// <summary>
        /// All exits to other worlds from this metaverse
        /// </summary>
        /// <value></value>
        public List<VerseReference> E { get; } = new List<VerseReference>();

        /// <summary>
        /// Name for this verse definition
        /// </summary>
        public I18NString Name{get;set;} = new I18NString();

        public VerseDefinition AddExit(VerseReference reference)
        {
            // validation
            if (this.E.Any(exit => exit.N == reference.N))
            {
                throw new InvalidOperationException($"Exit reference {reference.N} already defined");
            }
            this.E.Add(reference);

            return this;
        }
    }
}