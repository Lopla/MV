using MV.IDE;
using SDK.Verse;


bool oneDimension = false;

if (oneDimension)
{
    await Clients.StartOneD(new VerseManifest());
}
else
{
    await Clients.StartTwoD(new VerseManifest());
}

