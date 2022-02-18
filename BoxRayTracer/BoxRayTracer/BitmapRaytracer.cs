using Scene;
using System.Drawing;
using Math = System.Math;

namespace BoxRayTracer
{
    internal class BitmapRaytracer
    {
        private readonly IDistanceEstimatable dE;
        private readonly Camera camera;
        private readonly int dWidth;
        private readonly int dHeight;
        private readonly double maxDist;
        private readonly Scene.Color objColor;
        private readonly Scene.Color backColor;

        // Lights
        private readonly ISceneLight[] sceneLights;

        public BitmapRaytracer(IDistanceEstimatable dE, double maxDist, int dWidth, int dHeight, double fov, Vector camPos, Vector lookAt, Scene.Color objColor, Scene.Color backColor, ISceneLight[] sceneLights)
        {
            this.dE = dE;
            this.dWidth = dWidth;
            this.dHeight = dHeight;
            this.maxDist = maxDist;
            this.camera = new Camera(camPos, lookAt, fov, 1, (double)dHeight / dWidth, dWidth, dHeight);
            this.objColor = objColor;
            this.backColor = backColor;
            this.sceneLights = sceneLights;
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

        private System.Drawing.Color RayMarch(uint x, uint y)
        {
            camera.RayForPixel(x, y, out Vector pos, out Vector rayDir);
            double currentDist = dE.DE(pos);
            while (currentDist <= maxDist)
            {
                if (Utilities.IsEqualApprox(currentDist, 0))
                {
                    // Do the B.P. thing
                    Scene.Color outColor = new Scene.Color(0, 0, 0);
                    for (int i = 0; i < sceneLights.Length; i++)
                    {
                        outColor += BPContribution(sceneLights[i], pos);
                    }
                    return ToDrawingColor(outColor);
                }
                pos += rayDir * currentDist;
                currentDist = dE.DE(pos);
            }
            return ToDrawingColor(backColor);
        }

        public void GetSceneParams(out Vector camPos, out Vector camFrus, out Vector camRoll)
        {
            camPos = camera.camPos;
            camFrus = camera.vFrus;
            camRoll = camera.vRoll;
        }

        private Scene.Color BPContribution(ISceneLight sceneLight, Vector fragPos)
        {
            Scene.Color compoundColor = new Scene.Color(0, 0, 0);
            
            // Ambient light component:
            compoundColor += sceneLight.color * sceneLight.iAmbient * objColor;
            
            // Only apply Diffuse and Specular if the vector to the light source is non-zero
            //  (Enables SpotLight lighting region check, and avoids undefined light behavior)
            Vector vToLight = sceneLight.VToLight(fragPos);
            if (!vToLight.Equals(Vector.origin))
            {
                Vector normal = dE.Normal(fragPos);
                // Diffuse light component:
                compoundColor += sceneLight.color * sceneLight.iDiffuse * Math.Max(normal.Dot(vToLight), 0);

                // Specular light component:
                // TODO: Correct to Blinn calc -- currently using Phong specular calc.
                Vector halfV = (vToLight + (camera.camPos - fragPos).Unit()).Unit();
                compoundColor += sceneLight.color * sceneLight.iSpecular * Math.Pow(Math.Max(normal.Dot(halfV), 0.0), 32);
            }


            return compoundColor;
        }

        // If this is going to be useful elsewhere, it could go onto the Color class.
        //  However, seems likely to only be useful to BRT.
        private static System.Drawing.Color ToDrawingColor(Scene.Color sceneColor)
        {
            return System.Drawing.Color.FromArgb(255, Math.Min((int) (sceneColor.R * 255), 255), Math.Min((int) (sceneColor.G * 255), 255), Math.Min((int) (sceneColor.B * 255), 255));
        }
    }
}
