using MV.Interfaces;
using MV.Models;

namespace MV.Forms
{
    public class Label : IElement
    {
        public Label(I18NString text = null)
        {
            this.Text = text ?? new I18NString();
        }
        
        public I18NString Text{get;set;} = new I18NString();

    }
}