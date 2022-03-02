
namespace Scene
{
    public class SpotLight : SceneLight
    {
        private Vector position;
        private Vector facingVector { get; set; }
        private double boundingAngle { get; set; }
        // The angle, in degrees, between the facing vector and the boundary of the light cone.
        //      May want to enforce 0 <= BA <= 180 for logical behavior.

        public SpotLight(Color color, double iDiffuse, double iSpecular, Vector position, Vector facingVector, double boundingAngle)
        {
            this.color = color;
            this.iDiffuse = iDiffuse;
            this.iSpecular = iSpecular;
            iAmbient = 0;

            this.position = position;
            this.facingVector = facingVector;
            this.boundingAngle = boundingAngle;
        }

        public SpotLight(Color color, double iDiffuse, double iSpecular, double iAmbient, Vector position, Vector facingVector, double boundingAngle)
        {
            this.color = color;
            this.iDiffuse = iDiffuse;
            this.iSpecular = iSpecular;
            this.iAmbient = iAmbient;

            this.position = position;
            this.facingVector = facingVector;
            this.boundingAngle = boundingAngle;
        }

        protected override Vector GetVToLight(Vector objPos)
        {
            Vector vToLight = (objPos - position).Unit();
            if(Utilities.RadiansToDegrees(vToLight.GetTheta(facingVector)) <= boundingAngle)
            {
                return vToLight;
            }
            return Vector.origin;
        }
    }
}
