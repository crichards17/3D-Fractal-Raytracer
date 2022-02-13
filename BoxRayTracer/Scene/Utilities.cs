
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
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }
    }
}
