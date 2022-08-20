using MV.IDE;
using SDK.Verse;

var manifest = new TestVerse();

//await Clients.StartOneD(manifest);

await Clients.StartTwoD(manifest);

