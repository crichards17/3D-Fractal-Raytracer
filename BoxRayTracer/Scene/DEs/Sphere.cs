using System;

namespace Scene
{
    public class Sphere : IDistanceEstimatable
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

        public Vector Normal(Vector surfacePos)
        {
            return (surfacePos - pos).Unit();
        }
    }
}
