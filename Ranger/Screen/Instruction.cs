using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Ranger.Screen
{
    public class Instruction : GameEngine.Framework.Interface.Screen
    {
        private Model.Instruction.World _world;

        public Instruction(GameEngine.Framework.Interface.Game game) : base(game)
        {
            // create a new main menu model
            _world = new Model.Instruction.World(this.game.graphics.width, this.game.graphics.height);

            // set the cursor
            this.game.setCursor(Ranger.Asset.List.mainMenuCursor);
            return;
        }

        public override void update(long deltaTime)
        {
            // update the instruction model
            this._world.update(game, deltaTime);

            // see if any of the buttons have been pushed
            if (_world.buttons[0].mouseLeftDown)
            {
                // push the game through to the main menu screen
                this.game.screen = new MainMenu(this.game);
            }
            return;
        }

        public override void draw(long deltaTime)
        {
            // draw the main menu background
            this.game.graphics.drawRect(0, 0, this.game.graphics.width, this.game.graphics.height, Brushes.Black, null);
            
            // draw title
            this.game.graphics.drawText(
                "instructions",
                this.game.graphics.width / 2 - (("instructions".Length * Asset.Constants.letterWidth) / 2),
                Asset.Constants.letterHeight,
                Ranger.Asset.List.text,
                Asset.Constants.letterWidth,
                Asset.Constants.letterHeight);

            this.game.graphics.drawText(
                "go out",
                this.game.graphics.width / 2 - (("go out".Length * Asset.Constants.letterWidth) / 2),
                Asset.Constants.letterHeight * 3,
                Ranger.Asset.List.text,
                Asset.Constants.letterWidth,
                Asset.Constants.letterHeight);

            this.game.graphics.drawText(
                "and come back again",
                this.game.graphics.width / 2 - (("and come back again".Length * Asset.Constants.letterWidth) / 2),
                Asset.Constants.letterHeight * 4,
                Ranger.Asset.List.text,
                Asset.Constants.letterWidth,
                Asset.Constants.letterHeight);

            this.game.graphics.drawText(
                "that is it",
                this.game.graphics.width / 2 - (("that is it".Length * Asset.Constants.letterWidth) / 2),
                Asset.Constants.letterHeight * 5,
                Ranger.Asset.List.text,
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
