//#define Singlethread
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
                outColors[pixelIndex] = ColorForPixel(x, y);
            });
#else
            for (uint pixelIndex = 0; pixelIndex < outColors.Length; pixelIndex++)
            {
                uint x = (uint)(pixelIndex % dWidth);
                uint y = (uint)(pixelIndex / dWidth);
                if (x == dWidth / 3 && y == dHeight / 3)
                {
                    Console.WriteLine();
                }
                outColors[pixelIndex] = ColorForPixel(x, y);
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
        private Scene.Color ColorForPixel(uint x, uint y)
        {
            camera.RayForPixel(x, y, out Vector pos, out Vector rayDir);
            RayMarch(pos, rayDir, Defaults.maxMarch, out int marchCount, out SceneObjectEstimatable collisionObj, out Vector? fragPos);
            if (collisionObj != null && fragPos != null)
            {
                // Do the B.P. thing
                Scene.Color outColor = new Scene.Color(0, 0, 0);
                Vector normal = collisionObj.Normal(fragPos.Value, rayDir);
                for (int i = 0; i < sceneStage.sceneLights.Length; i++)
                {
                    outColor += BPContribution(sceneStage.sceneLights[i], fragPos.Value, normal, collisionObj.material, collisionObj.minDist, camera.camPos, marchCount);
                }
                // Add reflections;
                outColor += GetReflections(0, collisionObj, fragPos.Value, camera.camPos);

                return outColor;
            }
            return sceneStage.backColor;
        }

        private Scene.Color GetReflections(int reflectionDepth, SceneObjectEstimatable collisionObj, Vector fragmentPos, Vector fromPos)
        {
            Scene.Color reflectionColor = Scene.Color.Black;
            if (reflectionDepth < Defaults.maxReflections)
            {
                Vector incident = (fragmentPos - fromPos).Unit();
                Vector normal = collisionObj.Normal(fragmentPos, incident);
                Vector reflectV = (incident - 2 * incident.Dot(normal) * normal);
                RayMarch(fragmentPos + reflectV * Utilities.eps, reflectV, Defaults.maxMarch, out int marchCount, out SceneObjectEstimatable reflectionObj, out Vector? reflectionObjPos);
                if (reflectionObj != null && reflectionObjPos != null)
                {
                    Scene.Color reflectObjColor = Scene.Color.Black;
                    for (int i = 0; i < sceneStage.sceneLights.Length; i++)
                    {
                        reflectObjColor += BPContribution(sceneStage.sceneLights[i], reflectionObjPos.Value, normal, reflectionObj.material, reflectionObj.minDist, fragmentPos, marchCount);
                    }
                    reflectObjColor += GetReflections(reflectionDepth + 1, reflectionObj, reflectionObjPos.Value, fragmentPos);
                    reflectionColor += reflectObjColor * collisionObj.material.reflectivity * collisionObj.material.diffuseColor;
                }
            }
            return reflectionColor;
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
        ///     of the given SceneLight on the given object fragment.
        /// </summary>
        /// <param name="sceneLight">Light source being evaluated</param>
        /// <param name="fragPos">Scene-space position of the object fragment</param>
        /// <param name="obj">Object being evaluated</param>
        /// <returns></returns>
        private Scene.Color BPContribution(SceneLight sceneLight, Vector fragPos, Vector normal, Material objMaterial, double minDist, Vector camPos, int marches)
        {
            Scene.Color compoundColor = Scene.Color.Black;

            // Ambient light component with occlusion:
            double occludeLevel = 1.0 / (255 * (double)marches / Defaults.maxMarch);
            occludeLevel += (1 - occludeLevel) / 8;
            compoundColor += sceneLight.color * sceneLight.iAmbient * objMaterial.ambientColor * occludeLevel;

            // Only apply Diffuse and Specular if the vector to the light source is non-zero
            //  (Enables SpotLight lighting region check, and avoids undefined light behavior)
            //  and if the vToLight is not parallel to the fragment
            //  (prevents unnecessary max-march iterations on flat surfaces)
            Vector vToLight = sceneLight.VToLight(fragPos);

            if (!vToLight.Equals(Vector.origin) && !vToLight.IsOrtho(normal))
            {
                // Ray march along vToLight
                RayMarch(fragPos + normal * minDist, vToLight, int.MaxValue, out int marchCount, out SceneObjectEstimatable collisionObj, out Vector? _);
                
                // Apply diffuse and specular if no object is intersected (shadowing)
                if (collisionObj == null)
                {

                    // Diffuse light component:
                    if (sceneLight.iDiffuse != 0)
                    {
                        compoundColor += sceneLight.color * sceneLight.iDiffuse * Math.Max(normal.Dot(vToLight), 0) * objMaterial.diffuseColor;
                    }

                    // Specular light component:
                    if (sceneLight.iSpecular != 0)
                    {
                        Vector halfV = (vToLight + (camPos - fragPos).Unit()).Unit();
                        compoundColor += sceneLight.color * sceneLight.iSpecular * Math.Pow(Math.Max(normal.Dot(halfV), 0.0), objMaterial.reflectivity * 64) * objMaterial.specularColor;
                    }
                }
            }
            
            return compoundColor;
        }

        private void GetNearestObject(Vector pos, out SceneObjectEstimatable nearestObj, out double minDist)
        {
            minDist = int.MaxValue;
            nearestObj = sceneStage.sceneObjects[0];
            foreach (SceneObjectEstimatable checkObj in sceneStage.sceneObjects)
            {
                double objDist = checkObj.DE(pos);
                if (objDist < minDist)
                {
                    minDist = objDist;
                    nearestObj = checkObj;
                }
            }
        }

        private void RayMarch(Vector pos, Vector rayDir, int maxMarch, out int marchCount, out SceneObjectEstimatable collisionObj, out Vector? fragPos)
        {
            GetNearestObject(pos, out SceneObjectEstimatable nearestObj, out double currentDist);
            marchCount = 0;

            while (currentDist <= maxDist && marchCount < maxMarch)
            {
                if (Utilities.IsEqualApprox(currentDist, 0))
                {
                    collisionObj = nearestObj;
                    fragPos = pos;
                    return;
                }
                else if (currentDist < nearestObj.minDist)
                {
                    collisionObj = nearestObj;
                    fragPos = pos -= rayDir * collisionObj.minDist;
                    return;
                }
                pos += rayDir * currentDist;
                GetNearestObject(pos, out nearestObj, out currentDist);
                marchCount++;
            }
            collisionObj = null;
            fragPos = null;
            marchCount = 0;
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
