
namespace Scene
{
    public class AmbientLight : SceneLight
    {

        public AmbientLight(Color color, double iAmbient)
        {
            this.color = color;
            this.iAmbient = iAmbient;
            iDiffuse = 0.0;
            iSpecular = 0.0;
        }

        protected override Vector GetVToLight(Vector objPos)
        {
            return Vector.origin;
        }
    }
}
