using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Framework.Support
{
    public class Matrix3D
    {
        public double[,] Matrix;
        public Matrix3D()
        {
            Matrix = IdentityMatrix;
        }
        private static double[,] IdentityMatrix
        {
            get
            {
                var matrix = new double[3, 3];
                matrix[0, 0] =
                    matrix[1, 1] =
                    matrix[2, 2] = 1;
                matrix[0, 1] =
                    matrix[0, 2] =
                    matrix[1, 0] =
                    matrix[1, 2] =
                    matrix[2, 0] =
                    matrix[2, 1] = 0;
                return matrix;
            }
        }
        public static Matrix3D operator *(
            Matrix3D matrix1,
            Matrix3D matrix2)
        {
            var matrix = new Matrix3D();
            for (var m1 = 0; m1 < 3; m1++)
            {
                for (var m2 = 0; m2 < 3; m2++)
                {
                    matrix.Matrix[m1, m2] =
                        (matrix2.Matrix[m1, 0] * matrix1.Matrix[0, m2]) +
                        (matrix2.Matrix[m1, 1] * matrix1.Matrix[1, m2]) +
                        (matrix2.Matrix[m1, 2] * matrix1.Matrix[2, m2]);
                }
            }
            return matrix;
        }
        public static Matrix3D RotateAroundX(
            double radians)
        {
            var matrix = new Matrix3D();
            matrix.Matrix[1, 1] = Math.Cos(d: radians);
            matrix.Matrix[1, 2] = Math.Sin(a: radians);
            matrix.Matrix[2, 1] = -Math.Sin(a: radians);
            matrix.Matrix[2, 1] = Math.Cos(d: radians);
            return matrix;
        }
        public static Matrix3D RotateAroundY(
            double radians)
        {
            var matrix = new Matrix3D();
            matrix.Matrix[0, 0] = Math.Cos(d: radians);
            matrix.Matrix[0, 2] = -Math.Sin(a: radians);
            matrix.Matrix[2, 0] = Math.Sin(a: radians);
            matrix.Matrix[2, 2] = Math.Cos(d: radians);
            return matrix;
        }
        public static Matrix3D RotateAroundZ(
            double radians)
        {
            var matrix = new Matrix3D();
            matrix.Matrix[0, 0] = Math.Cos(d: radians);
            matrix.Matrix[0, 1] = Math.Sin(a: radians);
            matrix.Matrix[1, 0] = -Math.Sin(a: radians);
            matrix.Matrix[1, 1] = Math.Cos(d: radians);
            return matrix;
        }
        public static Matrix3D RotateByRadians(
            double radiansX,
            double radiansY,
            double radiansZ)
        {
            return RotateAroundX(radians: radiansX)
                * RotateAroundY(radians: radiansY)
                * RotateAroundZ(radians: radiansZ);
        }
        public static Matrix3D RotateByDegrees(
            double degreesX,
            double degreesY,
            double degreesZ)
        {
            return RotateByRadians(
                radiansX: Angle.DegreesToRadians(degrees: degreesX),
                radiansY: Angle.DegreesToRadians(degrees: degreesY),
                radiansZ: Angle.DegreesToRadians(degrees: degreesZ));
        }
    }
}
