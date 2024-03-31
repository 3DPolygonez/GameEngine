using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using GameEngine.Framework.Interface;
using static Planetfall.Model.MainMenu.World;

namespace Planetfall.Screen
{
    public class MainMenu : GameEngine.Framework.Interface.Screen
    {
        private readonly Model.MainMenu.World _world;
        public MainMenu(
            Game game) : base(game)
        {
            // create a new main menu model
            _world = new Model.MainMenu.World(
                screenWidth: this.game.graphics.width, 
                screenHeight: this.game.graphics.height);

            // set the cursor
            this.game.setCursor(Asset.List.mainMenuCursor);
            return;
        }
        public override void update(
            long deltaTime)
        {
            // update the main menu model
            this._world.update(
                game: game,
                deltaTime: deltaTime);

            // see if any of the buttons have been pushed
            // see if any of the buttons have been pushed
            if (_world.buttons[(int)ButtonName.Play].mouseLeftDown)
            {
                // push the game through to the play screen
                this.game.screen = new Play(
                    game: this.game);
            }
            else if (_world.buttons[(int)ButtonName.About].mouseLeftDown)
            {
                // push the game through to the about screen
                this.game.screen = new About(
                    game: this.game);
            }
            else if (_world.buttons[(int)ButtonName.Quit].mouseLeftDown)
            {
                // close the game
                this.game.close();
            }
            return;
        }
        public override void draw(
            long deltaTime)
        {
            // draw the main menu background
            int xCount = this.game.graphics.width / Asset.List.mainMenuBackground.width;
            int yCount = this.game.graphics.height / Asset.List.mainMenuBackground.height;

            for (int x = 0; x <= xCount; x++)
            {
                for (int y = 0; y <= yCount; y++)
                {
                    this.game.graphics.drawPixmap(
                        Asset.List.mainMenuBackground,
                        x * Asset.List.mainMenuBackground.width,
                        y * Asset.List.mainMenuBackground.height);
                }
            }

            this.game.graphics.drawRect(
                _world.buttons[(int)ButtonName.Play].x - Asset.Constants.buttonHeight,
                _world.buttons[(int)ButtonName.Play].y - Asset.Constants.buttonHeight,
                _world.buttons[(int)ButtonName.Play].width + (Asset.Constants.buttonHeight * 2),
                Asset.Constants.buttonHeight * 6,
                Brushes.Black,
                null);

            // draw title
            this.game.graphics.drawText(
                "planetfall",
                this.game.graphics.width / 2 - (("planetfall".Length * Asset.Constants.letterWidth) / 2),
                Asset.Constants.letterHeight,
                Asset.List.text,
                Asset.Constants.letterWidth,
                Asset.Constants.letterHeight);

            // draw buttons
            for (int i = 0; i < this._world.buttons.Count; i++)
            {
                this._world.buttons[i].draw(this.game);
            }
            return;
        }
        public override void pause()
        {
            throw new NotImplementedException();
        }
        public override void resume()
        {
            throw new NotImplementedException();
        }
        public override void dispose()
        {
            throw new NotImplementedException();
        }
    }
}
