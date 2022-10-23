using UnityEngine;
using System.Threading.Tasks;

public class MVUnityClient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {        
        Debug.Log("Starting");

        System.Reflection.Assembly ass = 
            System.Reflection.Assembly.LoadFile(@"C:\Users\Gal\AppData\Local\Temp\3cacf86b-8269-41de-b9cf-0774c1bcf082-verse.dll"); 

        Debug.Log(ass.GetName());

        // MV3d metaVerse = new MV3d();    
        // var c = new MV.Client.MVClient(metaVerse, true);
        // c.Init().Wait();
        // c.Start().Wait();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
