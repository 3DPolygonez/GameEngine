namespace Ranger.Model.Play
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public abstract class GameObject  
    {
        private double _x;
        private double _y;

        public int x 
        {
            get
            {
                return (int)this._x;
            }
        }
        public int y 
        {
            get
            {
                return (int)this._y;
            }
        }
        public int directionX { get; set; }
        public int directionY { get; set; }
        public double detailHeightFromGround{ get; set; }
        public double maxSpeed { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int depth { get; set; }
        public int groundLevel { get; set; }
        public int distancePerSecond { get; set; }
        public int heightFromGround 
        {
            get
            {
                return (int)this.detailHeightFromGround;
            }
        }
        public double xSpeed { get; set; }
        public double ySpeed { get; set; }
        public double xSpeedNoAnimateAdjustment { get; set; }
        public double ySpeedNoAnimateAdjustment { get; set; }

        public Rectangle screenPosition { get; set; }
        public int temp { get; set; }
        
        public GameObject(
            double maxSpeed,
            int distancePerSecond,
            int x,
            int y,
            int width,
            int height,
            int depth,
            int heightFromGround) : base()
        {
            this.maxSpeed = maxSpeed;
            this._x = x;
            this._y = y;
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.detailHeightFromGround = heightFromGround;
            this.distancePerSecond = distancePerSecond;
            return;
        }

        public double maxDiagonalMove
        {
            get
            {
                 return Math.Sqrt(maxSpeed * maxSpeed / 2);
            }
        }

        public void move()
        {
            if ((xSpeed != 0 || xSpeedNoAnimateAdjustment != 0) && ySpeed == 0 || xSpeed == 0 && (ySpeed != 0 || ySpeedNoAnimateAdjustment != 0))
            {
                this._x += (xSpeed + xSpeedNoAnimateAdjustment);
                this._y += (ySpeed + ySpeedNoAnimateAdjustment);
            }
            else
            {
                this._x += (xSpeed + xSpeedNoAnimateAdjustment) * (this.maxDiagonalMove / this.maxSpeed);
                this._y += (ySpeed + ySpeedNoAnimateAdjustment) * (this.maxDiagonalMove / this.maxSpeed);
            }
            xSpeedNoAnimateAdjustment = 0;
            ySpeedNoAnimateAdjustment = 0;
            return;
        }

        public void setScreenPosition(
            Rectangle screenPosition,
            Rectangle screen)
        {
            int screenPositionX;
            int screenPositionY;

            if (screenPosition.X != 0)
            {
                screenPositionX = this.x + screenPosition.X;
            }
            else
            {
                screenPositionX = this.x - screen.X;
            }
            if (screenPosition.Y != 0)
            {
                screenPositionY = this.y - this.depth + screenPosition.Y;
            }
            else
            {
                screenPositionY = this.y - this.depth - screen.Y;
            }

            this.screenPosition = new Rectangle(
                screenPositionX,
                screenPositionY + this.depth,
                this.width, 
                this.depth);
            return;
        }

        public abstract void draw(
            GameEngine.Framework.Interface.Game game);
        public abstract void drawShadow(
            GameEngine.Framework.Interface.Game game);

        public int getEightCellPositionBasedOnAngle(
            double angle)
        {
            if (angle >= 338 || angle < 23)
            {
                // north
                return 6;
            }
            else if (angle >= 23 && angle < 68)
            {
                // north east
                return 7;
            }
            else if (angle >= 68 && angle < 113)
            {
                // east
                return 0;
            }
            else if (angle >= 113 && angle < 158)
            {
                // south east
                return 1;
            }
            else if (angle >= 158 && angle < 203)
            {
                // south
                return 2;
            }
            else if (angle >= 203 && angle < 248)
            {
                // south west
                return 3;
            }
            else if (angle >= 248 && angle < 293)
            {
                // west
                return 4;
            }
            else
            {
                // north west
                return 5;
            }
        }
    }
}
