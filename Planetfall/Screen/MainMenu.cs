using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Planetfall.Screen
{
    public class MainMenu : GameEngine.Framework.Interface.Screen
    {
        private readonly Model.MainMenu.World _world;
        public MainMenu(
            GameEngine.Framework.Interface.Game game) : base(game)
        {
            // create a new main menu model
            _world = new Model.MainMenu.World(
                screenWidth: this.game.graphics.width, 
                screenHeight: this.game.graphics.height);

            // set the cursor
            this.game.setCursor(Asset.List.mainMenuCursor);
            return;
        }
        public override void update(long deltaTime)
        {
            // update the main menu model
            this._world.update(game, deltaTime);

            // see if any of the buttons have been pushed
            if (_world.buttons[0].mouseLeftDown)
            {
                // push the game through to the instruction screen
                this.game.close();
            }
            return;
        }

        public override void draw(long deltaTime)
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
                _world.buttons[0].x - Asset.Constants.buttonHeight,
                _world.buttons[0].y - Asset.Constants.buttonHeight,
                _world.buttons[0].width + (Asset.Constants.buttonHeight * 2),
                _world.buttons[0].y - _world.buttons[0].y + (Asset.Constants.buttonHeight * 3),
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
