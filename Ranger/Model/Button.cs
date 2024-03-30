namespace Ranger.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public class Button
    {
        public string text;
        public int x;
        public int y;
        public int width;
        public int height;
        public bool mouseOver = false;
        public bool mouseLeftDown = false;
        public bool mouseOverSoundPlayed = false;

        public Button(string text, int x, int y, int width, int height)
        {
            this.text = text;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            return;
        }
        public void draw(GameEngine.Framework.Interface.Game game)
        {
            Brush brush;

            if (!this.mouseLeftDown)
            {
                brush = Brushes.Orange;
            }
            else
            {
                brush = Brushes.Silver;
            }

            game.graphics.drawRect(
                this.x,
                this.y,
                this.width,
                this.height,
                brush,
                null);
            game.graphics.drawText(
                this.text,
                game.graphics.width / 2 - ((this.text.Length * Asset.Constants.letterWidth) / 2),
                this.y + 16,
                Ranger.Asset.List.text,
                Asset.Constants.letterWidth,
                Asset.Constants.letterHeight);
            if (this.mouseOver)
            {
                if (!this.mouseOverSoundPlayed)
                {
                    this.mouseOverSoundPlayed = true;
                    Asset.List.select.play(0);
                }
                game.graphics.drawRect(
                    this.x - 2,
                    this.y - 2,
                    this.width + 3,
                    this.height + 3,
                    null,
                    Pens.Orange);
            }
            else
            {
                this.mouseOverSoundPlayed = false;
            }
        }
    }
}
