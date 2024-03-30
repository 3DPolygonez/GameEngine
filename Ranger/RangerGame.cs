using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Ranger
{
    public class RangerGame : GameEngine.Framework.Implementation.Game
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new RangerGame());
        }

        public RangerGame() : base()
        {
            base.title = Application.ProductName;
            return;
        }

        public override GameEngine.Framework.Interface.Screen getStartScreen()
        {
            return new Ranger.Screen.Loading(this); 
        }
    }
}
