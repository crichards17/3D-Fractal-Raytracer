using System.Drawing;

namespace Scene
{
    public class PointLight : IPointLight
    {
        public System.Drawing.Color Color { get; private set; }
        public double Intensity { get; private set; }
        public Vector Position { get; private set; }

        public PointLight(System.Drawing.Color color, double intensity, Vector position)
        {
            Color = color;
            Intensity = intensity;
            Position = position;
        }

        public bool IsIlluminating(Vector pos) => true;
    }
}
