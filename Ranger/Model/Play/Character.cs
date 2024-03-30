namespace Ranger.Model.Play
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public class Character : Model.Play.GameObject
    {
        private CharacterType _characterType;
        private GameEngine.Framework.Interface.Pixmap _head;
        private GameEngine.Framework.Interface.Pixmap _body;
        private GameEngine.Framework.Interface.Pixmap _foot;
        private GameEngine.Framework.Interface.Pixmap _hand;
        private SolidBrush _shadowBrush = new SolidBrush(Color.FromArgb(32, Color.Black));
        private SolidBrush _coneOfVisionBrush = new SolidBrush(Color.FromArgb(5, Color.White));
        private int _legFrameMax = 8;
        private int _legFrameCurrent = 0;
        private int _legFrameIncrement = 1;
        private int _lastAngleInDegrees = 5;
        private List<Weapon> _weapons = new List<Weapon>();
        private bool _jumped = false;
        private bool _maxJumpHeightAchieved = false;
        private int _maxJumpHeight = 0;
        private int _maxJumpHeightOriginal = 0;
        private Weapon.WeaponType _currentWeaponType = Weapon.WeaponType.None;
        private int _fireAdjustment = 0;

        public enum CharacterType
        {
            Player = 0,
            Fighter = 1,
            Sprinter = 2,
            Ninja = 3,
            Giant = 4,
            Prisoner = 5,
            PowerArmour = 6,
            Zombie = 7
        }

        private enum BodyPart
        {
            Head = 0,
            Body = 1,
            LeftArm = 2,
            RightArm = 3,
            Legs = 4,
            Shadow = 5,
            Weapon = 6
        }

        public Character(
            CharacterType characterType,
            int maxSpeed,
            int x,
            int y,
            int width,
            int height,
            int depth,
            int maxJumpHeight,
            Weapon weapon) : base(maxSpeed:maxSpeed, distancePerSecond:maxSpeed * 25, x:x, y:y, width:width, height:height, depth:depth, heightFromGround:0)
        {
            this._characterType = characterType;
            this.addWeapon(weapon: new Weapon(Weapon.WeaponType.None));
            this.addWeapon(weapon: weapon);
            this._maxJumpHeight = maxJumpHeight;
            this._maxJumpHeightOriginal = maxJumpHeight;
            if (this._characterType == CharacterType.Ninja)
            {
                this._head = Asset.List.ninjaPlayerHead;
                this._body = Asset.List.ninjaPlayerBody;
                this._foot = Asset.List.ninjaPlayerFoot;
                this._hand = Asset.List.ninjaPlayerHand;
                this._legFrameMax = 20;
            }
            else if (this._characterType == CharacterType.Giant)
            {
                this._head = Asset.List.giantPlayerHead;
                this._body = Asset.List.giantPlayerBody;
                this._foot = Asset.List.giantPlayerFoot;
                this._hand = Asset.List.giantPlayerHand;
                this._legFrameMax = 60;
            }
            else if (this._characterType == CharacterType.Fighter)
            {
                this._head = Asset.List.fighterPlayerHead;
                this._body = Asset.List.fighterPlayerBody;
                this._foot = Asset.List.fighterPlayerFoot;
                this._hand = Asset.List.fighterPlayerHand;
                this._legFrameMax = 30;
            }
            else if (this._characterType == CharacterType.Prisoner)
            {
                this._head = Asset.List.humanPlayerHead;
                this._body = Asset.List.humanPlayerBody;
                this._foot = Asset.List.humanPlayerFoot;
                this._hand = Asset.List.humanPlayerHand;
            }
            else if (this._characterType == CharacterType.Zombie)
            {
                this._head = Asset.List.zombiePlayerHead;
                this._body = Asset.List.zombiePlayerBody;
                this._foot = Asset.List.zombiePlayerFoot;
                this._hand = Asset.List.zombiePlayerHand;
                this._legFrameMax = 60;
            }
            else if (this._characterType == CharacterType.PowerArmour)
            {
                this._head = Asset.List.powerArmourPlayerHead;
                this._body = Asset.List.powerArmourPlayerBody;
                this._foot = Asset.List.powerArmourPlayerFoot;
                this._hand = Asset.List.powerArmourPlayerHand;
                this._legFrameMax = 60;
            }
            else
            {
                this._head = Asset.List.characterPlayerHead;
                this._body = Asset.List.characterPlayerBody;
                this._foot = Asset.List.characterPlayerFoot;
                this._hand = Asset.List.characterPlayerHand;
            }
            return;
        }

        public override void drawShadow(GameEngine.Framework.Interface.Game game)
        {
            return;
        }

        public override void draw(
            GameEngine.Framework.Interface.Game game)
        {
            BodyPart[] renderOrder = new BodyPart[7];
            int rightArmHeightAdjustment = 0;
            int leftArmHeightAdjustment = 0;

            // find out what the angle between the central body point and the mouse location is
            int angleInDegrees = GameEngine.Framework.Support.Shape.findAngleBetweenPoints(
                directionX - (this.screenPosition.X + (this.screenPosition.Width / 2)),
                directionY - (this.screenPosition.Y - this.heightFromGround - this._body.height / 2));

            int speedOfRotation = 60 / this._legFrameMax;
            int angleDiffInDegrees = angleInDegrees - this._lastAngleInDegrees;
            if (angleDiffInDegrees < speedOfRotation && angleDiffInDegrees > -speedOfRotation)
            {
                angleDiffInDegrees = 0;
            }
            if (angleDiffInDegrees < -180)
            {
                angleDiffInDegrees = 360 + angleDiffInDegrees;
            }
            else if (angleDiffInDegrees > 180)
            {
                angleDiffInDegrees = angleDiffInDegrees - 360;
            }
            if (angleDiffInDegrees < 0)
            {
                this._lastAngleInDegrees -= speedOfRotation;
            }
            else if (angleDiffInDegrees > 0)
            {
                this._lastAngleInDegrees += speedOfRotation;
            }
            if (this._lastAngleInDegrees < 0)
            {
                this._lastAngleInDegrees = 360 - this._lastAngleInDegrees;
            }
            else if (this._lastAngleInDegrees > 360)
            {
                this._lastAngleInDegrees = this._lastAngleInDegrees - 360;
            }
         
            // calc an temp ellipse for the character hand ellipse
            Rectangle handMovementEllipse = new Rectangle(
                this.screenPosition.X - 2 + this.screenPosition.Width / 2 - this._body.width / 8 / 2,
                this.screenPosition.Y - this._body.height + (this._body.height / 4) - this.heightFromGround,
                this._body.width / 8 + 4,
                6);

            // calculate which leg frame we need
            if ((this._legFrameCurrent > 0 && this._legFrameCurrent < this._legFrameMax || base.ySpeed != 0 || base.xSpeed != 0))
            {
                this._legFrameCurrent += this._legFrameIncrement;
                if (this._legFrameCurrent == 0)
                {
                    this._legFrameIncrement = Math.Abs(this._legFrameIncrement);
                }
                else if (this._legFrameCurrent == this._legFrameMax)
                {
                    this._legFrameIncrement = -this._legFrameIncrement;
                }
            }

            // need to work out which order the character elements get drawn
            // 1) character shadow always first
            // 2) feet always second
            // 3) if angle is > 270 and angle is < 90 then head is third
            renderOrder[0] = BodyPart.Shadow;
            renderOrder[1] = BodyPart.Legs;
            if (this._lastAngleInDegrees > 180)
            {
                renderOrder[2] = BodyPart.LeftArm;
                if (this._lastAngleInDegrees > 315)
                {
                    renderOrder[3] = BodyPart.Weapon;
                    renderOrder[4] = BodyPart.Body;
                    renderOrder[5] = BodyPart.Head;
                    renderOrder[6] = BodyPart.RightArm;
                }
                else
                {
                    renderOrder[3] = BodyPart.Weapon;
                    renderOrder[4] = BodyPart.Body;
                    renderOrder[5] = BodyPart.RightArm;
                    renderOrder[6] = BodyPart.Head;
                }
            }
            else
            {
                renderOrder[2] = BodyPart.RightArm;
                if (this._lastAngleInDegrees < 45)
                {
                    renderOrder[3] = BodyPart.Weapon;
                    renderOrder[4] = BodyPart.Body;
                    renderOrder[5] = BodyPart.Head;
                    renderOrder[6] = BodyPart.LeftArm;
                }
                else
                {
                    renderOrder[3] = BodyPart.Body;
                    renderOrder[4] = BodyPart.Head;
                    renderOrder[5] = BodyPart.Weapon;
                    renderOrder[6] = BodyPart.LeftArm;
                }
            }

            // movement for weapon firing
            if (this.currentWeapon.hasFired)
            {
                this._fireAdjustment = 4;
            }
            if (!this.currentWeapon.hasFired && this._fireAdjustment > 0)
            {
                this._fireAdjustment--;
            }

            Point firingOffsetPoint = GameEngine.Framework.Support.Shape.findTriangleCorner(
                hypotenuse: this._fireAdjustment,
                angleInDegrees: angleInDegrees);

            foreach (BodyPart bodyPart in renderOrder)
            {
                switch (bodyPart)
                {
                    case BodyPart.Head:
                        // calc an temp ellipse for the character head ellipse
                        Rectangle headMovementEllipse = new Rectangle(
                            this.screenPosition.X + this.screenPosition.Width / 2 - 3,
                            this.screenPosition.Y - this._body.height + (this._head.height / 3) - this.heightFromGround,
                            6,
                            4);

                        Point headAnglePoint = GameEngine.Framework.Support.Shape.findPointOnEllipse(
                            headMovementEllipse.X + (headMovementEllipse.Width / 2),
                            headMovementEllipse.Y + (headMovementEllipse.Height / 2),
                            headMovementEllipse.Width,
                            headMovementEllipse.Height,
                            this._lastAngleInDegrees);

                        game.graphics.drawPixmap(
                            this._head,
                            headAnglePoint.X - this._head.width / 8 / 2,
                            (int)(headAnglePoint.Y - this._head.height * 1.2),
                            this._head.width / 8 * getEightCellPositionBasedOnAngle(this._lastAngleInDegrees),
                            0,
                            this._head.width / 8,
                            this._head.height);
                        break;
                    case BodyPart.Body:
                        // draw the body above the feet
                        game.graphics.drawPixmap(
                            this._body,
                            this.screenPosition.X + this.screenPosition.Width / 2 - this._body.width / 8 / 2,
                            this.screenPosition.Y - (int)(this._body.height) - this.heightFromGround,
                            this._body.width / 8 * getEightCellPositionBasedOnAngle(this._lastAngleInDegrees),
                            0,
                            this._body.width / 8,
                            this._body.height);
                        break;
                    case BodyPart.LeftArm:
                        Point leftHandAnglePoint = GameEngine.Framework.Support.Shape.findPointOnEllipse(
                            handMovementEllipse.X + (handMovementEllipse.Width / 2) - firingOffsetPoint.X,
                            handMovementEllipse.Y + (handMovementEllipse.Height / 8) - firingOffsetPoint.Y,
                            handMovementEllipse.Width,
                            handMovementEllipse.Height,
                            this._lastAngleInDegrees + 90);

                        if (this._legFrameIncrement < 0)
                        {
                            if (this._legFrameCurrent == 0 || this._legFrameCurrent == this._legFrameMax)
                            {
                                leftArmHeightAdjustment = 0;
                            }
                            else if (this._legFrameCurrent <= 3 || this._legFrameCurrent >= this._legFrameMax - 3)
                            {
                                leftArmHeightAdjustment = -1;
                            }
                            else if (this._legFrameCurrent <= 6 || this._legFrameCurrent >= this._legFrameMax - 6)
                            {
                                leftArmHeightAdjustment = -2;
                            }
                            else
                            {
                                leftArmHeightAdjustment = -3;
                            }
                        }
                        leftHandAnglePoint.Y += leftArmHeightAdjustment;

                        game.graphics.drawPixmap(
                            this._hand,
                            leftHandAnglePoint.X - this._hand.width / 8 / 2 + (this._lastAngleInDegrees + 90 > 270 ? 0 : 1),
                            leftHandAnglePoint.Y,
                            this._hand.width / 8 * getEightCellPositionBasedOnAngle(this._lastAngleInDegrees),
                            0,
                            this._hand.width / 8,
                            this._hand.height);
                        break;
                    case BodyPart.RightArm:
                        Point rightHandAnglePoint = GameEngine.Framework.Support.Shape.findPointOnEllipse(
                            handMovementEllipse.X + (handMovementEllipse.Width / 2),
                            handMovementEllipse.Y + (handMovementEllipse.Height / 8),
                            handMovementEllipse.Width,
                            handMovementEllipse.Height,
                            this._lastAngleInDegrees + 90 + 180);

                        if (this._legFrameIncrement > 0)
                        {
                            if (this._legFrameCurrent == 0 || this._legFrameCurrent == this._legFrameMax)
                            {
                                rightArmHeightAdjustment = 0;
                            }
                            else if (this._legFrameCurrent <= 3 || this._legFrameCurrent >= this._legFrameMax - 3)
                            {
                                rightArmHeightAdjustment = -1;
                            }
                            else if (this._legFrameCurrent <= 6 || this._legFrameCurrent >= this._legFrameMax - 6)
                            {
                                rightArmHeightAdjustment = -2;
                            }
                            else
                            {
                                rightArmHeightAdjustment = -3;
                            }
                        }
                        rightHandAnglePoint.Y += rightArmHeightAdjustment;

                        game.graphics.drawPixmap(
                            this._hand,
                            rightHandAnglePoint.X - this._hand.width / 8 / 2 + (this._lastAngleInDegrees + 90 > 270 ? 0 : 1),
                            rightHandAnglePoint.Y,
                            this._hand.width / 8 * getEightCellPositionBasedOnAngle(this._lastAngleInDegrees),
                            0,
                            this._hand.width / 8,
                            this._hand.height);
                        break;
                    case BodyPart.Legs:
                        // render the feet based on the angle above
                        Rectangle footPosition = new Rectangle(
                            this.screenPosition.X + (this._foot.width / 8) / 4,
                            this.screenPosition.Y + this._foot.height / 4 - this.heightFromGround,
                            this.screenPosition.Width - (this._foot.width / 8) / 2,
                            this.screenPosition.Height - this._foot.height / 2);

                        for (int i = 0; i < 2; i++)
                        {
                            int footAngle = 0;
                            if (this.xSpeed == 0 && this.ySpeed == 0)
                            {
                                footAngle = this._lastAngleInDegrees;
                            }
                            else if (this.xSpeed > 0 && this.ySpeed < 0)
                            {
                                footAngle = 45;
                            }
                            else if (this.xSpeed > 0 && this.ySpeed == 0)
                            {
                                footAngle = 90;
                            }
                            else if (this.xSpeed > 0 && this.ySpeed > 0)
                            {
                                footAngle = 135;
                            }
                            else if (this.xSpeed == 0 && this.ySpeed > 0)
                            {
                                footAngle = 180;
                            }
                            else if (this.xSpeed < 0 && this.ySpeed > 0)
                            {
                                footAngle = 225;
                            }
                            else if (this.xSpeed < 0 && this.ySpeed == 0)
                            {
                                footAngle = 270;
                            }
                            else if (this.xSpeed < 0 && this.ySpeed < 0)
                            {
                                footAngle = 315;
                            }

                            Point footAnglePoint = GameEngine.Framework.Support.Shape.findPointOnEllipse(
                                footPosition.X + (footPosition.Width / 2),
                                footPosition.Y + (footPosition.Height / 2),
                                footPosition.Width,
                                footPosition.Height,
                                footAngle + 60 + (i * 180));
                            Point footAnglePointReverse = GameEngine.Framework.Support.Shape.findPointOnEllipse(
                                footPosition.X + (footPosition.Width / 2),
                                footPosition.Y + (footPosition.Height / 2),
                                footPosition.Width,
                                footPosition.Height,
                                footAngle + 120 + (i * 180));

                            double x = footAnglePoint.X - (((footAnglePoint.X - footAnglePointReverse.X) / (double)this._legFrameMax) * this._legFrameCurrent);
                            double y = footAnglePoint.Y - (((footAnglePoint.Y - footAnglePointReverse.Y) / (double)this._legFrameMax) * this._legFrameCurrent);

                            if ((this._legFrameIncrement > 0 && i == 1) || (this._legFrameIncrement < 0 && i == 0))
                            {
                                if (this._legFrameCurrent == 0 || this._legFrameCurrent == this._legFrameMax)
                                {
                                    y -= 0;
                                }
                                else if (this._legFrameCurrent <= 3 || this._legFrameCurrent >= this._legFrameMax - 3)
                                {
                                    y -= 3;
                                }
                                else if (this._legFrameCurrent <= 6 || this._legFrameCurrent >= this._legFrameMax - 6)
                                {
                                    y -= 6;
                                }
                                else
                                {
                                    y -= 9;
                                }
                            }

                            game.graphics.drawPixmap(
                                this._foot,
                                (int)x - (this._foot.width / 8) / 2,
                                (int)y - (this._foot.width / 8) / 2,
                                (this._foot.width / 8) * getEightCellPositionBasedOnAngle(footAngle),
                                0,
                                (this._foot.width / 8),
                                this._foot.height);
                        }
                        break;
                    case BodyPart.Shadow:
                        // draw an ellipse for the character shadow
                        game.graphics.drawEllipse(
                            this.screenPosition.X - 4 + (this._jumped ? this.heightFromGround - this.groundLevel : 0),
                            this.screenPosition.Y - 4 - this.groundLevel + (this._jumped ? this.heightFromGround - this.groundLevel : 0),
                            this.screenPosition.Width + 6,
                            this.screenPosition.Height + 3,
                            this._shadowBrush,
                            null);

                        //// draw the cone of vision
                        //Rectangle coneOfVision = new Rectangle(
                        //    this.screenPosition.X + this.screenPosition.Width / 2 - 200,
                        //    this.screenPosition.Y - this.groundLevel + (this._jumped ? this.heightFromGround - this.groundLevel : 0) + this.screenPosition.Height / 2 - 200,
                        //    400,
                        //    400);

                        //game.graphics.drawArc(
                        //    coneOfVision,
                        //    this._lastAngleInDegrees - 45,
                        //    90,
                        //    this._coneOfVisionBrush,
                        //    null);
                        break;
                    case BodyPart.Weapon:
                        // call the draw method on the current weapon
                        Rectangle handMovementEllipseForWeapon = new Rectangle(
                            handMovementEllipse.X + 2,
                            handMovementEllipse.Y - rightArmHeightAdjustment - leftArmHeightAdjustment - 2,
                            handMovementEllipse.Width - 4,
                            handMovementEllipse.Height);
                            
                        this.currentWeapon.draw(
                            game: game,
                            angleInDegrees: this._lastAngleInDegrees,
                            characterOffset: this.screenPosition,
                            handMovementEllipse: handMovementEllipseForWeapon,
                            firingOffsetPoint: firingOffsetPoint);
                        break;
                    default:
                        break;
                }
            }
            return;
        }
        public Weapon currentWeapon
        {
            get
            {
                for (int i = 0; i < this._weapons.Count; i++)
                {
                    if (this._weapons[i].weaponType == this._currentWeaponType)
                    {
                        return this._weapons[i];
                    }
                }
                throw new Exception();
            }
        }
        public void addWeapon(
            Weapon weapon)
        {
            this._weapons.Add(weapon);
            return;
        }
        public void changeWeapon(
            Weapon.WeaponType weaponType)
        {
            for (int i = 0; i < this._weapons.Count; i++)
            {
                if(this._weapons[i].weaponType == weaponType){
                    this._currentWeaponType = weaponType;
                    break;
                }
            }
            return;
        }
        public void nextWeapon()
        {
            int currentWeaponIndex = 0;
            for (int i = 0; i < this._weapons.Count; i++)
            {
                if (this._weapons[i].weaponType == this.currentWeapon.weaponType)
                {
                    currentWeaponIndex = i;
                    break;
                }
            }
            return;
        }
        public void previousWeapon()
        {
            int currentWeaponIndex = 0;
            for (int i = 0; i < this._weapons.Count; i++)
            {
                if (this._weapons[i].weaponType == this.currentWeapon.weaponType)
                {
                    currentWeaponIndex = i;
                    break;
                }
            }
            if (currentWeaponIndex - 1 < 0)
            {
                this._currentWeaponType = this._weapons[this._weapons.Count - 1].weaponType;
            }
            else
            {
                this._currentWeaponType = this._weapons[currentWeaponIndex - 1].weaponType;
            }
            return;
        }
        public List<Weapon> weapons
        {
            get
            {
                this._weapons.Sort();
                return this._weapons;
            }
        }
        public void jump()
        {
            if (!this._jumped)
            {
                this._maxJumpHeight = _maxJumpHeightOriginal + this.groundLevel;
                this._jumped = true;
            }
            return;
        }
        public void checkJump(
            double fps)
        {
            // determine the speed of the height increase/decrease
            int heightIncrement = 0;
            
            // are we jumping yet?
            if (this._jumped)
            {
                // has the jump height been achieved yet?
                if (!this._maxJumpHeightAchieved)
                {
                    if (this.heightFromGround < this._maxJumpHeight - 6)
                    {
                        heightIncrement = 4;
                    }
                    else if (this.heightFromGround >= this._maxJumpHeight - 6 && this.heightFromGround <= this._maxJumpHeight - 2)
                    {
                        heightIncrement = 2;
                    }
                    else
                    {
                        heightIncrement = 1;
                    }

                    // if the maximum jump height has not yet been achieved then keep increasing the vertical height
                    this.detailHeightFromGround += heightIncrement / (distancePerSecond / fps);
                    if (this.heightFromGround >= this._maxJumpHeight)
                    {
                        this._maxJumpHeightAchieved = true;
                    }
                }
                else
                {
                    if (this.heightFromGround < this._maxJumpHeight - 10)
                    {
                        heightIncrement = 8;
                    }
                    else if (this.heightFromGround < this._maxJumpHeight - 6)
                    {
                        heightIncrement = 4;
                    }
                    else if (this.heightFromGround >= this._maxJumpHeight - 6 && this.heightFromGround <= this._maxJumpHeight - 2)
                    {
                        heightIncrement = 2;
                    }
                    else
                    {
                        heightIncrement = 1;
                    }

                    if (this.heightFromGround > this.groundLevel)
                    {
                        this.detailHeightFromGround -= heightIncrement / (distancePerSecond / fps);
                        if (this.heightFromGround <= this.groundLevel)
                        {
                            this.detailHeightFromGround = this.groundLevel;
                            this._maxJumpHeightAchieved = false;
                            this._jumped = false;
                        }
                    }
                    else
                    {
                        this.detailHeightFromGround = this.groundLevel;
                        this._maxJumpHeightAchieved = false;
                        this._jumped = false;
                    }
                }
            }
            else if (this.heightFromGround > this.groundLevel)
            {
                if (this.heightFromGround < this._maxJumpHeight - 10)
                {
                    heightIncrement = 8;
                }
                else if (this.heightFromGround < this._maxJumpHeight - 6)
                {
                    heightIncrement = 4;
                }
                else if (this.heightFromGround >= this._maxJumpHeight - 6 && this.heightFromGround <= this._maxJumpHeight - 2)
                {
                    heightIncrement = 2;
                }
                else
                {
                    heightIncrement = 1;
                }

                this.detailHeightFromGround -= heightIncrement / (distancePerSecond / fps);
                if (this.heightFromGround <= this.groundLevel)
                {
                    this.detailHeightFromGround = this.groundLevel;
                    this._maxJumpHeightAchieved = false;
                    this._jumped = false;
                }
            }
            return;
        }
        public int LastAngleInDegrees
        {
            get
            {
                return this._lastAngleInDegrees;
            }
        }
        public int XSpeedNoAnimateFigure
        {
            get
            {
                int max = 4;
                if (this.LastAngleInDegrees >= 338 || this.LastAngleInDegrees < 22)
                {
                    return 0;
                }
                else if (this.LastAngleInDegrees >= 22 && this.LastAngleInDegrees < 68)
                {
                    return max / 2;
                }
                else if (this.LastAngleInDegrees >= 68 && this.LastAngleInDegrees < 112)
                {
                    return max;
                }
                else if (this.LastAngleInDegrees >= 112 && this.LastAngleInDegrees < 158)
                {
                    return max / 2;
                }
                else if (this.LastAngleInDegrees >= 158 && this.LastAngleInDegrees < 202)
                {
                    return 0;
                }
                else if (this.LastAngleInDegrees >= 202 && this.LastAngleInDegrees < 258)
                {
                    return -max / 2;
                }
                else if (this.LastAngleInDegrees >= 258 && this.LastAngleInDegrees < 292)
                {
                    return -max;
                }
                else
                {
                    return -max / 2;
                }
            }
        }
        public int YSpeedNoAnimateFigure
        {
            get
            {
                int max = 4;
                if (this.LastAngleInDegrees >= 338 || this.LastAngleInDegrees < 22)
                {
                    return -max;
                }
                else if (this.LastAngleInDegrees >= 22 && this.LastAngleInDegrees < 68)
                {
                    return -max / 2;
                }
                else if (this.LastAngleInDegrees >= 68 && this.LastAngleInDegrees < 112)
                {
                    return 0;
                }
                else if (this.LastAngleInDegrees >= 112 && this.LastAngleInDegrees < 158)
                {
                    return max / 2;
                }
                else if (this.LastAngleInDegrees >= 158 && this.LastAngleInDegrees < 202)
                {
                    return max;
                }
                else if (this.LastAngleInDegrees >= 202 && this.LastAngleInDegrees < 258)
                {
                    return max / 2;
                }
                else if (this.LastAngleInDegrees >= 258 && this.LastAngleInDegrees < 292)
                {
                    return 0;
                }
                else
                {
                    return -max / 2;
                }
            }
        }
    }
}
