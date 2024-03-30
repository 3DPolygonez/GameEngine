using System;

namespace Planetfall.Screen
{
    public class Loading : GameEngine.Framework.Interface.Screen
    {
        public Loading(
            GameEngine.Framework.Interface.Game game) : base(game)
        {
            // nothing to do here
            return;
        }
        public override void update(
            long deltaTime)
        {
            // loads assets
            Asset.List.text = base.game.graphics.newPixmap(@"Asset/Graphics/Text/text.png");
            Asset.List.mainMenuBackground = base.game.graphics.newPixmap(@"Asset/Graphics/Background/menu_bg1.png");

            // load cursors
            Asset.List.mainMenuCursor = @"Asset/Graphics/Cursor/MainMenu.ani";
            Asset.List.playCursor = @"Asset/Graphics/Cursor/Play.cur";

            // push the game through to the main menu screen
            this.game.screen = new MainMenu(
                game: this.game);
            return;
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
        public override void dispose()
        {
            throw new NotImplementedException();
        }
    }
}
