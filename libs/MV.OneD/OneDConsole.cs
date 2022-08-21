using MV.Forms;
using MV.Interfaces;
using Terminal.Gui;
using Button = MV.Forms.Button;
using Label = MV.Forms.Label;

namespace MV.OneD;

public class OneDConsole : IMetaVerse
{
    private View rootView;

    private Stack<View> viewParents = new();

    public OneDConsole()
    {
        Application.Init();
    }

    public void Show(IElement element)
    {
        // it always starts with the window
        var w = new Window
        {
            Width = Dim.Percent(50),
            Height = Dim.Percent(50)
        };

        Show(w, element, (0,0));

        Application.Top.Add(w);
    }

    public Task Start()
    {
        Application.Run();
        Application.Shutdown();

        return Task.CompletedTask;
    }

    public Task Init()
    {
        return Task.CompletedTask;
    }

    private (int offsetX, int offsetY) Show(View view, IElement element, (int offsetX, int offsetY) offsets)
    {
        switch (element)
        {
            case Frame frame:
            {
                var fv = new FrameView
                {
                    Width = Dim.Fill(),
                    Y = offsets.offsetY
                };

                var startOffset = (0, 0);
                foreach (var item in frame.Elements)
                {
                    startOffset = Show(fv, item.Value, startOffset);
                }

                offsets.offsetX += startOffset.Item1 ;
                offsets.offsetY += startOffset.Item2 +2;

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