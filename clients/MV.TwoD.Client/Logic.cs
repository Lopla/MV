using Home;

namespace MV.TwoD
{
    /// <summary>
    /// Base class for handling logic delivered by verses (virtual worlds)
    /// </summary>
    public class Logic
    {
        private HomeManifest mv;

        public Logic()
        {
            this.mv = new Home.HomeManifest();
            //1 load verse
            //2 show gui
            //3 interact
        }

        public void GameLoop()
        {
            //verse logic to handle gameloop
            //as in graphical engines
        }

        public void Events()
        {
            //events loop
        }


    }
}