namespace Ranger.Model.Play 
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public class Block : Model.Play.GameObject
    {
        private Pen _topEdgePen;
        private Pen _sideEdgePen;
        private Pen _sideRightEdgePen;
        private SolidBrush _topBrush;
        private SolidBrush _sideBrush;
        private SolidBrush _shadowBrush;
        private SolidBrush _topShadowBrush;
        private Color _backgroundColor;
        private bool _showLeftEdge;
        private bool _showRightEdge;
        private bool _showTopEdge;
        private bool _showBottomEdge;
        private int _maxLeftEdgeHeight;
        private int _maxRightEdgeHeight;
        private Point[] _shadowMap;

        public Block(
            int x,
            int y,
            int width,
            int height,
            int depth,
            int heightFromGround,
            bool showLeftEdge,
            bool showRightEdge,
            bool showTopEdge,
            bool showBottomEdge,
            int maxLeftEdgeHeight,
            int maxRightEdgeHeight,
            Point[] shadowMap,
            Color topColor,
            Color sideColor,
            Color backgroundColor)
            : base(maxSpeed: 0, distancePerSecond: 0, x: x, y: y, width: width, height: height, depth: depth, heightFromGround: heightFromGround)
        {
            this._maxLeftEdgeHeight = maxLeftEdgeHeight;
            this._maxRightEdgeHeight = maxRightEdgeHeight;
            this._showLeftEdge = showLeftEdge;
            this._showRightEdge = showRightEdge;
            this._showTopEdge = showTopEdge;
            this._showBottomEdge = showBottomEdge;
            this._shadowMap = shadowMap;
            this._backgroundColor = backgroundColor;
            this._topEdgePen = new Pen(GameEngine.Framework.Support.Color.modifyColorLight(topColor, 0.25));
            this._sideEdgePen = new Pen(GameEngine.Framework.Support.Color.modifyColorLight(topColor, 0.15));
            this._sideRightEdgePen = new Pen(GameEngine.Framework.Support.Color.modifyColorLight(sideColor, -0.40));
            this._topBrush = new SolidBrush(GameEngine.Framework.Support.Color.modifyColorLight(topColor, 0.10));
            this._sideBrush = new SolidBrush(GameEngine.Framework.Support.Color.modifyColorLight(sideColor, -0.25));
            this._shadowBrush = new SolidBrush(GameEngine.Framework.Support.Color.modifyColorLight(this._backgroundColor, -0.1));
            this._topShadowBrush = new SolidBrush(GameEngine.Framework.Support.Color.modifyColorLight(topColor, -0.05));
            return;
        }

        public override void drawShadow(
            GameEngine.Framework.Interface.Game game)
        {
            // draw ground shadow
            const double half = 30000;
            const double whole = 60000;
            const double maxWidth = 4;
            double maxHeight = 1;

            double portionThroughSecond = 0;
            double widthPortionDivisor = 1;
            double heightPortionDivisor = 1;
            int milliSecond = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;

            if (DateTime.Now.Minute <= 30)
            {
                maxHeight = maxHeight * (DateTime.Now.Minute / 30.00);
            }
            else
            {
                maxHeight = maxHeight * ((60 - DateTime.Now.Minute) / 30.00);
            }

            if (milliSecond <= half)
            {
                portionThroughSecond = milliSecond / half;
                widthPortionDivisor = -maxWidth * ((half - milliSecond) / half);
                heightPortionDivisor = maxHeight * (milliSecond / half);
            }
            else
            {
                portionThroughSecond = ((whole - milliSecond) / half);
                widthPortionDivisor = maxWidth * ((milliSecond - half) / half);
                heightPortionDivisor = maxHeight * ((whole - milliSecond) / half);
            }

            int widthFromGroundPortionDivisor = (int)(this.heightFromGround * widthPortionDivisor);
            int heightFromGroundPortionDivisor = (int)(this.heightFromGround * heightPortionDivisor);
            int widthPortion = (int)(this.height * widthPortionDivisor);
            int heightPortion = (int)(this.height * heightPortionDivisor);

            Point[] shadow;
            if (milliSecond <= half)
            {
                shadow = new Point[6];
                shadow[0] = new Point(
                    x: this.screenPosition.X + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + heightFromGroundPortionDivisor);
                shadow[1] = new Point(
                    x: this.screenPosition.X + this.width + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + heightFromGroundPortionDivisor);
                shadow[2] = new Point(
                    x: this.screenPosition.X + this.width + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + this.screenPosition.Height + heightFromGroundPortionDivisor);
                shadow[3] = new Point(
                    x: this.screenPosition.X + this.width + widthPortion + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + this.screenPosition.Height + heightPortion + heightFromGroundPortionDivisor);
                shadow[4] = new Point(
                    x: this.screenPosition.X + widthPortion + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + this.screenPosition.Height + heightPortion + heightFromGroundPortionDivisor);
                shadow[5] = new Point(
                    x: this.screenPosition.X + widthPortion + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + heightPortion + heightFromGroundPortionDivisor);
            }
            else
            {
                shadow = new Point[6];
                shadow[0] = new Point(
                    x: this.screenPosition.X + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + heightFromGroundPortionDivisor);
                shadow[1] = new Point(
                    x: this.screenPosition.X + this.width + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + heightFromGroundPortionDivisor);
                shadow[2] = new Point(
                    x: this.screenPosition.X + this.width + widthPortion + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + heightPortion + heightFromGroundPortionDivisor);
                shadow[3] = new Point(
                    x: this.screenPosition.X + this.width + widthPortion + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + this.screenPosition.Height + heightPortion + heightFromGroundPortionDivisor);
                shadow[4] = new Point(
                    x: this.screenPosition.X + widthPortion + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + this.screenPosition.Height + heightPortion + heightFromGroundPortionDivisor);
                shadow[5] = new Point(
                    x: this.screenPosition.X + widthFromGroundPortionDivisor,
                    y: this.screenPosition.Y + this.screenPosition.Height + heightFromGroundPortionDivisor);
            }
            //this._shadowBrush = new SolidBrush(GameEngine.Framework.Support.Color.modifyColorLight(this._backgroundColor, -0.1 * portionThroughSecond));

            game.graphics.drawPoly(
                shadow,
                this._shadowBrush,
                null);
            return;
        }

        public override void draw(
            GameEngine.Framework.Interface.Game game)
        {
            // draw top surface
            game.graphics.drawRect(
                this.screenPosition.X,
                this.screenPosition.Y - this.height - this.heightFromGround,
                this.width,
                this.depth,
                this._topBrush,
                null);

            // draw side surface
            game.graphics.drawRect(
                this.screenPosition.X,
                this.screenPosition.Y + this.depth - this.height - this.heightFromGround,
                this.width,
                this.height,
                this._sideBrush,
                null);

            if (this._shadowMap.Length > 1)
            {
                Point[] shadowPoly = new Point[this._shadowMap.Length];
                for (int shadowPolyIndex = 0; shadowPolyIndex < shadowPoly.Length; shadowPolyIndex++)
                {
                    shadowPoly[shadowPolyIndex] = new Point(this._shadowMap[shadowPolyIndex].X, this._shadowMap[shadowPolyIndex].Y);
                }
                // draw top shadow
                game.graphics.drawPoly(
                    points: shadowPoly,
                    offset: new Point(this.screenPosition.X, this.screenPosition.Y - this.height),
                    brush: this._topShadowBrush,
                    pen: null);
            }

            if (this._showLeftEdge)
            {
                // draw left top edge highlight
                game.graphics.drawLine(
                    this.screenPosition.X,
                    this.screenPosition.Y - this.height - this.heightFromGround,
                    this.screenPosition.X,
                    this.screenPosition.Y - this.height + this.screenPosition.Height - this.heightFromGround,
                    this._topEdgePen);

                // draw side left edge highlight
                game.graphics.drawLine(
                    this.screenPosition.X,
                    this.screenPosition.Y + this.depth - this.height - this.heightFromGround + 1,
                    this.screenPosition.X,
                    this.screenPosition.Y + this.depth - this.height + this._maxLeftEdgeHeight - this.heightFromGround - 1,
                    this._sideEdgePen);
            }

            if (this._showRightEdge)
            {
                // draw right top edge highlight
                game.graphics.drawLine(
                    this.screenPosition.X + this.width,
                    this.screenPosition.Y - this.height - this.heightFromGround + 1,
                    this.screenPosition.X + this.width,
                    this.screenPosition.Y - this.height + this.screenPosition.Height - this.heightFromGround,
                    this._sideRightEdgePen);

                // draw side right edge highlight
                game.graphics.drawLine(
                    this.screenPosition.X + this.width,
                    this.screenPosition.Y + this.depth - this.height - this.heightFromGround,
                    this.screenPosition.X + this.width,
                    this.screenPosition.Y + this.depth - this.height + this._maxRightEdgeHeight - this.heightFromGround - 1,
                    this._sideRightEdgePen);
            }

            if (this._showBottomEdge)
            {
                // draw bottom surface top edge highlight
                game.graphics.drawLine(
                    this.screenPosition.X + 1,
                    this.screenPosition.Y + this.screenPosition.Height - this.height - this.heightFromGround,
                    this.screenPosition.X + this.screenPosition.Width,
                    this.screenPosition.Y + this.screenPosition.Height - this.height - this.heightFromGround,
                    this._sideRightEdgePen);

                // draw top surface top edge highlight
                game.graphics.drawLine(
                    this.screenPosition.X + 1,
                    this.screenPosition.Y + this.screenPosition.Height - this.heightFromGround,
                    this.screenPosition.X + this.screenPosition.Width,
                    this.screenPosition.Y + this.screenPosition.Height - this.heightFromGround,
                    this._sideRightEdgePen);

            }

            if (this._showTopEdge)
            {
                // draw top surface bottom edge highlight
                game.graphics.drawLine(
                    this.screenPosition.X,
                    this.screenPosition.Y - this.height - this.heightFromGround,
                    this.screenPosition.X + this.screenPosition.Width - 1,
                    this.screenPosition.Y - this.height - this.heightFromGround,
                    this._topEdgePen);
            }
            return;
        }
    }
}
