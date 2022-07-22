using MV.Forms;
using MV.Interfaces;
using Terminal.Gui;

namespace MV.OneD;

public class OneDConsole : IMetaVerse
{
    private View parent;

    public OneDConsole(View parent)
    {
        this.parent = parent;
    }
    
    public void Show(View view, IElement element, int idx = 0)
    {
        if(element is VFrame vframe)
        {
            var fv = new FrameView()
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                Y=idx
            };

            int relIdx = 0;
            foreach (var item in vframe.Elements)
            {
                this.Show(fv, item.Value, relIdx++);
            }
            view.Add(fv);

        }else if(element is Forms.Label lb)
        {
            var label = new Terminal.Gui.Label()
            {
                Text = lb.Text.T,
                Width = Dim.Fill(),
                Height = 1,
                Y= idx,
            };
            view.Add(label);
        }else if(element is Forms.Button bt)
        {
            var label = new Terminal.Gui.Button()
            {
                Text = bt.Text.T,
                Width = Dim.Fill(),
                Height = 1,
                Y= idx,
            };

            label.Clicked+=()=>{
                bt.OnClicked();
            };
        
            view.Add(label);
        }else{
            throw new NotImplementedException($"Not supported gui element: {element.GetType()}");
        }
    }

    public void Show(IElement element)
    {
        // it always starts with the window
        Window w = new Window()
        {
            Width = Dim.Percent(50),
            Height = Dim.Percent(50)
        };

        this.Show(w, element);
        this.parent.Add(w);  
    }
}
