using MV.Client;
using Terminal.Gui;

namespace MV.OneD.UI
{
    public class NavigcationMenu
    {
        public NavigcationMenu(MV.Client.MVClient client)
        {
            Client = client;
        }

        public MVClient Client { get; }

        public void Setup(View root)
        {
            
        }
    }
}