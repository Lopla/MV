using MV.Interfaces;
using MV.Models;

namespace MV.Forms
{
    public delegate void OnClickedEvent();

    public class Button : IElement
    {
        public Button(I18NString? text = null) 
        {
            this.Text = text ?? new I18NString();
        }

        public I18NString Text { get; set; }

        public event OnClickedEvent? Clicked;

        public void OnClicked()
        {
            this.Clicked?.Invoke();   
        }
    }
}