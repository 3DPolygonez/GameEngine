namespace Ranger.Model.Play
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;
    using System.Windows.Forms;

    public class World
    {
        private Rectangle _map;
        private Rectangle _screen;
        private Character _player;
        private List<Character> _characters;
        private List<Light> _lights;
        private List<Block> _walls;
        private List<Round> _rounds = new List<Round>();
        private Rectangle _screenPosition;
        public float _currentTime = 0.0f;
        private const int WallWidth = 50;
        private const int WallHeight = 25;
        private const int WallDepth = 50;
        private Color _wallTopColor = Color.DarkGray;
        private Color _wallSideColor = Color.DarkGray;
        private Color _mapColor = Color.FromArgb(115, 87, 50);
        //private Brush _x = new TextureBrush();
        private Pen _mapPen = new Pen(Color.LimeGreen);
        private bool _objectSorted = false;
        private GameObject[] _gameObjects;
        private long _delta = 0;

        public World(
            int screenWidth, 
            int screenHeight, 
            int level)
        {
            // define the size of this game world
            this._map = new Rectangle(
                0, 
                0, 
                2000,
                2000);

            // define the game player
            this._player = new Character(
                characterType: Character.CharacterType.Prisoner,
                maxSpeed: 4,
                x: 50,
                y: 50, 
                width: 20, 
                height: 20, 
                depth: 12,
                maxJumpHeight: (int)(WallHeight * 1.5),
                weapon: new Weapon(Weapon.WeaponType.SubmachineGun));
            this._player.changeWeapon(Weapon.WeaponType.SubmachineGun);

            // work out where the player is within the current screen
            this._screen = new Rectangle(
                this._player.x + (this._player.width / 2) - (screenWidth / 2),
                this._player.y + (this._player.height / 2) - (screenHeight / 2),
                screenWidth,
                screenHeight);

            // define the game NPCs
            this._characters = new List<Character>();
            for (int x = 0; x < 0; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    this._characters.Add(
                        new Character(
                            characterType: Character.CharacterType.Prisoner,
                            maxSpeed: 4,
                            x: x * WallWidth,
                            y: y * (WallHeight * 2),
                            width: 20,
                            height: 40,
                            depth: 12,
                            maxJumpHeight: WallHeight + (WallHeight / 2),
                            weapon: new Weapon(Weapon.WeaponType.SubmachineGun)));
                    this._characters[this._characters.Count - 1].changeWeapon(Weapon.WeaponType.SubmachineGun);
                }
            }
            this._lights = new List<Light>();
            this._walls = new List<Block>();
            Random rnd = new Random();
            for (int x = 2; x < 6; x++)
            {
                for (int y = 2; y < 6; y++)
                {
                    int xRnd = rnd.Next(-30, 10);
                    int yRnd = rnd.Next(-30, 10);
                    int heightRnd = rnd.Next(2, 4);
                    this._walls.AddRange(new Tree(x: x * 120 + xRnd, y: y * 120 + yRnd, width: WallWidth + xRnd, depth: WallDepth + xRnd, height: WallHeight * heightRnd, backgroundColor: this._mapColor).blocks);
                }
            }
            return;
        }

        public void update(
            long deltaTime,
            GameEngine.Framework.Interface.Game game)
        {
            this._player.maxSpeed = this._player.distancePerSecond / game.fps;
            this._delta += deltaTime;

            this._currentTime += deltaTime;
            {
                updatePlayer(
                    game: game,
                    deltaTime: deltaTime);
                checkCollision();
                this._screenPosition = this.screenPosition;
                this._player.setScreenPosition(
                    this._screenPosition,
                    this._screen);
                for (int i = 0; i < this._characters.Count; i++)
                {
                    this._characters[i].directionX = game.input.mouseX;
                    this._characters[i].directionY = game.input.mouseY;
                    this._characters[i].setScreenPosition(
                        this._screenPosition,
                        this._screen);
                }
                for (int i = 0; i < this._walls.Count; i++)
                {
                    this._walls[i].setScreenPosition(
                        this._screenPosition,
                        this._screen);
                }
                List<Round> roundsToRemove = new List<Round>();
                for (int i = 0; i < this._rounds.Count; i++)
                {
                    if (this._rounds[i].RangeExpired)
                    {
                        roundsToRemove.Add(this._rounds[i]);
                    }
                    this._rounds[i].setScreenPosition(
                        this._screenPosition,
                        this._screen);
                }
                foreach (var round in roundsToRemove)
                {
                    this._rounds.Remove(round);
                }
            }
            return;
        }

        private void updatePlayer(
            long deltaTime,
            GameEngine.Framework.Interface.Game game)
        {
            this._player.directionX = game.input.mouseX;
            this._player.directionY = game.input.mouseY;
            if (game.input.isKeyPressed(keyCode: (int)Keys.Up))
            {
                this._player.ySpeed = -this._player.maxSpeed;
            }
            else if (game.input.isKeyPressed(keyCode: (int)Keys.Down))
            {
                this._player.ySpeed = this._player.maxSpeed;
            }
            else
            {
                this._player.ySpeed = 0;
            }
            if (game.input.isKeyPressed(keyCode: (int)Keys.Left))
            {
                this._player.xSpeed = -this._player.maxSpeed;
            }
            else if (game.input.isKeyPressed(keyCode: (int)Keys.Right))
            {
                this._player.xSpeed = this._player.maxSpeed;
            }
            else
            {
                this._player.xSpeed = 0;
            }
            if (game.input.isKeyPressed(keyCode: (int)Keys.D0))
            {
                this._player.changeWeapon(weaponType: Weapon.WeaponType.None);
            }
            if (game.input.isKeyPressed(keyCode: (int)Keys.D1))
            {
                this._player.changeWeapon(weaponType: Weapon.WeaponType.SubmachineGun);
            }
            if (game.input.isKeyPressed(keyCode: (int)Keys.D2))
            {
                this._player.changeWeapon(weaponType: Weapon.WeaponType.ScopedSubmachineGun);
            }
            if (game.input.isKeyPressed(keyCode: (int)Keys.D3))
            {
                this._player.changeWeapon(weaponType: Weapon.WeaponType.ShotGun);
            }
            if (game.input.isKeyPressed(keyCode: (int)Keys.D4))
            {
                this._player.changeWeapon(weaponType: Weapon.WeaponType.MiniGun);
            }
            if (game.input.isKeyPressed(keyCode: (int)Keys.Space))
            {
                this._player.jump();
            }
            if (game.input.mouseMiddleButtonDelta < 0)
            {
                this._player.previousWeapon();
            }
            if (game.input.mouseMiddleButtonDelta > 0)
            {
                this._player.nextWeapon();
            }
            if (game.input.mouseLeftButtonDown)
            {
                this._player.currentWeapon.useWeapon(
                    deltaTime: deltaTime,
                    firing: true);
                if (this._player.currentWeapon.hasFired)
                {
                    this._player.xSpeedNoAnimateAdjustment -= this._player.XSpeedNoAnimateFigure;
                    this._player.ySpeedNoAnimateAdjustment -= this._player.YSpeedNoAnimateFigure;
                    this._rounds.Add(
                        new Round(
                            x: this._player.currentWeapon.RoundOrigin.X -
                                (this.screenPosition.X != 0 ? this.screenPosition.X : this._player.screenPosition.X - this._player.x),
                            y: this._player.currentWeapon.RoundOrigin.Y -
                                (this.screenPosition.Y != 0 ? this.screenPosition.Y : this._player.screenPosition.Y - this._player.y),
                            angleInDegrees: this._player.LastAngleInDegrees));
                }
            }
            else
            {
                this._player.currentWeapon.useWeapon(
                    deltaTime: deltaTime,
                    firing: false);
            }
            this._player.checkJump(game.fps);
            return;
        }

        private void checkCollision()
        {
            // check to see if the player has actually collided with anything
            bool collided = false;
            int maxGroundLevel = 0;

            // check map bounds
            if (this._player.x + this._player.xSpeed < 0 ||
                this._player.x + this._player.screenPosition.Width + this._player.xSpeed > this._map.Width)
            {
                this._player.xSpeed = 0;
            }
            if (this._player.y + this._player.ySpeed < 0 ||
                this._player.y + this._player.screenPosition.Height + this._player.ySpeed > this._map.Height)
            {
                this._player.ySpeed = 0;
            }

            // check player against walls and objects
            if (this._player.xSpeed != 0 || this._player.ySpeed != 0)
            {
                for (int i = 0; i < this._walls.Count; i++)
                {
                    // get the rectangle that represents the position of the wall on the map
                    Rectangle wallScreenPosition = this._walls[i].screenPosition;
                    Rectangle wallVerticalPosition = new Rectangle(
                        0,
                        this._walls[i].heightFromGround,
                        1,
                        this._walls[i].heightFromGround + this._walls[i].height);

                    // get the rectangle that represents the position of the character on the map after moving the character
                    Rectangle characterPosition = new Rectangle(
                        this._player.screenPosition.X + (int)this._player.xSpeed,
                        this._player.screenPosition.Y + (int)this._player.ySpeed,
                        this._player.width,
                        this._player.depth);
                    Rectangle characterVerticalPosition = new Rectangle(
                        0,
                        this._player.heightFromGround,
                        1,
                        this._player.heightFromGround + this._player.height);

                    // check for collision
                    if (GameEngine.Framework.Support.Shape.doesRectangleOverlaps(rect1: characterPosition, rect2: wallScreenPosition)
                        &&
                        (GameEngine.Framework.Support.Shape.doesRectangleOverlaps(rect1: characterVerticalPosition, rect2: wallVerticalPosition) || this._player.heightFromGround != 0))
                    {
                        collided = true;
                        if (this._player.heightFromGround < this._walls[i].height + this._walls[i].heightFromGround)
                        {
                            if (this._player.xSpeed != 0)
                            {
                                if (this._player.screenPosition.Y + this._player.screenPosition.Height > wallScreenPosition.Y
                                    && this._player.screenPosition.Y < wallScreenPosition.Y + wallScreenPosition.Height)
                                {
                                    this._player.xSpeed = 0;
                                }
                            }
                            if (this._player.ySpeed != 0)
                            {
                                if (this._player.screenPosition.X + this._player.screenPosition.Width > wallScreenPosition.X
                                    && this._player.screenPosition.X < wallScreenPosition.X + wallScreenPosition.Width)
                                {
                                    this._player.ySpeed = 0;
                                }
                            }
                        }
                        else
                        {
                            if(this._walls[i].height + this._walls[i].heightFromGround > maxGroundLevel)
                            {
                                maxGroundLevel = this._walls[i].height + this._walls[i].heightFromGround;
                            }
                        }
                    }
                }
                if (!collided)
                {
                    this._player.groundLevel = 0;
                }
                else
                {
                    this._player.groundLevel = maxGroundLevel;
                }
            }

            // set the speed for the player
            double x = this._player.x;
            double y = this._player.y;
            this._player.move();
            this._screen.X += (int)(this._player.x - x);
            this._screen.Y += (int)(this._player.y - y);

            for (int i = 0; i < this._characters.Count; i++ )
            {
                this._characters[i].move();
            }

            for (int i = 0; i < this._rounds.Count; i++)
            {
                this._rounds[i].move();
            }
            //this._rounds[i].move(
            //    this._rounds[i].xSpeed,
            //    this._rounds[i].ySpeed);
            return;
        }

        public Rectangle screenPosition
        {
            get
            {
                int x = this._screen.X;
                int y = this._screen.Y;
                int width = this._map.Width;
                int height = this._map.Height;

                if (this._screen.X < 0)
                {
                    x = Math.Abs(this._screen.X);
                }
                else if (this._screen.X > 0 && this._screen.X < this._map.Width)
                {
                    x = 0;
                    if (this._screen.X + this._screen.Width > this._map.Width)
                    {
                        width = width - this._screen.X;
                    }
                }

                if (this._screen.Y < 0)
                {
                    y = Math.Abs(this._screen.Y);
                }
                else if (this._screen.Y > 0 && this._screen.Y < this._map.Height)
                {
                    y = 0;
                    if (this._screen.Y + this._screen.Height > this._map.Height)
                    {
                        height = height - this._screen.Y;
                    }
                }

                return new Rectangle(
                    x,
                    y,
                    width,
                    height);
            }
        }

        public void draw(
            GameEngine.Framework.Interface.Game game)
        {
            // draw the main menu background
            //var x = this._screenPosition;

            int backgroundWidth = Ranger.Asset.List.tileDirt.width;
            int backgroundWidthOffset = -(this._player.x + this._screen.Width) % backgroundWidth;
            int backgroundHeight = Ranger.Asset.List.tileDirt.height;
            int backgroundHeightOffset = -(this._player.y + this._screen.Height) % backgroundHeight;

            for (int x = -1; x <= this._screen.Width / backgroundWidth; x++)
            {
                for (int y = -1; y <= this._screen.Height / backgroundHeight; y++)
                {
                    game.graphics.drawPixmap(
                        Ranger.Asset.List.tileDirt,
                        backgroundWidth * x + backgroundWidthOffset,
                        backgroundHeight * y + backgroundHeightOffset);
                }
            }

            this._gameObjects = new GameObject[this._walls.Count + this._characters.Count + this._rounds.Count + 1];
            this._objectSorted = true;
            this._gameObjects[0] = this._player;
            for (int i = 0; i < this._walls.Count; i++)
            {
                this._gameObjects[i + 1] = this._walls[i];
            }
            for (int i = 0; i < this._characters.Count; i++)
            {
                this._gameObjects[i + this._walls.Count + 1] = this._characters[i];
            }
            for (int i = 0; i < this._rounds.Count; i++)
            {
                this._gameObjects[i + this._walls.Count + this._characters.Count + 1] = this._rounds[i];
            }

            for (int round1 = 0; round1 < this._gameObjects.Length; round1++)
            {
                this._gameObjects[round1].temp = 0;
            }

            // calculate weight
            for (int object1 = 0; object1 < this._gameObjects.Length; object1++)
            {
                for (int object2 = 0; object2 < this._gameObjects.Length; object2++)
                {
                    if (object1 
                        != 
                        object2
                        && 
                            (
                                (
                                    this._gameObjects[object1].heightFromGround == this._gameObjects[object2].heightFromGround
                                    &&
                                    this._gameObjects[object1].y + this._gameObjects[object1].depth 
                                    >
                                    this._gameObjects[object2].y + this._gameObjects[object2].depth
                                )
                                ||
                                (
                                    this._gameObjects[object1].heightFromGround != 0
                                    &&
                                    this._gameObjects[object1].height + this._gameObjects[object1].heightFromGround
                                    >
                                    this._gameObjects[object2].height + this._gameObjects[object2].heightFromGround
                                )
                            )
                        )
                    {
                        this._gameObjects[object1].temp++;
                    }
                }
            }

            Array.Sort(this._gameObjects, delegate(GameObject g1, GameObject g2)
            {
                if (g1.temp == g2.temp)
                {
                    if (g1.y == g2.y)
                    {
                        return (g1.x).CompareTo(g2.x);
                    }
                    else
                    {
                        return (g1.y).CompareTo(g2.y);
                    }
                }
                else
                {
                    return (g1.temp).CompareTo(g2.temp);
                }
            });

            // draw the game shadows
            for (int i = 0; i < this._gameObjects.Length; i++)
            {
                this._gameObjects[i].drawShadow(game);
            }

            // draw the game objects
            for (int i = 0; i < this._gameObjects.Length; i++)
            {
                this._gameObjects[i].draw(game);
            }

            for (int i = 0; i < this._gameObjects.Length; i++)
            {
                //game.graphics.drawText(i.ToString(), this._gameObjects[i].screenPosition.X, this._gameObjects[i].screenPosition.Y - this._gameObjects[i].heightFromGround);
            }

            const double half = 30000;
            const double whole = 60000;
            int milliSecond = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            double portionThroughSecond = 0;

            if (milliSecond <= half)
            {
                portionThroughSecond = milliSecond / half;
            }
            else
            {
                portionThroughSecond = ((whole - milliSecond) / half);
            }

            //game.graphics.drawRect(
            //    new Rectangle(0, 0, this._screen.Width, this._screen.Height),
            //    new SolidBrush(Color.FromArgb((int)(48 * portionThroughSecond), Color.Orange)),
            //    null);
            //game.graphics.drawRect(
            //    new Rectangle(0, 0, this._screen.Width, this._screen.Height),
            //    new SolidBrush(Color.FromArgb(128 - (int)(128 * portionThroughSecond), Color.Navy)),
            //    null);

            // draw the players weapon list
            for (int i = 0; i < this._player.weapons.Count; i++)
            {
                //game.graphics.drawText(
                //    ((int)(this._player.weapons[i].weaponType)).ToString() + " - " + this._delta.ToString() + " - " + this._player.weapons[i].weaponName + (this._player.weapons[i].capableOfFiring ? " : " + this._player.weapons[i].rounds.ToString() : "") + (this._player.weapons[i].weaponName == this._player.currentWeapon.weaponName ? "*" : ""),
                //    5,
                //    this._screen.Height - 60 - (i * 20));
            }
            //game.graphics.drawText(
            //    "this.screenPosition = " + this.screenPosition.X.ToString() + ", " + this.screenPosition.Y.ToString(), 0, 0);
            //game.graphics.drawText(
            //    "this._player.screenPosition = " + this._player.screenPosition.X.ToString() + ", " + this._player.screenPosition.Y.ToString(), 0, 20);
            //game.graphics.drawText(
            //    "this._player = " + this._player.x.ToString() + ", " + this._player.y.ToString(), 0, 40);
            return;
        }
    }
}
