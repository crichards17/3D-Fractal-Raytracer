using System;

namespace Scene
{
    public class Mandlebox : SceneObjectEstimatable
    {
        int iterations;
        double scale;
        double foldingLimit;
        
        public Mandlebox(Vector position, Material material)
        {
            this.position = position;
            this.material = material;
            this.iterations = 16;
            this.scale = 1.0;
            this.foldingLimit = 1.0;
            minDist = 0.0075;
        }
        
        public override double DE(Vector z)
        {
            Vector offset = z;
            double dr = 1.0;
            for (int n = 0; n < this.iterations; n++)
            {
                BoxFold(ref z, ref dr);       // Reflect
                SphereFold(ref z, ref dr);    // Sphere Inversion

                z = this.scale * z + offset;  // Scale & Translate
                dr = dr * Math.Abs(scale) + 1.0;
            }
            double r = z.Length();
            return r / Math.Abs(dr);
        }

        void BoxFold(ref Vector z, ref double dz)
        {
            z = z.Clamp(-this.foldingLimit, this.foldingLimit) * 2.0 - z;
        }
        void SphereFold(ref Vector z, ref double dz)
        {
            double r2 = z.Dot(z);
            if (r2 < 0.5)
            {
                // linear inner scaling
                double temp = (1.0 / 0.5);
                z *= temp;
                dz *= temp;
            }
            else if (r2 < 1.0)
            {
                // this is the actual sphere inversion
                double temp = (1.0 / r2);
                z *= temp;
                dz *= temp;
            }
        }
    }
}
