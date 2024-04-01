using GameEngine.Framework.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planetfall.Screen
{
    public class Play : GameEngine.Framework.Interface.Screen
    {
        private readonly Model.Play.World _world;
        public Play(
            Game game) : base(game)
        {
            // create a new play model
            _world = new Model.Play.World(
                screenWidth: game.graphics.width,
                screenHeight: game.graphics.height);

            // set the cursor
            game.setCursor(Asset.List.playCursor);
            return;
        }
        public override void dispose()
        {
            throw new NotImplementedException();
        }
        public override void draw(
            long deltaTime)
        {
            _world.draw(
                game: game);
        }
        public override void pause()
        {
            throw new NotImplementedException();
        }
        public override void resume()
        {
            throw new NotImplementedException();
        }
        public override void update(
            long deltaTime)
        {
            _world.update(
                game: game);
            if (game.input.isKeyPressed(keyCode: (int)Keys.Escape))
            {
                // return to menu
                game.screen = new MainMenu(
                    game: game);
            }
        }
    }
}
