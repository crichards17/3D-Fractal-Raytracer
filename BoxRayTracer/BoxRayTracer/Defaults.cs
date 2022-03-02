using Scene;
using Color = Scene.Color;

namespace BoxRayTracer
{
    internal class Defaults
    {
        // Object defaults
        public const double camPosX = -2;
        public const double camPosY = 0;
        public const double camPosZ = 4;

        // Camera defaults
        public const double objPosX = 0;
        public const double objPosY = 0;
        public const double objPosZ = 0;

        public const double camFrusX = 0;
        public const double camFrusY = 0;
        public const double camFrusZ = -1;

        public const double fov = 90;

        // Shape defaults
        public static readonly string[] shapeList = new string[] { "Sphere" };
        public const int shapeDefaultIndex = 0;
        public const double radius = 1;

        // Color defaults
        public static readonly string objColor = "White";
        public static readonly string backColor = "Black";

        // Render defaults
        public const double maxDist = 20;
        public const double imgWidth = 512;
        public const double imgHeight = 512;


        // Light defaults
        private static readonly AmbientLight globalAmbient1 = new AmbientLight(new Color(1.0,1.0,1.0), 0.1);
        // Global soft white ambient

        private static readonly GlobalDiffuseLight globalDiffuseLight1 = new GlobalDiffuseLight(new Color(1.0, 0.2, 1.0), 0.5, new Vector(1, 0, 0));
        // Pink diffuse facing (1,0,0)

        private static readonly PointLight pointLight1 = new PointLight(new Color(0.2, 1.0, 1.0), 0.5, 0.5, new Vector(10, 10, 10));
        // Light blue point light at (5, 5, 5)

        public static readonly ISceneLight[] sceneLights = new ISceneLight[] {globalAmbient1, globalDiffuseLight1, pointLight1};


        // Object defaults
        public static readonly Sphere sphere1 = new Sphere(Vector.origin, 1.0, new Color(1.0, 1.0, 1.0));
        public static readonly Sphere sphere2 = new Sphere(new Vector(-3, 1, 0), 1.0, new Color(1.0, 1.0, 1.0));
        public static readonly ISceneObjectEstimatable[] sceneObjects = new ISceneObjectEstimatable[] { sphere1, sphere2 };
    }
}
