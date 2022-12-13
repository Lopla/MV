using System.Diagnostics;
using MV.Forms;
using MV.Interfaces;
using Terminal.Gui;
using static MV.OneD.TerminalRender;
using Button = MV.Forms.Button;
using Label = MV.Forms.Label;

namespace MV.OneD;

public class TerminalRender
{
    public enum Direction
    {
        TopBottom,
        ToRight
    }

    private readonly Toplevel _top;

    public TerminalRender()
    {
        Application.Init();
        _top = new Toplevel
        {
            IsMdiContainer = true
        };
    }

    public Task Start()
    {
        Application.Run(_top);
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
            Border = new Border
            {
                BorderStyle = BorderStyle.Rounded
            }
        };
         
        w.Enter += a => { _top.BringSubviewToFront(w); };

        RenderGuiElement(w, element, (0, 0), Direction.TopBottom,0);

        _top.Add(w);
    }

    private static (int offsetX, int offsetY) CalculateSize(
        View viewElement,
        (int offsetX, int offsetY) offsets,
        Direction direction,
        (int w, int h)? defaultSize = null)
    {
        defaultSize = defaultSize ?? (10, 1);

        var w = //direction == Direction.TopBottom ? Dim.Fill() :
                defaultSize.Value.w;
        var h = //direction == Direction.ToRight ? Dim.Fill() :
                defaultSize.Value.h;

        viewElement.Y = offsets.offsetY;
        viewElement.X = offsets.offsetX;
        viewElement.Width = w;
        viewElement.Height = h;

        int _w, _h;

        viewElement.GetCurrentHeight(out _h);
        viewElement.GetCurrentWidth(out _w);

        offsets.offsetY += _h;
        offsets.offsetX += _w;

        return offsets;
    }

    public (int offsetX, int offsetY) RenderGuiElement(
        View view,
        IElement element,
        (int offsetX, int offsetY) offsets,
        Direction direction,
        int level)
    {
        var initOffset = offsets;
        switch (element)
        {
            case VFrame frame:
            {
                offsets = ShowFrame(view, offsets, frame, direction, Direction.TopBottom, level++);
                break;
            }
            case HFrame frame:
            {
                offsets = ShowFrame(view, offsets, frame, direction, Direction.ToRight, level++);
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

        Debug.WriteLine($"{new string('-', level) }Adding element at {initOffset} resulting with {offsets}");
        return offsets;
    }

    private (int offsetX, int offsetY) ShowFrame(
        View view,
        (int offsetX, int offsetY) offsets,
        Frame frame,
        Direction parentDirection,
        Direction frameDirection,
        int level)
    {
        var fv = new FrameView();
        Debug.WriteLine($"{new string('-', level) }F: {offsets}");
        var o = CalculateSize(fv, offsets, parentDirection, (5, 2));
        
        var startOffset = RenderElements(offsets, frame, frameDirection, fv, level);

        o.offsetY = startOffset.offsetY + offsets.offsetY + 2;
        o.offsetX = startOffset.offsetX + offsets.offsetX + 2;

        fv.Height = parentDirection == Direction.TopBottom ? Dim.Fill() : o.offsetY;
        fv.Width = parentDirection == Direction.ToRight ? Dim.Fill() : o.offsetX;

        view.Add(fv);

        Debug.WriteLine($"{new string('-', level) }F: {view.ToString()} {o}");

        return o;
    }

    private (int offsetX, int offsetY) RenderElements(
        (int offsetX, int offsetY) offsets, 
        Frame frame,
        Direction frameDirection, 
        FrameView fv,
        int level)
    {
        var so = (0,0);

        Debug.WriteLine($"{new string('-', level) }Starting rendering frame at: {so}");
        int maxHeight = 1, maxWidth = 1;
        foreach (var item in frame.Elements)
        {
            var newSize = RenderGuiElement(fv, item.Value, so, frameDirection, level+1);
            Debug.WriteLine($"{new string(' ', level) } * added {item.Value} @{so} new size: {newSize}");

            maxHeight = maxHeight < newSize.offsetY ? newSize.offsetY : maxHeight;
            maxWidth = maxWidth < newSize.offsetX ? newSize.offsetX : maxWidth;

            if (frameDirection == Direction.TopBottom)
            {
                so = (0, newSize.offsetY);
            }
            else if (frameDirection == Direction.ToRight)
            {
                so = (newSize.offsetX, 0);
            }

        }

        var r = (maxWidth, maxHeight);
        Debug.WriteLine($"{new string('-', level) } * Frame size {r}");
        return r;
    }

    private static (int offsetX, int offsetY) ShowLabel(
        View view,
        (int offsetX, int offsetY) offsets,
        Label lb,
        Direction direction)
    {
        var label = new Terminal.Gui.Label
        {
            Text = lb.Text.T,
            TextAlignment = TextAlignment.Centered
        };
        var o = CalculateSize(label, offsets, direction, (lb.Text.T.Length + 2, 1));

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
            Text = bt.Text.T
        };
        button.Clicked += () => { bt.OnClicked(); };

        var o = CalculateSize(button, offsets, direction, (bt.Text.T.Length + 2, 1));
        view.Add(button);

        return o;
    }
}