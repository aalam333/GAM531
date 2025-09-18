using OpenTK.Mathematics;
using System.Security.AccessControl;

namespace VectorMatrixOperations
{
    // This class is for a 3D Vector
    public class Vector
    {

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector Add(Vector term1, Vector term2)
        {
            double newX = term1.X + term2.X;
            double newY = term1.Y + term2.Y;
            double newZ = term1.Z + term2.Z;

            Vector result = new Vector(newX, newY, newZ);
            return result;
        }

        public static Vector Subtract(Vector subtractfrom, Vector subtractvalue)
        { 
            double newX = subtractfrom.X - subtractvalue.X;
            double newY = subtractfrom.Y - subtractvalue.Y;
            double newZ = subtractfrom.Z - subtractvalue.Z;

            Vector result = new Vector(newX, newY, newZ);
            return result;
        
        }

        public static double DotProduct(Vector vector1, Vector vector2) {
            return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
        }

        public static Vector CrossProduct(Vector vector1, Vector vector2)
        {
            double x = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
            double y = vector1.Z * vector2.X - vector1.X * vector2.Z;
            double z = vector1.X * vector2.Y - vector1.Y * vector2.X;

            return new Vector(x, y, z);
        }
    }

    public class Matrix
    {
        private int m, n;

        private int[,] array;

        public Matrix(int m, int n)
        {
            array = new int[m, n];
        }
    }
}
