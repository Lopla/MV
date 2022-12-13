using MV.Forms;
using MV.OneD;

Frame AddButtons(Frame hFrame)
{
    hFrame.Add(new Button() {Text = "abcdfrergddddddddddddddddddeer"});
    hFrame.Add(new Label() {Text = "B"});
    hFrame.Add(new Button() {Text = "c"});
    hFrame.Add(new Label() {Text = "D"});
    hFrame.Add(new Button() {Text = "e"});
    hFrame.Add(new Label() {Text = "F"});
    return hFrame;
}

var frame = new HFrame();
frame.Add(AddButtons(new VFrame()));
// frame.Add(AddButtons(new VFrame()));
// frame.Add(AddButtons(new HFrame()));
// frame.Add(AddButtons(new HFrame()));
AddButtons(frame);
var tr = new TerminalRender();
tr.Show(frame);

await tr.Start();

//await CommandLineApplication.MainCode(args);

return 0;
