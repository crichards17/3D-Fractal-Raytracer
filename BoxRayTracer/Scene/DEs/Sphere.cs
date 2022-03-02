using System;

namespace Scene
{
    public class Sphere : SceneObjectEstimatable
    {

        private readonly double radius;

        public Sphere(Vector position, Color color, double radius)
        {
            this.position = position;
            this.color = color;
            this.radius = radius;
        }


        public override double DE(Vector p)
        {
            return Math.Max(0, (p - position).Length() - radius);
        }

        protected override Vector GetNormal (Vector surfacePos)
        {
            return (surfacePos - position);
        }
    }
}
