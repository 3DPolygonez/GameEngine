using System;
using System.Collections.Generic;
using System.Text;

namespace Ranger.Model.MainMenu
{
    public class Button
    {
        public string text;
        public int x;
        public int y;
        public int width;
        public int height;
        public Boolean mouseOver = false;
        public Boolean mouseLeftDown = false;

        public Button(string text, int x, int y, int width, int height)
        {
            this.text = text;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            return;
        }
    }
}
