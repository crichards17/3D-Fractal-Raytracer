using Scene;
using System;

namespace BoxRayTracer
{
    internal class Defaults
    {
        // Object defaults
        public const double camPosX = -5;
        public const double camPosY = 0;
        public const double camPosZ = 0;

        // Camera defaults
        public const double objPosX = 0;
        public const double objPosY = 0;
        public const double objPosZ = 0;

        public const double camFrusX = 1;
        public const double camFrusY = 0;
        public const double camFrusZ = 0;

        public const double fov = 90;

        // Shape defaults
        public static readonly String[] shapeList = new String[] { "Sphere" };
        public const int shapeDefaultIndex = 0;
        public const double radius = 1;

        // Render defaults
        public const double maxDist = 20;
    }
}
