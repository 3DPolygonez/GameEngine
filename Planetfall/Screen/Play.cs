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
        private int _size = 0;
        private int _increment = 1;
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
            game.graphics.drawRect(
                x: 0, 
                y: 0, 
                width: game.graphics.width, 
                height: game.graphics.height,
                brush: Brushes.Black,
                pen: null);
            if (_size < 10)
            {
                _size = 10;
                _increment = 1;
            }
            else if (_size > game.graphics.width)
            {
                _increment = -1;
            }
            _size += _increment;
            game.graphics.drawRect(
                x: (game.graphics.width / 2) - (_size / 2),
                y: (game.graphics.height / 2) - (_size / 2),
                width: _size,
                height: _size,
                brush: null,
                pen: Pens.Orange);
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
            if (game.input.isKeyPressed(keyCode: (int)Keys.Escape))
            {
                // return to menu
                game.screen = new MainMenu(
                    game: game);
            }
        }
    }
}
