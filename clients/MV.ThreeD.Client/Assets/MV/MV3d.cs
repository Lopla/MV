using System.Threading.Tasks;
using MV.Interfaces;
using UnityEngine;

internal class MV3d : IMetaVerse
{
    public MV3d()
    {
    }

    public async Task Init()
    {
        
    }

    public void Show(IElement form)
    {
        Debug.Log("Request to show: "+ form.ToString());
    }

    public async Task Start()
    {
    }
}