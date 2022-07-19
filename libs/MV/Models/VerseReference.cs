namespace MV.Models
{
    public class VerseReference
    {
        /// <summary>
        /// Reference number to be used in navigation
        /// </summary>
        /// <value></value>
        public string N {get;set;} = String.Empty;
        
        /// <summary>
        /// Where the verse file is found. This should be a public GH repo url
        /// </summary>
        public string GH {get;set;}= String.Empty;

        /// <summary>
        /// Human readable title of the referenced verse
        /// </summary>
        /// <returns></returns>
        public I18NString Name {get;set;} = new I18NString();
    }
}