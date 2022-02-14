using Scene;
using System.Drawing;

namespace BoxRayTracer
{
    internal class BitmapRaytracer
    {
        private readonly IDistanceEstimator dE;
        private readonly Camera camera;
        private readonly int dWidth;
        private readonly int dHeight;
        private readonly double maxDist;

        public BitmapRaytracer(IDistanceEstimator dE, double maxDist, int dWidth, int dHeight, int fov, Vector camPos, Vector lookAt)
        {
            this.dE = dE;
            this.dWidth = dWidth;
            this.dHeight = dHeight;
            this.maxDist = maxDist;
            this.camera = new Camera(camPos, lookAt, fov, 1, (double)dHeight / dWidth, dWidth, dHeight);
        }

        public Bitmap Render()
        {
            Bitmap image = new(dWidth, dHeight);
            for (int i = 0; i < dWidth; i++)
            {
                for (int j = 0; j < dHeight; j++)
                {
                    image.SetPixel(i, j, RayMarch((uint)i, (uint)j));
                }
            }
            return image;
        }

        private Color RayMarch(uint x, uint y)
        {
            camera.RayForPixel(x, y, out Vector pos, out Vector rayDir);
            double currentDist = dE.DE(pos);
            while (!Utilities.IsEqualApprox(currentDist, 0))
            {
                pos += rayDir * currentDist;
                currentDist = dE.DE(pos);
                if (currentDist > maxDist)
                {
                    return Color.Black;
                }
            }
            return Color.Purple;
        }

        public void GetSceneParams(out Vector camPos, out Vector camFrus, out Vector camRoll)
        {
            camPos = camera.GetPos();
            camFrus = camera.GetVFrus();
            camRoll = camera.GetVRoll();
        }
    }
}
