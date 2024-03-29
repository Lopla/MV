using UnityEngine;
using System.Threading.Tasks;

public class MVUnityClient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {        
        Debug.Log("Starting");
        
        MV3d metaVerse = new MV3d();    
        var c = new MV.Client.MVClient(metaVerse, new NotSepartedContext(),true);
        c.Init().Wait();
        c.Start().Wait();
    }

    // Update is called once per frame
    void Update()
    {
    }
}