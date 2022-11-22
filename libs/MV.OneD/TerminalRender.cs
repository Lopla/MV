using MV.Forms;
using MV.Interfaces;
using Terminal.Gui;
using Button = MV.Forms.Button;
using Label = MV.Forms.Label;

namespace MV.OneD;

public class TerminalRender
{
    private Toplevel top;

    public TerminalRender()
    {
        Application.Init();
        this.top = new Toplevel
        {
            IsMdiContainer = true,
        };        
    }

    public Task Start()
    {
        Application.Run(top);
        Application.Shutdown();

        return Task.CompletedTask;
    }
    
    private (int offsetX, int offsetY) ShowVFrame(
        View view, (int offsetX, int offsetY) offsets, 
        VFrame frame, 
        (int directionx, int directiony) directions)
    {
        var fv = new FrameView
        {
            Width = Dim.Fill(),
            Y = offsets.offsetY
        };

        var sv = new ScrollView();

        var startOffset = (0, 0);
        foreach (var item in frame.Elements) startOffset = 
            RenderFrame(fv, item.Value, startOffset, directions);

        offsets.offsetX += startOffset.Item1;
        offsets.offsetY += startOffset.Item2 + 2;

        fv.Height = startOffset.Item2 + 2;

        fv.Add(sv);
        view.Add(fv);
        return offsets;
    }
    
    private static (int offsetX, int offsetY) ShowLabel(
        View view, 
        (int offsetX, int offsetY) offsets, 
        Label lb,
        (int directionx, int directiony) directions)
    {
        var label = new Terminal.Gui.Label
        {
            Text = lb.Text.T,
            Width = Dim.Fill(),
            Height = 1,
            Y = offsets.offsetY
        };
        offsets.offsetY+= directions.directiony;
        offsets.offsetX+= directions.directionx;
        view.Add(label);
        return offsets;
    }

    private static (int offsetX, int offsetY) ShowButton(
        View view, 
        (int offsetX, int offsetY) offsets, 
        Button bt,
        (int directionx, int directiony) directions)
    {
        var button = new Terminal.Gui.Button
        {
            Text = bt.Text.T,
            Width = Dim.Fill(),
            Height = 1,
            Y = offsets.offsetY
        };
        button.Clicked += () => { bt.OnClicked(); };

        offsets.offsetY+= directions.directiony;
        offsets.offsetX+= directions.directionx;

        view.Add(button);
        return offsets;
    }


    public (int offsetX, int offsetY) RenderFrame(
        View view, 
        IElement element, 
        (int offsetX, int offsetY) offsets, 
        (int directionx, int directiony) directions)
    {
        switch (element)
        {
            case VFrame frame:
                {
                    offsets = ShowVFrame(view, offsets, frame, (0,1));
                    break;
                }
            case HFrame frame:
                {
                    offsets = ShowHFrame(view, offsets, frame);

                    break;
                }
            case Label lb:
                {
                    offsets = ShowLabel(view, offsets, lb, directions);
                    break;
                }
            case Button bt:
                {
                    offsets = ShowButton(view, offsets, bt, directions);
                    break;
                }
            default:
                throw new NotImplementedException($"Not supported gui element: {element.GetType()}");
        }

        return offsets;
    }

    private (int offsetX, int offsetY) ShowHFrame(View view, (int offsetX, int offsetY) offsets, HFrame frame)
    {
        var fv = new FrameView
        {
            Width = Dim.Fill(),
            Y = offsets.offsetY
        };

        var startOffset = (0, 0);
        foreach (var item in frame.Elements) startOffset = RenderFrame(fv, item.Value, startOffset, (1,0));

        offsets.offsetX += startOffset.Item1;
        offsets.offsetY += startOffset.Item2 + 2;

        fv.Height = startOffset.Item2 + 2;

        view.Add(fv);
        return offsets;
    }

    internal void Show(IElement element)
    {
        
        // it always starts with the window
        var w = new Window
        {
            Width = Dim.Percent(75),
            Height = Dim.Percent(75),
            Border = new Border()
            {
               BorderStyle = BorderStyle.Rounded,
            },
        };

        w.Enter += (a)=>{
            this.top.BringSubviewToFront(w);
        };

        RenderFrame(w, element, (0, 0), (0 ,0));
        
        this.top.Add(w);
    }
}