using MV.Forms;
using MV.Interfaces;
using Terminal.Gui;
using Button = MV.Forms.Button;
using Label = MV.Forms.Label;

namespace MV.OneD;

public class TerminalRender
{
    public TerminalRender()
    {
    }

    public (int offsetX, int offsetY) ShowV(View view, IElement element, (int offsetX, int offsetY) offsets)
    {
        switch (element)
        {
            case VFrame frame:
            {
                var fv = new FrameView
                {
                    Width = Dim.Fill(),
                    Y = offsets.offsetY
                };

                var startOffset = (0, 0);
                foreach (var item in frame.Elements) startOffset = ShowV(fv, item.Value, startOffset);

                offsets.offsetX += startOffset.Item1;
                offsets.offsetY += startOffset.Item2 + 2;

                fv.Height = startOffset.Item2 + 2;

                view.Add(fv);

                break;
            }
            case HFrame frame:
            {
                var fv = new FrameView
                {
                    Width = Dim.Fill(),
                    Y = offsets.offsetY
                };

                var startOffset = (0, 0);
                foreach (var item in frame.Elements) startOffset = ShowH(fv, item.Value, startOffset);

                offsets.offsetX += startOffset.Item1;
                offsets.offsetY += startOffset.Item2 + 2;

                fv.Height = startOffset.Item2 + 2;

                view.Add(fv);

                break;
            }
            case Label lb:
            {
                var label = new Terminal.Gui.Label
                {
                    Text = lb.Text.T,
                    Width = Dim.Fill(),
                    Height = 1,
                    Y = offsets.offsetY
                };
                offsets.offsetY++;
                view.Add(label);
                break;
            }
            case Button bt:
            {
                var label = new Terminal.Gui.Button
                {
                    Text = bt.Text.T,
                    Width = Dim.Fill(),
                    Height = 1,
                    Y = offsets.offsetY
                };
                offsets.offsetY++;
                label.Clicked += () => { bt.OnClicked(); };

                view.Add(label);
                break;
            }
            default:
                throw new NotImplementedException($"Not supported gui element: {element.GetType()}");
        }

        return offsets;
    }

    public (int offsetX, int offsetY) ShowH(View view, IElement element, (int offsetX, int offsetY) offsets)
    {
        switch (element)
        {
            case VFrame frame:
            {
                var fv = new FrameView
                {
                    Width = Dim.Fill(),
                    Y = offsets.offsetY
                };

                var startOffset = (0, 0);
                foreach (var item in frame.Elements) startOffset = ShowV(fv, item.Value, startOffset);

                offsets.offsetX += startOffset.Item1;
                offsets.offsetY += startOffset.Item2 + 2;

                fv.Height = startOffset.Item2 + 2;

                view.Add(fv);

                break;
            }
            case HFrame frame:
            {
                var fv = new FrameView
                {
                    Width = Dim.Fill(),
                    Y = offsets.offsetY
                };

                var startOffset = (0, 0);
                foreach (var item in frame.Elements) startOffset = ShowH(fv, item.Value, startOffset);

                offsets.offsetX += startOffset.Item1;
                offsets.offsetY += startOffset.Item2 + 2;

                fv.Height = startOffset.Item2 + 2;

                view.Add(fv);

                break;
            }
            case Label lb:
            {
                var label = new Terminal.Gui.Label
                {
                    Text = lb.Text.T,
                    Width = Dim.Fill(),
                    Height = 1,
                    Y = offsets.offsetY
                };
                offsets.offsetY++;
                view.Add(label);
                break;
            }
            case Button bt:
            {
                var label = new Terminal.Gui.Button
                {
                    Text = bt.Text.T,
                    Width = Dim.Fill(),
                    Height = 1,
                    Y = offsets.offsetY
                };
                offsets.offsetY++;
                label.Clicked += () => { bt.OnClicked(); };

                view.Add(label);
                break;
            }
            default:
                throw new NotImplementedException($"Not supported gui element: {element.GetType()}");
        }

        return offsets;
    }
}