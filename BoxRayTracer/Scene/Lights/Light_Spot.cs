using System.Drawing;

namespace Scene
{
    public class Light_Spot : IPointLight
    {
        public Color Color { get; private set; }
        public double Intensity { get; private set; }
        public Vector Position { get; private set; }
        public Vector FacingVector { get; private set; }
        public double BoundingAngle { get; private set; } 
        // The angle, in degrees, between the facing vector and the boundary of the light cone.
        //      May want to enforce 0 <= BA <= 180 for logical behavior.

        public Light_Spot(Color color, double intensity, Vector position, Vector facingVector, double boundingAngle)
        {
            Color = color;
            Intensity = intensity;
            Position = position;
            FacingVector = facingVector.Unit();
            BoundingAngle = boundingAngle;
        }

        public Boolean IsIlluminating(Vector objPos)
        {
            return Utilities.RadiansToDegrees((objPos - Position).GetTheta(FacingVector)) <= BoundingAngle;
        }
    }
}
