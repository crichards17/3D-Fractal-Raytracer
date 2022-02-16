using System.Drawing;

namespace Scene
{
    public class Light_Point : IPointLight
    {
        public Color Color { get; private set; }
        public double Intensity { get; private set; }
        public Vector Position { get; private set; }

        public Light_Point(Color color, double intensity, Vector position)
        {
            Color = color;
            Intensity = intensity;
            Position = position;
        }
    }
}
