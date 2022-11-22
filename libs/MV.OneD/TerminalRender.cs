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
        
    private static (int offsetX, int offsetY) ShowLabel(
        View view, 
        (int offsetX, int offsetY) offsets, 
        Label lb,
        (int directionx, int directiony) directions)
    {
        var label = new Terminal.Gui.Label();
        label.Text = lb.Text.T;
        var off = CreateElement(label, offsets, directions);

        view.Add(label);
        return offsets;
    }

    private static (int offsetX, int offsetY)  CreateElement(
        View lb, 
        (int offsetX, int offsetY) offsets,
        (int directionx, int directiony) directions)
    {
        var w = directions.directiony == 1 ? Dim.Fill() : 100;
        var h = directions.directionx == 1 ? Dim.Fill() : 100;

        lb.Y = offsets.offsetY;
        lb.X = offsets.offsetX;
        lb.Width = w;
        lb.Height = h;

        int _w,_h;
        lb.GetCurrentHeight(out _h);      
        lb.GetCurrentWidth(out _w);      


        offsets.offsetY += _h + directions.directiony;
        offsets.offsetX += _w + directions.directionx;

        return offsets;
    }

    private static (int offsetX, int offsetY) ShowButton(
        View view, 
        (int offsetX, int offsetY) offsets, 
        Button bt,
        (int directionx, int directiony) directions)
    {
        int h=0;
        int w=0;
        var button = new Terminal.Gui.Button
        {
            Text = bt.Text.T,
            Width = Dim.Fill(),
            Height = 1,
            Y = offsets.offsetY
        };
        button.Clicked += () => { bt.OnClicked(); };

        offsets.offsetY+= 1 + directions.directiony;
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
                    offsets = ShowFrame(view, offsets, frame, directions, (0,1));
                    break;
                }
            case HFrame frame:
                {
                    offsets = ShowFrame(view, offsets, frame, directions, (1,0));
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

    private (int offsetX, int offsetY) ShowFrame(
        View view, (int offsetX, int offsetY) offsets, 
        Frame frame,
        (int directionx, int directiony) directions,
        (int directionx, int directiony) type)
    {
        var fv = new FrameView
        {
            Width = Dim.Fill(),
            Y = offsets.offsetY,
            Title= $"Frame {type}"
        };

        var startOffset = (0, 0);
        foreach (var item in frame.Elements) startOffset = 
            RenderFrame(fv, item.Value, startOffset, type);

        offsets.offsetX += startOffset.Item1 + (2 * directions.directionx);
        offsets.offsetY += startOffset.Item2 + (2 * directions.directiony);

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