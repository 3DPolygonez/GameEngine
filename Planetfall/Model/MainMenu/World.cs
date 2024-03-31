using System;
using System.Collections.Generic;
using System.Text;

namespace Planetfall.Model.MainMenu
{
    public class World
    {
        private List<Button> _buttons;
        public enum ButtonName : int
        {
            Play = 0,
            About = 1,
            Quit = 2
        }
        public World(
            int screenWidth,
            int screenHeight)
        {
            int buttonStart = screenHeight / 2 - (int)(5.5 * Asset.Constants.buttonHeight) / 2;

            this._buttons = new List<Button>();
            this._buttons.Add(
                item: new Button(
                    text: "play",
                    x: screenWidth / 2 - Asset.Constants.buttonWidth / 2,
                    y: buttonStart + (int)(0 * Asset.Constants.buttonHeight),
                    width: Asset.Constants.buttonWidth,
                    height: Asset.Constants.buttonHeight));
            this._buttons.Add(
                item: new Button(
                    text: "about",
                    x: screenWidth / 2 - Asset.Constants.buttonWidth / 2,
                    y: buttonStart + (int)(1.5 * Asset.Constants.buttonHeight),
                    width: Asset.Constants.buttonWidth,
                    height: Asset.Constants.buttonHeight));
            this._buttons.Add(
                item: new Button(
                    text: "quit game", 
                    x: screenWidth / 2 - Asset.Constants.buttonWidth / 2, 
                    y: buttonStart + (int)(3.0 * Asset.Constants.buttonHeight), 
                    width: Asset.Constants.buttonWidth, 
                    height: Asset.Constants.buttonHeight));
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
