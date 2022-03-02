//#define debug
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
            Vector p = rayOrigin.Abs();
            double d = Math.Sqrt(Math.Pow(Math.Max(p.x - cornerPos.x, 0), 2) + Math.Pow(Math.Max(p.y - cornerPos.y, 0), 2) + Math.Pow(Math.Max(p.z - cornerPos.z, 0), 2));
            return d;

            // Have not yet accounted for translated / rotated cube.
        }

        protected override Vector GetNormal(Vector surfacePos)
        {
            // Transform surfacePos to the Quad1 corner equivalent
            Vector absFrag = surfacePos.Abs();

            // For each component (e.g. x):
            //  If Corner.x - absFrag.x is 0, return the unit representation of that component.
            //  If Corner.x - absFrag.x is nonzero, return 0.
            Vector n = new Vector(Math.Floor(1.0 + Utilities.eps - cornerPos.x + absFrag.x) * Math.Sign(surfacePos.x), Math.Floor(1.0 + Utilities.eps - cornerPos.y + absFrag.y) * Math.Sign(surfacePos.y), Math.Floor(1.0 + Utilities.eps - cornerPos.z + absFrag.z) * Math.Sign(surfacePos.z));
#if debug   
            if (!Utilities.IsEqualApprox(n.Length(), 1.0))
            {
                throw new InvalidOperationException();
            }
#endif
            return n;
        }
    }
}
