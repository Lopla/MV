using MV.Interfaces;
using MV.Models;

namespace MV.Forms
{
    public class Label : IElement
    {
        public Label(string text = "")
        {
            this.Text = (I18NString)text;
        }
        
        public I18NString Text{get;set;} = new I18NString();

    }
}