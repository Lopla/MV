using MV.IDE;

bool oneDimension = true;

if (oneDimension)
{
    await Clients.StartOneD(null);
}
else
{
    await Clients.StartTwoD(null);
}

