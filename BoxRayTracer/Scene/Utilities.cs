
using System;

namespace Scene
{
    public static class Utilities
    {
        public const double eps = 0.0000001;
        public static bool IsEqualApprox(double a, double b, double eps = Utilities.eps)
        {
            return Math.Abs(a - b) < eps;
        }

        public static double DegreesToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

        public static double RadiansToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }
    }
}
