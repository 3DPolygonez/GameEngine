using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Ranger.Screen
{
    public class Play : GameEngine.Framework.Interface.Screen
    {
        private Model.Play.World _world;
        
        public Play(GameEngine.Framework.Interface.Game game, int level) : base(game)
        {
            // start playing the main menu music
            //Ranger.Asset.List.mainMenuTheme.stop();
            //Ranger.Asset.List.playTheme.play();

            // create a new main menu model
            this._world = new Model.Play.World(this.game.graphics.width, this.game.graphics.height, level);

            // set the cursor
            this.game.setCursor(Ranger.Asset.List.playCursor);
            return;
        }

        public override void update(
            long deltaTime)
        {
            // call the update method on the game model
            this._world.update(
                deltaTime,
                this.game);
            return;
        }

        public override void draw(
            long deltaTime)
        {
            // call the draw methid on the game model
            this._world.draw(
                this.game);
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
