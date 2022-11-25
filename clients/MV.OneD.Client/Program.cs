using MV.Forms;
using MV.OneD;

var frame = new HFrame();
frame.Add(new Button(){Text="a"});
frame.Add(new Label(){Text="B"});
frame.Add(new Button(){Text="c"});
frame.Add(new Label(){Text="D"});
frame.Add(new Button(){Text="e"});
frame.Add(new Label(){Text="F"});

var tr = new TerminalRender();
tr.Show(frame);

await tr.Start();

//await CommandLineApplication.MainCode(args);

return 0;
