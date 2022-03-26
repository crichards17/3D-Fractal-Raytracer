using System;

namespace Scene
{
    public class Sphere : SceneObjectEstimatable
    {

        private readonly double radius;

        public Sphere(Vector position, Material material, double radius)
        {
            this.position = position;
            this.material = material;
            this.radius = radius;
        }


        public override double DE(Vector rayOrigin)
        {
            return Math.Max(0, (rayOrigin - position).Length() - radius);
        }
    }
}
