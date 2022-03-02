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


        public override double DE(Vector rayOrigin)
        {
            return Math.Max(0, (rayOrigin - position).Length() - radius);
        }

        protected override Vector GetNormal (Vector surfacePos)
        {
            return (surfacePos - position);
        }
    }
}
