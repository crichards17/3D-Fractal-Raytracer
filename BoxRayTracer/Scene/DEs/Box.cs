using System;

namespace Scene
{
    public class Box : SceneObjectEstimatable
    {
        private Vector widthHeightDepth;
        private Vector facingV;
        private Vector topV;

        #region constructors
        public Box(Vector center, Vector widthHeightDepth, Color color)
        {
            this.position = center;
            this.widthHeightDepth = widthHeightDepth;
            this.color = color;
            this.facingV = Vector.unitZ;
            this.topV = Vector.unitY;
        }

        public Box(Vector center, Vector widthHeightDepth, Color color, Vector facingV, Vector topV)
        {
            this.position = center;
            // TODO: incorrect cornerPosition assignment
            this.widthHeightDepth = widthHeightDepth;
            this.color = color;
            this.facingV = facingV.Unit();
            this.topV = topV.Unit();
        }
        #endregion

        public override double DE(Vector rayOrigin)
        {
            Vector p = (rayOrigin - position).Abs();
            double d = Math.Sqrt(Math.Pow(Math.Max(p.x - widthHeightDepth.x, 0), 2) + Math.Pow(Math.Max(p.y - widthHeightDepth.y, 0), 2) + Math.Pow(Math.Max(p.z - widthHeightDepth.z, 0), 2));
            return d;

            // Have not yet accounted for translated / rotated cube.
        }

        protected override Vector GetNormal(Vector surfacePos)
        {
            // Transform surfacePos to the Quad1 corner equivalent
            Vector absFrag = (surfacePos - position).Abs();

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
            return new Vector(surfacePos.x * (int)(1.0 + 2 * Utilities.eps - (widthHeightDepth.x - absFrag.x) / widthHeightDepth.x), surfacePos.y * (int)(1.0 + 2 * Utilities.eps - (widthHeightDepth.y - absFrag.y) / widthHeightDepth.y), surfacePos.z * (int)(1.0 + 2 * Utilities.eps - (widthHeightDepth.z - absFrag.z) / widthHeightDepth.z));
        }
    }
}
