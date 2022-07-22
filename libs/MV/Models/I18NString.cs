namespace MV.Models
{
    public class I18NString
    {
        /// <summary>
        /// Keep in mind that this is a fallback text, you should use the translated property. 
        /// </summary>
        /// <value></value>
        public string T {get;set;} = String.Empty;

        public Dictionary<string,string> Translations {get;set;}= new Dictionary<string, string>();

        public I18NString(string text = "")
        {
            this.T = text;
        }
        
        public static implicit operator string(I18NString t)
        {
            return t.T;
        }
    
        public static implicit operator I18NString(string t)
        {
            return new I18NString(t);
        }

        public override string ToString() => this.T;
    }
}