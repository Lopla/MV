using MV.OneD;
using Terminal.Gui;

var navigationMenu = new NavigcationMenu();

Application.Init ();
var label = new Label ("Hello World") {
            X = Pos.Center (),
            Y = Pos.Center (),
            Height = 1,
        };
Application.Top.Add (label);
Application.Run ();
Application.Shutdown ();

var commandLineApplication = new CommandLineApplication();
