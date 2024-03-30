using System;
using System.Collections.Generic;
using System.Text;

namespace Ranger.Screen
{
    public class Loading : GameEngine.Framework.Interface.Screen
    {
        public Loading(GameEngine.Framework.Interface.Game game) : base(game)
        {
            // nothing to do here
            return;
        }

        public override void update(long deltaTime) 
        {
            // loads sounds
            Ranger.Asset.List.blaster = base.game.audio.newSound(@"Asset/Sound/blaster.wav");
            Ranger.Asset.List.select = base.game.audio.newSound(@"Asset/Sound/select.wav");

            // load music
            Ranger.Asset.List.mainMenuTheme = base.game.audio.newMusic(@"Asset/Music/MainMenuTheme.mp3");
            Ranger.Asset.List.playTheme = base.game.audio.newMusic(@"Asset/Music/PlayTheme.mp3");

            // load images
            Ranger.Asset.List.mainMenuBackground = base.game.graphics.newPixmap(@"Asset/Graphics/Background/menu_bg1.png");
            Ranger.Asset.List.tileGrey = base.game.graphics.newPixmap(@"Asset/Graphics/Background/tile_grey.png");
            Ranger.Asset.List.tileDirt = base.game.graphics.newPixmap(@"Asset/Graphics/Background/tile_dirt.png");
            Ranger.Asset.List.text = base.game.graphics.newPixmap(@"Asset/Graphics/Text/text.png");
            
            Ranger.Asset.List.characterPlayerHead = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Player/fighter_head.png");
            Ranger.Asset.List.characterPlayerBody = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Player/fighter_body.png");
            Ranger.Asset.List.characterPlayerFoot = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Player/fighter_foot.png");
            Ranger.Asset.List.characterPlayerHand = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Player/fighter_hand.png");

            Ranger.Asset.List.ninjaPlayerHead = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Ninja/turtle_ninja_head.png");
            Ranger.Asset.List.ninjaPlayerBody = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Ninja/turtle_ninja_body.png");
            Ranger.Asset.List.ninjaPlayerFoot = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Ninja/turtle_ninja_foot.png");
            Ranger.Asset.List.ninjaPlayerHand = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Ninja/turtle_ninja_hand.png");

            Ranger.Asset.List.giantPlayerHead = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Giant/giant_head.png");
            Ranger.Asset.List.giantPlayerBody = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Giant/giant_body.png");
            Ranger.Asset.List.giantPlayerFoot = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Giant/giant_foot.png");
            Ranger.Asset.List.giantPlayerHand = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Giant/giant_hand.png");

            Ranger.Asset.List.fighterPlayerHead = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Fighter/fighter_head.png");
            Ranger.Asset.List.fighterPlayerBody = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Fighter/fighter_body.png");
            Ranger.Asset.List.fighterPlayerFoot = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Fighter/fighter_foot.png");
            Ranger.Asset.List.fighterPlayerHand = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Fighter/fighter_hand.png");

            Ranger.Asset.List.humanPlayerHead = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Human/human_head.png");
            Ranger.Asset.List.humanPlayerBody = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Human/human_body.png");
            Ranger.Asset.List.humanPlayerFoot = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Human/human_foot.png");
            Ranger.Asset.List.humanPlayerHand = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Human/human_hand.png");

            Ranger.Asset.List.powerArmourPlayerHead = base.game.graphics.newPixmap(@"Asset/Graphics/Character/PowerArmour/powerarmour_head.png");
            Ranger.Asset.List.powerArmourPlayerBody = base.game.graphics.newPixmap(@"Asset/Graphics/Character/PowerArmour/powerarmour_body.png");
            Ranger.Asset.List.powerArmourPlayerFoot = base.game.graphics.newPixmap(@"Asset/Graphics/Character/PowerArmour/powerarmour_foot.png");
            Ranger.Asset.List.powerArmourPlayerHand = base.game.graphics.newPixmap(@"Asset/Graphics/Character/PowerArmour/powerarmour_hand.png");

            Ranger.Asset.List.zombiePlayerHead = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Zombie/zombie_head.png");
            Ranger.Asset.List.zombiePlayerBody = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Zombie/zombie_body.png");
            Ranger.Asset.List.zombiePlayerFoot = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Zombie/zombie_foot.png");
            Ranger.Asset.List.zombiePlayerHand = base.game.graphics.newPixmap(@"Asset/Graphics/Character/Zombie/zombie_hand.png");

            // set cursors
            Ranger.Asset.List.mainMenuCursor = @"Asset/Graphics/Cursor/MainMenu.ani";
            Ranger.Asset.List.playCursor = @"Asset/Graphics/Cursor/Play.cur";

            // push the game through to the main menu screen
            this.game.screen = new MainMenu(this.game);
            return;
        }

        public override void draw(long deltaTime)
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
