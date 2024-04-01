namespace GameEngine.Framework.Support
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Text.Json;

    public static class Shape
    {
        private const double _180OverPi = 180.00 / Math.PI;
        private const double _PiOver180 = Math.PI / 180.00;
        private static double[] _cosArray = new double[360];
        private static double[] _sinArray = new double[360];
        private static double[] _tanArray = new double[360];
        private static bool _trigArrayBuilt = false;

        private static void init()
        {
            // if this is the first time the function is being called,
            // prepare the trig arrays for every other call
            if (!_trigArrayBuilt)
            {
                for (int i = 0; i < 360; i++)
                {
                    double radian = i * _PiOver180;
                    _cosArray[i] = Math.Cos(radian);
                    _sinArray[i] = Math.Sin(radian);
                    _tanArray[i] = Math.Tan(radian);
                }
                _trigArrayBuilt = true;
            }
            return;
        }

        public static Point findTriangleCorner(
            int hypotenuse,
            int angleInDegrees)
        {
            // work out the angle in degrees between 0 and 359
            angleInDegrees = angleInDegrees - 90;
            if (angleInDegrees < 0)
            {
                angleInDegrees = 360 + angleInDegrees;
            }
            else if (angleInDegrees > 360)
            {
                angleInDegrees = angleInDegrees - 360;
            }

            return new Point(
                x: (int)(_cosArray[angleInDegrees] * hypotenuse),
                y: (int)(_sinArray[angleInDegrees] * hypotenuse));
        }
        public static Point findPointOnEllipse(
           int startingOffsetX,
           int startingOffsetY,
           int ellipseWidth,
           int ellipseHeight,
           int angleInDegrees)
        {
            init();

            // http://stackoverflow.com/questions/2781206/finding-a-point-on-an-ellipse-circumference-which-is-inside-a-rectangle-having-c
            // double X = C_x + (w / 2) * cos(t);
            // double Y = C_y + (h / 2) * sin(t);

            // work out the angle in degrees between 0 and 359
            angleInDegrees = angleInDegrees - 90;
            if (angleInDegrees < 0)
            {
                angleInDegrees = 360 + angleInDegrees;
            }
            else if (angleInDegrees >= 360)
            {
                angleInDegrees = angleInDegrees - 360;
            }

            // return a new point of where the position is on the ellipse
            return new Point(
                x: (int)(startingOffsetX + (ellipseWidth / 2) * _cosArray[angleInDegrees]), 
                y: (int)(startingOffsetY + (ellipseHeight / 2) * _sinArray[angleInDegrees]));
        }

        public static int findAngleBetweenPoints(
            double deltaX,
            double deltaY)
        {
            init();

            double angleInDegrees = Math.Atan2(deltaY, deltaX) * _180OverPi + 90;
            if (angleInDegrees < 0)
            {
                angleInDegrees = 360 + angleInDegrees;
            }
            return (int)angleInDegrees;
        }

        public static bool doesRectangleOverlaps(
            Rectangle rect1,
            Rectangle rect2)
        {
            init();

            return rect1.X + rect1.Width >= rect2.X
                && rect1.X <= rect2.X + rect2.Width
                && rect1.Y + rect1.Height >= rect2.Y
                && rect1.Y <= rect2.Y + rect2.Height;
        }
        public static Point rotatePoint(
            Point pointToRotate, 
            Point centerPoint, 
            double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y)  + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }
        public static Point ToPoint(
            this Point3D self,
            double perspecticeCoefficient,
            Point3D rotationCentrePoint)
        {
            return self.PointOnTheScreen(
                perspecticeCoefficient: perspecticeCoefficient,
                rotationCentrePoint: rotationCentrePoint);
        }
        public static Point[] ToPoints(
            this List<Point3D> self,
            double perspecticeCoefficient,
            Point3D rotationCentrePoint)
        {
            return self.Select(n => n.ToPoint(
                perspecticeCoefficient: perspecticeCoefficient,
                rotationCentrePoint: rotationCentrePoint)).ToArray();
        }
        public static List<List<Point3D>> Clone(
            this List<List<Point3D>> self)
        {
            string json = JsonSerializer.Serialize(self);
            List<List<Point3D>> output = JsonSerializer.Deserialize<List<List<Point3D>>>(json);
            return output;
        }
    }
}
