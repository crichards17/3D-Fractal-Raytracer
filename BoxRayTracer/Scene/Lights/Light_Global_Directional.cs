using System.Drawing;

namespace Scene
{
    public class Light_Global_Directional : ISceneLight
    {
        public Color Color { get; private set; }
        public double Intensity { get; private set; }
        public Vector Direction { get; private set; }

        public Light_Global_Directional(Color color, double intensity, Vector direction)
        {
            this.Color = color;
            this.Intensity = intensity;
            this.Direction = direction.Unit();
        }
    }
}
