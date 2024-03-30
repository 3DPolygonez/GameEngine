namespace Ranger.Model.Play
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public class Light : GameObject
    {
        public Light(
            int x,
            int y,
            int width,
            int height) : base(maxSpeed:0, distancePerSecond:0, x:x, y:y, width:width, height:height, depth:0, heightFromGround:0)
        {
           
            return;
        }

        public override void drawShadow(GameEngine.Framework.Interface.Game game)
        {
            return;
        }

        public override void draw(
            GameEngine.Framework.Interface.Game game)
        {
            for (int i = 10; i > 0; i--)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(5 * i, Color.White));
                game.graphics.drawEllipse(
                    this.x - i,
                    this.y - i,
                    i * 2,
                    i * 2,
                    brush,
                    null);
            }

            return;
        }
    }
}
