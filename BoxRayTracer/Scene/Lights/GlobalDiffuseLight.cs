using System.Drawing;

namespace Scene
{
    public class GlobalDiffuseLight : ISceneLight
    {
        public System.Drawing.Color Color { get; private set; }
        public double Intensity { get; private set; }
        public Vector Direction { get; private set; }

        public GlobalDiffuseLight(System.Drawing.Color color, double intensity, Vector direction)
        {
            this.Color = color;
            this.Intensity = intensity;
            this.Direction = direction.Unit();
        }
    }
}
