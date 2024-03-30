namespace Ranger.Model.MainMenu
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
            int buttonStart = screenHeight / 2 - (int)(5.5 * Asset.Constants.buttonHeight) / 2;

            this._buttons = new List<Button>();
            this._buttons.Add(new Button("new game", screenWidth / 2 - Asset.Constants.buttonWidth / 2, buttonStart + (int)(0.0 * Asset.Constants.buttonHeight), Asset.Constants.buttonWidth, Asset.Constants.buttonHeight));
            this._buttons.Add(new Button("instructions", screenWidth / 2 - Asset.Constants.buttonWidth / 2, buttonStart + (int)(1.5 * Asset.Constants.buttonHeight), Asset.Constants.buttonWidth, Asset.Constants.buttonHeight));
            this._buttons.Add(new Button("options", screenWidth / 2 - Asset.Constants.buttonWidth / 2, buttonStart + (int)(3.0 * Asset.Constants.buttonHeight), Asset.Constants.buttonWidth, Asset.Constants.buttonHeight));
            this._buttons.Add(new Button("quit game", screenWidth / 2 - Asset.Constants.buttonWidth / 2, buttonStart + (int)(4.5 * Asset.Constants.buttonHeight), Asset.Constants.buttonWidth, Asset.Constants.buttonHeight));
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
