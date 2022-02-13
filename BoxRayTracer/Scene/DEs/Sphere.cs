namespace Scene
{
    public class Sphere : IDistanceEstimator
    {
        private readonly Vector pos;
        private readonly double radius;

        public Sphere(Vector pos, double radius)
        {
            this.pos = pos;
            this.radius = radius;
        }

        public double DE(Vector p)
        {
            return Math.Max(0, (p - pos).Length() - radius);
        }
    }
}
