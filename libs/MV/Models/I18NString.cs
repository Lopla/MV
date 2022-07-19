namespace MV.Models
{
    public class I18NString
    {
        /// <summary>
        /// Keep in mind that this is a fallback text, you should use the translated property. 
        /// </summary>
        /// <value></value>
        public string T {get;set;} = String.Empty;

        public Dictionary<string,string> Translations = new Dictionary<string, string>();

        public I18NString(string text = "")
        {
            this.T = text;
        }
    }
}