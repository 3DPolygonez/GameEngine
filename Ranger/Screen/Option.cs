using System;
using System.Collections.Generic;
using System.Text;

namespace Ranger.Screen
{
    public class Option : GameEngine.Framework.Interface.Screen
    {
        public Option(GameEngine.Framework.Interface.Game game) : base(game)
        {
            // set the cursor
            this.game.setCursor(Ranger.Asset.List.mainMenuCursor);
            return;
        }

        public override void update(long deltaTime)
        {
            return;
        }

        public override void draw(long deltaTime)
        {
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
