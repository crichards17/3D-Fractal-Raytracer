using System;

namespace Scene
{
    public class Mandlebulb : SceneObjectEstimatable
    {
		private int iterations;
		
		public Mandlebulb(Vector position, Material material)
        {
            this.position = position;
            this.material = material;
			minDist = 0.00075;
			iterations = 20;
        }

        public override double DE(Vector rayOrigin)
        {
			Vector z = rayOrigin;
			double power = 6;
			double dr = 1.0;
			double r = 0.0;
			for (int i = 0; i < iterations; i++)
			{
				r = z.Length();
				if (r > 3.0)
				{
					break;
				}

				// convert to polar coordinates
				double theta = Math.Acos(z.z / r);
				double phi = Math.Atan2(z.y, z.x);
				dr = Math.Pow(r, power - 1.0) * power * dr + 1.0;

				// scale and rotate the point
				double zr = Math.Pow(r, power);
				theta = theta * power;
				phi = phi * power;

				// convert back to cartesian coordinates
				//z = zr * vec3(sin(theta) * cos(phi), sin(phi) * sin(theta), cos(theta));
				z = zr * new Vector(Math.Sin(theta) * Math.Cos(phi), Math.Sin(phi) * Math.Sin(theta), Math.Cos(theta)); 
				z += rayOrigin;
			}
			return 0.5 * Math.Log(r) * r / dr;
		}
    }
}
