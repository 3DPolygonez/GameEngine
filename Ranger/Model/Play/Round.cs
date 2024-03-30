namespace Ranger.Model.Play
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public class Round : GameObject
    {
        private Pen _roundPen = new Pen(Color.FromArgb(255, 255, 255));
        private SolidBrush _roundBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        private double _range = 0;
        private int _distanceTravelled = 0;

        public Round(
            int x,
            int y,
            int angleInDegrees) : base(maxSpeed:16, distancePerSecond:200, x:x, y:y, width:1, height:1, depth:1, heightFromGround:0)
        {
            this._range = 100;
            double multiplier = 0;
            if (angleInDegrees >= 0 && angleInDegrees < 90)
            {
                multiplier = (double)angleInDegrees / (double)90;
                this.xSpeed = base.maxSpeed * multiplier;
                this.ySpeed = -base.maxSpeed * (1 - multiplier);
            }
            else if (angleInDegrees >= 90 && angleInDegrees < 180)
            {
                multiplier = (double)(angleInDegrees - 90) / (double)90;
                this.xSpeed = base.maxSpeed * (1 - multiplier);
                this.ySpeed = base.maxSpeed * multiplier;
            }
            else if (angleInDegrees >= 180 && angleInDegrees < 270)
            {
                multiplier = (double)(angleInDegrees - 180) / (double)90;
                this.xSpeed = -base.maxSpeed * multiplier;
                this.ySpeed = base.maxSpeed * (1 - multiplier);
            }
            else if (angleInDegrees >= 270 && angleInDegrees < 360)
            {
                multiplier = (double)(angleInDegrees - 270) / (double)90;
                this.xSpeed = -base.maxSpeed * (1 - multiplier);
                this.ySpeed = -base.maxSpeed * multiplier;
            }
            return;
        }

        public override void drawShadow(GameEngine.Framework.Interface.Game game)
        {
        }

        public override void draw(
            GameEngine.Framework.Interface.Game game)
        {
            int x = this.screenPosition.X;
            int y = this.screenPosition.Y;
            int width = this.screenPosition.Width;
            int height = this.screenPosition.Height;
            this._distanceTravelled += 1;
            if (_distanceTravelled == 1)
            {
                x -= (int)this.maxSpeed / 2 + this.width / 2;
                y -= (int)this.maxSpeed / 2 + this.height / 2;
                width += (int)this.maxSpeed;
                height += (int)this.maxSpeed;
            }
            game.graphics.drawEllipse(
                x,
                y,
                width,
                height,
                this._roundBrush,
                this._roundPen);
            return;
        }

        public bool RangeExpired
        {
            get
            {
                return this._distanceTravelled > this._range;
            }
        }
    }
}
