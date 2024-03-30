namespace Ranger.Model.Instruction
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class World
    {
        private List<Button> _buttons;

        public World(
            int screenWidth, 
            int screenHeight)
        {
            this._buttons = new List<Button>();
            this._buttons.Add(new Button("back to menu", screenWidth / 2 - Asset.Constants.buttonWidth / 2, (int)(screenHeight - 2.0 * Asset.Constants.buttonHeight), Asset.Constants.buttonWidth, Asset.Constants.buttonHeight));
            return;
        }

        public List<Button> buttons
        {
            get
            {
                return this._buttons;
            }
        }

        public void update(
            GameEngine.Framework.Interface.Game game, 
            float deltaTime)
        {
            for (int i = 0; i < this._buttons.Count; i++)
            {
                this._buttons[i].mouseOver = game.input.mouseInBounds(this._buttons[i].x, this._buttons[i].y, this._buttons[i].width, this._buttons[i].height);
                if (this._buttons[i].mouseOver)
                {
                    this._buttons[i].mouseLeftDown = game.input.mouseLeftButtonDown;
                }
                else
                {
                    this._buttons[i].mouseLeftDown = false;
                }
            }
            return;
        }
    }
}
