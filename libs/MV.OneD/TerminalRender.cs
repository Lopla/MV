using System.Diagnostics;
using MV.Forms;
using MV.Interfaces;
using Terminal.Gui;
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

        RenderFrame(w, element, (0, 0), Direction.TopBottom);

        _top.Add(w);
    }

    private static (int offsetX, int offsetY) CalculateSize(
        View viewElement,
        (int offsetX, int offsetY) offsets,
        Direction direction,
        (int w, int h)? defaultSize = null)
    {
        defaultSize = defaultSize ?? (10, 1);

        var w = direction == Direction.TopBottom ? Dim.Fill() : defaultSize.Value.w;
        var h = direction == Direction.ToRight ? Dim.Fill() : defaultSize.Value.h;

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
        Direction parentDirection,
        Direction frameDirection)
    {
        var fv = new FrameView();
        var o = CalculateSize(fv, offsets, parentDirection, (10, 2));

        var startOffset = offsets;
        
        startOffset = 
            RenderElements(offsets, frame, frameDirection, startOffset, fv);

        o.offsetY = startOffset.offsetY + offsets.offsetY +2;
        o.offsetX = startOffset.offsetX + offsets.offsetX +2;

        fv.Height = o.offsetY;

        view.Add(fv);

        Debug.WriteLine($"{view.ToString()} {o}");

        return o;
    }

    private (int offsetX, int offsetY) RenderElements((int offsetX, int offsetY) offsets, Frame frame,
        Direction frameDirection, (int offsetX, int offsetY) startOffset, FrameView fv)
    {
        int maxHeight = 1, maxWidth = 1;
        var so = startOffset;
        foreach (var item in frame.Elements)
        {
            var newSize = RenderFrame(fv, item.Value, so, frameDirection);
            Debug.WriteLine($"Added {item.Value} {startOffset}");

            maxHeight = maxHeight < newSize.offsetY ? newSize.offsetY : maxHeight;
            maxWidth = maxWidth < newSize.offsetX ? newSize.offsetX : maxWidth;

            if (frameDirection == Direction.TopBottom)
            {
                so = (offsets.offsetX, newSize.offsetY);
            }
            else if (frameDirection == Direction.ToRight)
            {
                so = (newSize.offsetX, offsets.offsetY);
            }

        }

        var r = (maxWidth, maxHeight);
        Debug.WriteLine($"frame size {r}");
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