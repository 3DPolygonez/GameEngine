using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Framework.Support
{
    public static class Color
    {
        public static System.Drawing.Color modifyColorLight(System.Drawing.Color color, double lightValue)
        {
            byte r = color.R;
            byte g = color.G;
            byte b = color.B;
            if (r != 0 && r + r * lightValue <= 255)
            {
                r = (byte)(r + r * lightValue);
            }
            else if (r != 0)
            {
                r = (byte)255;
            }
            if (g != 0 && g + g * lightValue <= 255)
            {
                g = (byte)(g + g * lightValue);
            }
            else if (g != 0)
            {
                g = (byte)255;
            }
            if (b != 0 && b + b * lightValue <= 255)
            {
                b = (byte)(b + b * lightValue);
            }
            else if (b != 0)
            {
                b = (byte)255;
            }
            return System.Drawing.Color.FromArgb(r, g, b);
        }
    }
}
