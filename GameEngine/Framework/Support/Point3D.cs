using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Framework.Support
{
    public class Point3D
    {
        public Point3D()
        {
            X = Y = Z = 0;
        }
        public Point3D(
            double x,
            double y,
            double z)
        {
            X = x;
            Y = y;
            Z = z;
            OriginalX = x;
            OriginalY = y;
            OriginalZ = z;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double OriginalX { get; set; }
        public double OriginalY { get; set; }
        public double OriginalZ { get; set; }
        public static Point3D operator *(
            Matrix3D matrix,
            Point3D point)
        {
            var x = point.X * matrix.Matrix[0, 0] +
                point.Y * matrix.Matrix[0, 1] +
                point.Z * matrix.Matrix[0, 2];
            var y = point.X * matrix.Matrix[1, 0] +
                point.Y * matrix.Matrix[1, 1] +
                point.Z * matrix.Matrix[1, 2];
            var z = point.X * matrix.Matrix[0, 0] +
                point.Y * matrix.Matrix[0, 1] +
                point.Z * matrix.Matrix[0, 2];
            point.X = x;
            point.Y = y;
            point.Z = z;
            return point;
        }
        public double PointOnTheScreenX(
            double perspecticeCoefficient,
            Point3D rotationCentrePoint)
        {
            return (X * perspecticeCoefficient)
                / (Z + perspecticeCoefficient)
                + rotationCentrePoint.X;
        }
        public double PointOnTheScreenY(
            double perspecticeCoefficient,
            Point3D rotationCentrePoint)
        {
            return (Y * perspecticeCoefficient)
                / (Z + perspecticeCoefficient)
                + rotationCentrePoint.Y;
        }
        public Point PointOnTheScreen(
            double perspecticeCoefficient,
            Point3D rotationCentrePoint)
        {
            return new Point(
                x: (int)PointOnTheScreenX(
                    perspecticeCoefficient: perspecticeCoefficient,
                    rotationCentrePoint: rotationCentrePoint),
                y: (int)PointOnTheScreenY(
                    perspecticeCoefficient: perspecticeCoefficient,
                    rotationCentrePoint: rotationCentrePoint));
        }
    }
}
