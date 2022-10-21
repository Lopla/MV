using MV.IDE;
using SDK.Verse;


bool OneDimension = true;

if (OneDimension)
{
    await Clients.StartOneD(new VerseManifest());
}
else
{
    await Clients.StartTwoD(new VerseManifest());
}

