using System;

namespace Scene
{
    public class Box : SceneObjectEstimatable
    {
        private Vector cornerPos;
        private Vector facingV;
        private Vector topV;

        #region constructors
        public Box(Vector center, Vector corner, Color color)
        {
            this.position = center;
            this.cornerPos = (corner - center).Abs() + center;
            this.color = color;
            this.facingV = Vector.unitZ;
            this.topV = Vector.unitY;
        }

        public Box(Vector center, Vector corner, Color color, Vector facingV, Vector topV)
        {
            this.position = center;
            // TODO: incorrect cornerPosition assignment
            this.cornerPos = corner.Abs();
            this.color = color;
            this.facingV = facingV.Unit();
            this.topV = topV.Unit();
        }
        #endregion

        public override double DE(Vector rayOrigin)
        {
            // Base case: box is at the origin
            Vector p = (rayOrigin - position).Abs() + position;
            double d = Math.Sqrt(Math.Pow(Math.Max(p.x - cornerPos.x, 0), 2) + Math.Pow(Math.Max(p.y - cornerPos.y, 0), 2) + Math.Pow(Math.Max(p.z - cornerPos.z, 0), 2));
            return d;

            // Have not yet accounted for translated / rotated cube.
        }

        protected override Vector GetNormal(Vector surfacePos)
        {
            // Transform surfacePos to the Quad1 corner equivalent
            Vector absFrag = (surfacePos - position).Abs() + position;

            // For each component (x, y, z):
            //  If Corner.x - absFrag.x is 0, return that component of surfacePos.
            //  If Corner.x - absFrag.x is nonzero, return 0.

            // This works...
            #region conditionals solution
            /*if (Utilities.IsEqualApprox(cornerPos.x, absFrag.x))
            {
                return new Vector(surfacePos.x, 0, 0);
            }
            else if (Utilities.IsEqualApprox(cornerPos.y, absFrag.y)) 
            { 
                return new Vector(0, surfacePos.y, 0);
            }
            else if (Utilities.IsEqualApprox(cornerPos.z, absFrag.z))
            {
                return new Vector(0, 0, surfacePos.z);
            }
            else
            {
                return Vector.origin;
            }
            */
            #endregion

            // ...but this sparks joy
            return new Vector(surfacePos.x * (int)(1.0 + 2 * Utilities.eps - (cornerPos.x - absFrag.x) / (cornerPos.x - position.x)), surfacePos.y * (int)(1.0 + 2 * Utilities.eps - (cornerPos.y - absFrag.y) / (cornerPos.y - position.y)), surfacePos.z * (int)(1.0 + 2 * Utilities.eps - (cornerPos.z - absFrag.z) / (cornerPos.z - position.z)));
        }
    }
}
