
namespace Scene
{
    public class AmbientLight : ISceneLight
    {
        public Color color { get; private set; }

        public double iAmbient { get; private set;}

        public double iDiffuse { get; private set;}

        public double iSpecular { get; private set;}

        public AmbientLight(Color color, double iAmbient)
        {
            this.color = color;
            this.iAmbient = iAmbient;
            iDiffuse = 0.0;
            iSpecular = 0.0;
        }

        public Vector VToLight(Vector objPos)
        {
            return Vector.origin;
        }
    }
}
