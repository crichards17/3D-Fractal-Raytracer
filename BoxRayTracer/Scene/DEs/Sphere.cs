using System;

namespace Scene
{
    public class Sphere : ISceneObjectEstimatable
    {
        public Vector position { get; private set; }

        private readonly double radius;
        public Color color { get; private set; }

        public Sphere(Vector position, double radius, Color color)
        {
            this.position = position;
            this.radius = radius;
            this.color = color;
        }


        public double DE(Vector p)
        {
            return Math.Max(0, (p - position).Length() - radius);
        }

        public Vector Normal(Vector surfacePos)
        {
            return (surfacePos - position).Unit();
        }
    }
}
