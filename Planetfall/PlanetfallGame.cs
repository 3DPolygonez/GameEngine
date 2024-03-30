using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Planetfall
{
    public class PlanetfallGame : GameEngine.Framework.Implementation.Game
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.Run(
                mainForm: new PlanetfallGame());
        }

        public PlanetfallGame() : base()
        {
            base.title = Application.ProductName;
            return;
        }

        public override GameEngine.Framework.Interface.Screen getStartScreen()
        {
            return new Planetfall.Screen.Loading(
                game: this);
        }
    }
}
