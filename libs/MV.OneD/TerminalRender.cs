using MV.Forms;
using MV.Interfaces;
using Terminal.Gui;
using Button = MV.Forms.Button;
using Label = MV.Forms.Label;

namespace MV.OneD;

public class TerminalRender
{
    public enum Direction{TopBottom, ToRight};

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
    
    public void Show(IElement element)
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

        RenderFrame(w, element, (0, 0), Direction.TopBottom);
        
        this.top.Add(w);
    }
    
    private static (int offsetX, int offsetY) SetSize(
        View viewElement, 
        (int offsetX, int offsetY) offsets,
        Direction direction,
        (int w, int h) ?defaultSize = null)
    {
        defaultSize = defaultSize == null ? (10, 1) : defaultSize;

        var w = direction == Direction.TopBottom ? Dim.Fill() : defaultSize.Value.w;
        var h = direction == Direction.ToRight ? Dim.Fill() : defaultSize.Value.h;

        viewElement.Y = offsets.offsetY;
        viewElement.X = offsets.offsetX;
        viewElement.Width = w;
        viewElement.Height = h;

        int _w,_h;
        viewElement.GetCurrentHeight(out _h);      
        viewElement.GetCurrentWidth(out _w);      

        offsets.offsetY += _h ;
        offsets.offsetX += _w ;

        return offsets;
    }

    public (int offsetX, int offsetY) RenderFrame(
        View view, 
        IElement element, 
        (int offsetX, int offsetY) offsets, 
        Direction direction)
    {
        switch (element)
        {
            case VFrame frame:
                {
                    offsets = ShowFrame(view, offsets, frame, direction, Direction.TopBottom);
                    break;
                }
            case HFrame frame:
                {
                    offsets = ShowFrame(view, offsets, frame, direction, Direction.ToRight);
                    break;
                }
            case Label lb:
                {
                    offsets = ShowLabel(view, offsets, lb, direction);
                    break;
                }
            case Button bt:
                {
                    offsets = ShowButton(view, offsets, bt, direction);
                    break;
                }
            default:
                throw new NotImplementedException($"Not supported gui element: {element.GetType()}");
        }

        return offsets;
    }

    private (int offsetX, int offsetY) ShowFrame(
        View view, 
        (int offsetX, int offsetY) offsets, 
        Frame frame,
        Direction direction,
        Direction newDirection)
    {
        var fv = new FrameView();
        offsets = SetSize(fv, offsets, direction, (10,2));

        var startOffset = (0, 0);
        var maxHeight = 1;
        foreach (var item in frame.Elements) {
            startOffset = RenderFrame(fv, item.Value, startOffset, newDirection);

            if(newDirection == Direction.TopBottom)
            {
                startOffset = (offsets.offsetX, startOffset.Item2);
            }else if(newDirection == Direction.ToRight)
            {
                maxHeight = maxHeight < startOffset.Item2 ? startOffset.Item2 : maxHeight;
                startOffset = (startOffset.Item1, offsets.offsetY);
            }
        }

        startOffset.Item2 = maxHeight + 2;    

        if(newDirection == Direction.TopBottom)
        {
            fv.Height = startOffset.Item2 + 2;
        }
        if(newDirection == Direction.ToRight)
        {
            fv.Height = maxHeight + 2;
        }

        view.Add(fv);
        return startOffset;
    }

    private static (int offsetX, int offsetY) ShowLabel(
        View view, 
        (int offsetX, int offsetY) offsets, 
        Label lb,
        Direction direction)
    {
        var label = new Terminal.Gui.Label()
        {
            Text = lb.Text.T,
            TextAlignment = TextAlignment.Centered
        };
        var o = SetSize(label, offsets, direction, ( lb.Text.T.Length +2 ,1 ));

        view.Add(label);

        return o;
    }

    private static (int offsetX, int offsetY) ShowButton(
        View view, 
        (int offsetX, int offsetY) offsets, 
        Button bt,
        Direction direction)
    {
        var button = new Terminal.Gui.Button
        {
            Text = bt.Text.T,
        };
        button.Clicked += () => { bt.OnClicked(); };

        var o = SetSize(button, offsets, direction,( bt.Text.T.Length +2 ,1 ));
        view.Add(button);

        return o;
    }
}