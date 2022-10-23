using MV.IDE;
using SDK.Verse;

bool oneDimension = true;

if (oneDimension)
{
    await Clients.StartOneD(new VerseManifest());
}
else
{
    await Clients.StartTwoD(new VerseManifest());
}

