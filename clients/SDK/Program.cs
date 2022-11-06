using MV.IDE;
using MV.Interfaces;
using SDK.Verse;

IManifest manifest =
    null;
    //new VerseManifest();

if (false)
{
    await Clients.StartOneD(manifest);
}
else
{
    await Clients.StartTwoD(manifest);
}

