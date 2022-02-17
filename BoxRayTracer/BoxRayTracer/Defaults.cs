using Scene;
using System;
using Color = System.Drawing.Color;

namespace BoxRayTracer
{
    internal class Defaults
    {
        // Object defaults
        public const double camPosX = 0;
        public const double camPosY = 0;
        public const double camPosZ = -5;

        // Camera defaults
        public const double objPosX = 0;
        public const double objPosY = 0;
        public const double objPosZ = 0;

        public const double camFrusX = 0;
        public const double camFrusY = 0;
        public const double camFrusZ = 1;

        public const double fov = 90;

        // Shape defaults
        public static readonly String[] shapeList = new String[] { "Sphere" };
        public const int shapeDefaultIndex = 0;
        public const double radius = 1;

        // Render defaults
        public const double maxDist = 20;
        public const double imgWidth = 256;
        public const double imgHeight = 256;

        // Light defaults
        public static readonly AmbientLight globalAmbient = new AmbientLight(Color.Gray, 0.1);
        public static readonly GlobalDiffuseLight[] globalDiffuseLights = new GlobalDiffuseLight[] {new (Color.Pink, 0.1, new Vector(1,0,0))};
        public static readonly IPointLight[] pointLights = new IPointLight[] { new PointLight(Color.LightBlue, 0.2, new Vector(5, 5, 0))};
    }
}
