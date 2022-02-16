using System.Drawing;

namespace Scene
{
    public class Light_Ambient : ISceneLight
    {
        public Color Color { get; private set; }
        public double Intensity { get; private set; }

        public Light_Ambient(Color color, double intensity)
        {
            this.Color = color;
            this.Intensity = intensity;
        }
    }
}
