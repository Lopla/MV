using MV.IDE;
using SDK.Verse;

var manifest = new Verse1d();

//await Clients.StartOneD(manifest);

await Clients.StartTwoD(manifest);

