namespace Ranger.Model.Play
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public class Weapon : IComparable<Weapon>
    {
        public enum WeaponType
        {
            None = 0,
            SubmachineGun = 1,
            ScopedSubmachineGun = 2,
            ShotGun = 3,
            MiniGun = 4,
        }

        private struct WeaponPart
        {
            public SolidBrush weaponBrush;
            public Pen weaponPen;
            public List<Point> points;
            public List<Rectangle> rotationalEllipses;
            public bool roundOrigin;
        }

        private const int maxRoundsNone = 0;
        private const int maxRoundsSubmachineGun = 200;
        private const int maxRoundsMiniGun = 400;
        private const int maxRoundsShotGun = 60;
        private SolidBrush _weaponMetalBrush = new SolidBrush(Color.FromArgb(50, 50, 50));
        private Pen _weaponMetalPen = new Pen(Color.FromArgb(50, 50, 50));
        private SolidBrush _weaponAluminiumBrush = new SolidBrush(Color.FromArgb(20, 20, 20));
        private Pen _weaponAluminiumPen = new Pen(Color.FromArgb(20, 20, 20));
        private bool _capableOfFiring = false;
        private int _milliSecondsBetweenRounds = 0;
        public long _deltaTotal = 0;
        private long _lastMilliseconds = 0;
        private bool _hasFired = false;

        private WeaponType _weaponType;
        private int _rounds;
        private List<WeaponPart> _weaponParts = new List<WeaponPart>();

        public Weapon(
            WeaponType weaponType)
        {
            this._weaponType = weaponType;
            int rounds = 0;
            switch (this._weaponType)
            {
                case WeaponType.None:
                    rounds = maxRoundsNone;
                    break;
                case WeaponType.SubmachineGun:
                    rounds = maxRoundsSubmachineGun;
                    break;
                case WeaponType.MiniGun:
                    rounds = maxRoundsMiniGun;
                    break;
                case WeaponType.ShotGun:
                    rounds = maxRoundsShotGun;
                    break;
                case WeaponType.ScopedSubmachineGun:
                    rounds = maxRoundsSubmachineGun;
                    break;
                default:
                    break;
            }
            NewWeapon(
                weaponType: weaponType,
                rounds: rounds);
            return;
        }

        public Weapon(
            WeaponType weaponType, 
            int rounds)
        {
            NewWeapon(
                weaponType: weaponType,
                rounds: rounds);
            return;
        }

        private void NewWeapon(
            WeaponType weaponType,
            int rounds)
        {
            this._weaponType = weaponType;
            this._rounds = rounds;
            switch (this._weaponType)
            {
                case WeaponType.None:
                    this._capableOfFiring = false;
                    break;
                case WeaponType.SubmachineGun:
                    this._capableOfFiring = true;
                    this._milliSecondsBetweenRounds = 22;
                    break;
                case WeaponType.MiniGun:
                    this._capableOfFiring = true;
                    this._milliSecondsBetweenRounds = 100;
                    break;
                case WeaponType.ShotGun:
                    this._capableOfFiring = true;
                    this._milliSecondsBetweenRounds = 1000;
                    break;
                case WeaponType.ScopedSubmachineGun:
                    this._capableOfFiring = true;
                    this._milliSecondsBetweenRounds = 200;
                    break;
                default:
                    this._capableOfFiring = false;
                    break;
            }
            setWeaponPolygon();
            return;
        }

        public bool capableOfFiring
        {
            get
            {
                return this._capableOfFiring;
            }
        }

        public string weaponName
        {
            get
            {
                switch (this._weaponType)
                {
                    case WeaponType.None:
                        return "None";
                    case WeaponType.SubmachineGun:
                        return "Submachine Gun";
                    case WeaponType.MiniGun:
                        return "Mini Gun";
                    case WeaponType.ShotGun:
                        return "Shotgun";
                    case WeaponType.ScopedSubmachineGun:
                        return "Scoped Submachine Gun";
                    default:
                        return string.Empty;
                }
            }
        }

        public WeaponType weaponType
        {
            get
            {
                return this._weaponType;
            }
        }

        private void setWeaponPolygon()
        {
            WeaponPart weaponPart;
            
            switch (this._weaponType)
            {
                case WeaponType.None:
                    break;
                case WeaponType.SubmachineGun:
                    weaponPart = new WeaponPart();
                    weaponPart.weaponBrush = this._weaponMetalBrush;
                    weaponPart.weaponPen = this._weaponMetalPen;
                    weaponPart.points = new List<Point>();
                    weaponPart.rotationalEllipses = new List<Rectangle>();
                    weaponPart.points.Add(new Point(0, -5));
                    weaponPart.points.Add(new Point(14, -5));
                    weaponPart.points.Add(new Point(15, -4));
                    weaponPart.points.Add(new Point(0, -4));
                    this._weaponParts.Add(weaponPart);

                    weaponPart = new WeaponPart();
                    weaponPart.weaponBrush = this._weaponAluminiumBrush;
                    weaponPart.weaponPen = this._weaponAluminiumPen;
                    weaponPart.points = new List<Point>();
                    weaponPart.rotationalEllipses = new List<Rectangle>();
                    weaponPart.points.Add(new Point(0, -3));
                    weaponPart.points.Add(new Point(15, -3));
                    weaponPart.points.Add(new Point(15, -2));
                    weaponPart.points.Add(new Point(16, -2));
                    weaponPart.points.Add(new Point(16, 0));
                    weaponPart.points.Add(new Point(10, 0));
                    weaponPart.points.Add(new Point(10, 8));
                    weaponPart.points.Add(new Point(8, 8));
                    weaponPart.points.Add(new Point(8, 0));
                    weaponPart.points.Add(new Point(4, 0));
                    weaponPart.points.Add(new Point(4, 6));
                    weaponPart.points.Add(new Point(2, 6));
                    weaponPart.points.Add(new Point(2, 0));
                    weaponPart.points.Add(new Point(0, 0));
                    this._weaponParts.Add(weaponPart);

                    weaponPart = new WeaponPart();
                    weaponPart.roundOrigin = true;
                    weaponPart.weaponBrush = this._weaponAluminiumBrush;
                    weaponPart.weaponPen = this._weaponAluminiumPen;
                    weaponPart.points = new List<Point>();
                    weaponPart.rotationalEllipses = new List<Rectangle>();
                    weaponPart.points.Add(new Point(16, -3));
                    weaponPart.points.Add(new Point(16, -3));
                    this._weaponParts.Add(weaponPart);
                    break;
                case WeaponType.MiniGun:

                    weaponPart = new WeaponPart();
                    weaponPart.weaponBrush = this._weaponMetalBrush;
                    weaponPart.weaponPen = this._weaponMetalPen;
                    weaponPart.points = new List<Point>();
                    weaponPart.rotationalEllipses = new List<Rectangle>();
                    weaponPart.points.Add(new Point(3, -2));
                    weaponPart.points.Add(new Point(6, -5));
                    weaponPart.points.Add(new Point(24, -5));
                    weaponPart.points.Add(new Point(24, 3));
                    weaponPart.points.Add(new Point(21, 3));
                    weaponPart.points.Add(new Point(21, -1));
                    weaponPart.points.Add(new Point(15, -1));
                    weaponPart.points.Add(new Point(15, 3));
                    weaponPart.points.Add(new Point(6, 3));
                    weaponPart.points.Add(new Point(6, 8));
                    weaponPart.points.Add(new Point(3, 8));
                    this._weaponParts.Add(weaponPart);

                    weaponPart = new WeaponPart();
                    weaponPart.weaponBrush = this._weaponMetalBrush;
                    weaponPart.weaponPen = this._weaponMetalPen;
                    weaponPart.points = new List<Point>();
                    weaponPart.rotationalEllipses = new List<Rectangle>();
                    weaponPart.points.Add(new Point(24, -1));
                    weaponPart.points.Add(new Point(26, -1));
                    weaponPart.points.Add(new Point(26, 3));
                    weaponPart.points.Add(new Point(24, 3));
                    this._weaponParts.Add(weaponPart);

                    weaponPart = new WeaponPart();
                    weaponPart.weaponBrush = this._weaponMetalBrush;
                    weaponPart.weaponPen = this._weaponMetalPen;
                    weaponPart.points = new List<Point>();
                    weaponPart.rotationalEllipses = new List<Rectangle>();
                    weaponPart.points.Add(new Point(15, -1));
                    weaponPart.points.Add(new Point(21, -1));
                    weaponPart.points.Add(new Point(21, 4));
                    weaponPart.points.Add(new Point(15, 4));
                    this._weaponParts.Add(weaponPart);

                    weaponPart = new WeaponPart();
                    weaponPart.weaponBrush = this._weaponMetalBrush;
                    weaponPart.weaponPen = this._weaponMetalPen;
                    weaponPart.points = new List<Point>();
                    weaponPart.rotationalEllipses = new List<Rectangle>();
                    weaponPart.points.Add(new Point(9, 3));
                    weaponPart.points.Add(new Point(13, 3));
                    weaponPart.points.Add(new Point(13, 6));
                    weaponPart.points.Add(new Point(16, 9));
                    weaponPart.points.Add(new Point(13, 12));
                    weaponPart.points.Add(new Point(10, 9));
                    weaponPart.points.Add(new Point(9, 6));
                    this._weaponParts.Add(weaponPart);

                    break;
                case WeaponType.ShotGun:
                    weaponPart = new WeaponPart();
                    weaponPart.weaponBrush = this._weaponMetalBrush;
                    weaponPart.weaponPen = this._weaponMetalPen;
                    weaponPart.points = new List<Point>();
                    weaponPart.rotationalEllipses = new List<Rectangle>();
                    weaponPart.points.Add(new Point(0, 0));
                    weaponPart.points.Add(new Point(17, 0));
                    weaponPart.points.Add(new Point(17, -1));
                    weaponPart.points.Add(new Point(18, -1));
                    weaponPart.points.Add(new Point(18, 4));
                    weaponPart.points.Add(new Point(16, 4));
                    weaponPart.points.Add(new Point(16, 6));
                    weaponPart.points.Add(new Point(10, 6));
                    weaponPart.points.Add(new Point(10, 4));
                    weaponPart.points.Add(new Point(0, 4));
                    this._weaponParts.Add(weaponPart);
                    break;
                case WeaponType.ScopedSubmachineGun:
                    weaponPart = new WeaponPart();
                    weaponPart.weaponBrush = this._weaponMetalBrush;
                    weaponPart.weaponPen = this._weaponMetalPen;
                    weaponPart.points = new List<Point>();
                    weaponPart.rotationalEllipses = new List<Rectangle>();
                    weaponPart.points.Add(new Point(0, 0));
                    weaponPart.points.Add(new Point(6, 0));
                    weaponPart.points.Add(new Point(6, -2));
                    weaponPart.points.Add(new Point(4, -2));
                    weaponPart.points.Add(new Point(4, -4));
                    weaponPart.points.Add(new Point(14, -4));
                    weaponPart.points.Add(new Point(14, -2));
                    weaponPart.points.Add(new Point(12, -2));
                    weaponPart.points.Add(new Point(12, 0));
                    weaponPart.points.Add(new Point(25, 0));
                    weaponPart.points.Add(new Point(25, 5));
                    weaponPart.points.Add(new Point(11, 5));
                    weaponPart.points.Add(new Point(11, 7));
                    weaponPart.points.Add(new Point(12, 9));
                    weaponPart.points.Add(new Point(13, 11));
                    weaponPart.points.Add(new Point(10, 13));
                    weaponPart.points.Add(new Point(9, 11));
                    weaponPart.points.Add(new Point(8, 9));
                    weaponPart.points.Add(new Point(7, 5));
                    weaponPart.points.Add(new Point(5, 5));
                    weaponPart.points.Add(new Point(5, 6));
                    weaponPart.points.Add(new Point(3, 6));
                    weaponPart.points.Add(new Point(3, 5));
                    weaponPart.points.Add(new Point(0, 5));
                    this._weaponParts.Add(weaponPart);
                    break;
                default:
                    break;
            }
            
            foreach (WeaponPart part in this._weaponParts)
            {
                foreach (Point partPoint in part.points)
                {
                    part.rotationalEllipses.Add(
                        new Rectangle(
                            -partPoint.X,
                            -partPoint.X + partPoint.Y,
                            partPoint.X * 2,
                            partPoint.X * 2));
                }
            }
            return;
        }

        public int thickNess
        {
            get
            {
                switch (this._weaponType)
                {
                    case WeaponType.None:
                        return 0;
                    case WeaponType.SubmachineGun:
                        return 2;
                    case WeaponType.MiniGun:
                        return 4;
                    case WeaponType.ShotGun:
                        return 3;
                    case WeaponType.ScopedSubmachineGun:
                        return 2;
                    default:
                        return 0;
                }
            }
        }

        public void draw(
            GameEngine.Framework.Interface.Game game, 
            int angleInDegrees, 
            Rectangle characterOffset, 
            Rectangle handMovementEllipse,
            Point firingOffsetPoint)
        {
            Point handMovementEllipseOffset = GameEngine.Framework.Support.Shape.findPointOnEllipse(
                handMovementEllipse.X + (handMovementEllipse.Width / 2) - firingOffsetPoint.X,
                handMovementEllipse.Y + (handMovementEllipse.Height / 2) - firingOffsetPoint.Y,
                handMovementEllipse.Width,
                handMovementEllipse.Height,
                angleInDegrees + 90);

            int thickNessCount = 1;
            double angleDiff = 0;
            double fraction = 0;
            if (angleInDegrees >= 0 && angleInDegrees < 90)
            {
                angleDiff = 90.0 - angleInDegrees;
                fraction =  Math.Abs(angleDiff / 90.0);
                thickNessCount = (int)(thickNess * fraction) + 1;
            }
            else if (angleInDegrees >= 90 && angleInDegrees < 180)
            {
                angleDiff = angleInDegrees - 90;
                fraction = Math.Abs(angleDiff / 90.0);
                thickNessCount = (int)(thickNess * fraction) + 1;
            }
            else if (angleInDegrees >= 180 && angleInDegrees < 270)
            {
                angleDiff = angleInDegrees - 180.0;
                fraction = 1 - Math.Abs(angleDiff / 90.0);
                thickNessCount = (int)(thickNess * fraction) + 1;
            }
            else
            {
                angleDiff = angleInDegrees - 270;
                fraction = Math.Abs(angleDiff / 90.0);
                thickNessCount = (int)(thickNess * fraction) + 1;
            }
            //thickNessCount = 1;

            for (int thicknessIndex = 0; thicknessIndex < thickNessCount; thicknessIndex++)
            {
                foreach (WeaponPart weaponPart in this._weaponParts)
                {
                    Point[] weaponPoly = new Point[weaponPart.rotationalEllipses.Count];

                    int xOffset = 0;
                    int yOffset = 0;

                    for (int weaponPolyIndex = 0; weaponPolyIndex < weaponPoly.Length; weaponPolyIndex++)
                    {
                        weaponPoly[weaponPolyIndex] = GameEngine.Framework.Support.Shape.findPointOnEllipse(
                            weaponPart.rotationalEllipses[weaponPolyIndex].X + (weaponPart.rotationalEllipses[weaponPolyIndex].Width / 2),
                            weaponPart.rotationalEllipses[weaponPolyIndex].Y + (weaponPart.rotationalEllipses[weaponPolyIndex].Height / 2),
                            weaponPart.rotationalEllipses[weaponPolyIndex].Width,
                            weaponPart.rotationalEllipses[weaponPolyIndex].Height,
                            angleInDegrees);
                        weaponPoly[weaponPolyIndex].X += characterOffset.X + (characterOffset.Width / 2);

                        if (weaponPolyIndex == 0)
                        {
                            xOffset = characterOffset.X + (characterOffset.Width / 2) - handMovementEllipseOffset.X;
                            yOffset = -handMovementEllipseOffset.Y;
                        }

                        weaponPoly[weaponPolyIndex].X -= xOffset - (thickNessCount / 2) + (thicknessIndex);
                        weaponPoly[weaponPolyIndex].Y -= yOffset;
                    }
                    if (!weaponPart.roundOrigin)
                    {
                        game.graphics.drawPoly(
                            weaponPoly,
                            weaponPart.weaponBrush,
                            weaponPart.weaponPen);
                    }
                    else
                    {
                        game.graphics.drawPoly(
                            weaponPoly,
                            weaponPart.weaponBrush,
                            weaponPart.weaponPen);
                        this.RoundOrigin = weaponPoly[0];
                    }
                    
                }
            }
            this._hasFired = false;
            return;
        }

        public Point RoundOrigin { get; set; }

        public void useWeapon(
            long deltaTime,
            bool firing)
        {
            bool startFiring = false;
            if (this._capableOfFiring)
            {
                if (firing)
                {
                    if (this._deltaTotal == 0)
                    {
                        startFiring = true;
                    }
                    this._deltaTotal += (deltaTime);
                }
                else
                {
                    this._deltaTotal = 0;
                    this._lastMilliseconds = 0;
                }
                if (this._rounds > 0 && firing)
                {
                    if (this._lastMilliseconds + (this._milliSecondsBetweenRounds) < this._deltaTotal || startFiring)
                    {
                        this._hasFired = true;
                        this._rounds--;
                        Asset.List.blaster.play(0);
                        this._lastMilliseconds = this._deltaTotal;
                    }
                }
            }
            return;
        }

        public bool hasFired
        {
            get
            {
                return this._hasFired;
            }
        }

        public void addRounds(
            int rounds)
        {
            this._rounds += rounds;
            switch (this._weaponType)
            {
                case WeaponType.None:
                    if (this._rounds > maxRoundsNone)
                    {
                        this._rounds = maxRoundsNone;
                    }
                    break;
                case WeaponType.SubmachineGun:
                    if (this._rounds > maxRoundsSubmachineGun)
                    {
                        this._rounds = maxRoundsSubmachineGun;
                    }
                    break;
                default:
                    break;
            }
        }

        public int rounds
        {
            get
            {
                return this._rounds;
            }
        } 

        public int CompareTo(Weapon otherWeapon)
        {
            return otherWeapon.weaponType.CompareTo(this.weaponType);
        }

        public static Comparison<Weapon> weaponTypeComparison =
            delegate(
                Weapon weapon1, 
                Weapon weapon2)
            {
                return weapon1.weaponType.CompareTo(weapon2.weaponType);
            };
    }
}
