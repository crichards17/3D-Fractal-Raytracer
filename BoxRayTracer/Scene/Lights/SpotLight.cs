using System.Drawing;

namespace Scene
{
    public class SpotLight : IPointLight
    {
        public System.Drawing.Color Color { get; private set; }
        public double Intensity { get; private set; }
        public Vector Position { get; private set; }
        public Vector FacingVector { get; private set; }
        public double BoundingAngle { get; private set; } 
        // The angle, in degrees, between the facing vector and the boundary of the light cone.
        //      May want to enforce 0 <= BA <= 180 for logical behavior.

        public SpotLight(System.Drawing.Color color, double intensity, Vector position, Vector facingVector, double boundingAngle)
        {
            Color = color;
            Intensity = intensity;
            Position = position;
            FacingVector = facingVector.Unit();
            BoundingAngle = boundingAngle;
        }

        public bool IsIlluminating(Vector objPos)
        {
            return Utilities.RadiansToDegrees((objPos - Position).GetTheta(FacingVector)) <= BoundingAngle;
        }
    }
}
