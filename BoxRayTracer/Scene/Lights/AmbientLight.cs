
namespace Scene
{
    public class AmbientLight : ISceneLight
    {
        public Color Color { get; private set; }
        public double Intensity { get; private set; }

        public AmbientLight(Color color, double intensity)
        {
            this.Color = color;
            this.Intensity = intensity;
        }
    }
}
