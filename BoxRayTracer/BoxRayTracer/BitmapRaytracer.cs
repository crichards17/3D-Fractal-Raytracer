// #define Singlethread
using Scene;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Math = System.Math;

namespace BoxRayTracer
{
    internal class BitmapRaytracer
    {
        private readonly Camera camera;
        private readonly SceneStage sceneStage;
        private readonly int dWidth;
        private readonly int dHeight;
        private readonly double maxDist;


        public BitmapRaytracer(SceneStage sceneStage, double maxDist, int dWidth, int dHeight, double fov, Vector camPos, Vector lookAt)
        {
            this.camera = new Camera(camPos, lookAt, fov, 1, (double)dHeight / dWidth, dWidth, dHeight);
            this.sceneStage = sceneStage;
            this.dWidth = dWidth;
            this.dHeight = dHeight;
            this.maxDist = maxDist;
        }


        /// <summary>
        /// Renders the current Scene and returns the resulting Bitmap
        /// </summary>
        /// <returns></returns>
        public Bitmap Render()
        {
            Bitmap image = new(dWidth, dHeight);
            Scene.Color[] colorLinearArray = GetPixelArray();
            for (int linearIndex = 0; linearIndex < colorLinearArray.Length; linearIndex++)
            {
                int x = linearIndex % dWidth;
                int y = linearIndex / dWidth;
                image.SetPixel(x, y, ToDrawingColor(colorLinearArray[linearIndex]));
            }
            return image;
        }

        /// <summary>
        /// Returns a linear array of Scene.Colors corresponding to a pixel matrx of the Scene's dWidth and dHeight.
        /// </summary>
        /// <returns></returns>
        private Scene.Color[] GetPixelArray()
        {
            Scene.Color[] outColors = new Scene.Color[dWidth * dHeight];
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
#if !Singlethread
            Parallel.For(0, dWidth * dHeight, parallelOptions, pixelIndex =>
            {

                uint x = (uint)(pixelIndex % dWidth);
                uint y = (uint)(pixelIndex / dWidth);
                outColors[pixelIndex] = RayMarch(x, y);
            });
#else
            for (uint pixelIndex = 0; pixelIndex < outColors.Length; pixelIndex++)
            {
                uint x = (uint)(pixelIndex % dWidth);
                uint y = (uint)(pixelIndex / dWidth);
                outColors[pixelIndex] = RayMarch(x, y);
            }
#endif
            return outColors;
        }

        /// <summary>
        /// Returns the Scene.Color result for a given pixel position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Scene.Color RayMarch(uint x, uint y)
        {
            camera.RayForPixel(x, y, out Vector pos, out Vector rayDir);
            double minDist = int.MaxValue;
            double currentDist = minDist;
            ISceneObjectEstimatable nearestObj = sceneStage.sceneObjects[0];
            for (int i = 0; i < sceneStage.sceneObjects.Length; i++)
            {
                double objDist = sceneStage.sceneObjects[i].DE(pos);
                if (objDist < minDist)
                {
                    minDist = objDist;
                    nearestObj = sceneStage.sceneObjects[i];
                }
            }
            currentDist = minDist;
            while (currentDist <= maxDist)
            {
                if (Utilities.IsEqualApprox(currentDist, 0))
                {
                    // Do the B.P. thing
                    Scene.Color outColor = new Scene.Color(0, 0, 0);
                    for (int i = 0; i < sceneStage.sceneLights.Length; i++)
                    {
                        outColor += BPContribution(sceneStage.sceneLights[i], pos, nearestObj);
                    }
                    return outColor;
                }
                pos += rayDir * currentDist;
                minDist = int.MaxValue;
                for (int i = 0; i < sceneStage.sceneObjects.Length; i++)
                {
                    double objDist = sceneStage.sceneObjects[i].DE(pos);
                    if (objDist < minDist)
                    {
                        minDist = objDist;
                        nearestObj = sceneStage.sceneObjects[i];
                    }
                }
                currentDist = minDist;
            }
            return sceneStage.backColor;
        }

        /// <summary>
        /// Returns camera data in the out vectors.
        /// </summary>
        /// <param name="camPos"></param>
        /// <param name="camFrus"></param>
        /// <param name="camRoll"></param>
        public void GetSceneParams(out Vector camPos, out Vector camFrus, out Vector camRoll)
        {
            camPos = camera.camPos;
            camFrus = camera.vFrus;
            camRoll = camera.vRoll;
        }

        /// <summary>
        /// Returns the additive ambient, diffuse, and specular lighting contributions
        ///     of the given ISceneLight on the given object fragment.
        /// </summary>
        /// <param name="sceneLight">Light source being evaluated</param>
        /// <param name="fragPos">Scene-space position of the object fragment</param>
        /// <param name="obj">Object being evaluated</param>
        /// <returns></returns>
        private Scene.Color BPContribution(ISceneLight sceneLight, Vector fragPos, ISceneObjectEstimatable obj)
        {
            Scene.Color compoundColor = new Scene.Color(0, 0, 0);

            // Ambient light component:
            compoundColor += sceneLight.color * sceneLight.iAmbient * obj.color;

            // Only apply Diffuse and Specular if the vector to the light source is non-zero
            //  (Enables SpotLight lighting region check, and avoids undefined light behavior)
            Vector vToLight = sceneLight.VToLight(fragPos);
            if (!vToLight.Equals(Vector.origin))
            {
                Vector normal = obj.Normal(fragPos);
                
                // Diffuse light component:
                if (sceneLight.iDiffuse != 0)
                {
                compoundColor += sceneLight.color * sceneLight.iDiffuse * Math.Max(normal.Dot(vToLight), 0) * obj.color;
                }

                // Specular light component:
                if (sceneLight.iSpecular != 0)
                {
                Vector halfV = (vToLight + (camera.camPos - fragPos).Unit()).Unit();
                compoundColor += sceneLight.color * sceneLight.iSpecular * Math.Pow(Math.Max(normal.Dot(halfV), 0.0), 32) * obj.color;
                }
            }
            
            return compoundColor;
        }

        /// <summary>
        /// Returns the System.Drawing.Color equivalent of the provided Scene.Color,
        ///     clamped to 255 RGB.
        /// </summary>
        /// <param name="sceneColor"></param>
        /// <returns></returns>
        private static System.Drawing.Color ToDrawingColor(Scene.Color sceneColor)
        {
            return System.Drawing.Color.FromArgb(255, Math.Min((int)(sceneColor.R * 255), 255), Math.Min((int)(sceneColor.G * 255), 255), Math.Min((int)(sceneColor.B * 255), 255));
        }
    }
}
