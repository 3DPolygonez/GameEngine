using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Framework.Support
{
    public static class Angle
    {
        public static double DegreesToRadians(
            double degrees)
        {
            // radians = (2 x pi) / (360 / degrees)
            return (Math.PI / 180) * degrees;
        }
    }
}
