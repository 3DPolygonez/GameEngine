using GameEngine.Framework.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planetfall.Screen
{
    public class About : GameEngine.Framework.Interface.Screen
    {
        private readonly Model.About.World _world;
        public About(
            Game game) : base(game)
        {
            // create a new about model
            _world = new Model.About.World(
                screenWidth: this.game.graphics.width,
                screenHeight: this.game.graphics.height);

            // set the cursor
            this.game.setCursor(Asset.List.mainMenuCursor);
            return;
        }
        public override void dispose()
        {
            throw new NotImplementedException();
        }
        public override void draw(
            long deltaTime)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
