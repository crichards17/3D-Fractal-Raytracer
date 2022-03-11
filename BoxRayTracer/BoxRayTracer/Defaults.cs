//#define Boxes
#define Mixed
//#define AmbientOnly

using Scene;
using Color = Scene.Color;

namespace BoxRayTracer
{
    internal class Defaults
    {
        // Camera defaults
        public const double camPosX = 2;
        public const double camPosY = 2;
        public const double camPosZ = 4;

        public const double camFrusX = -1;
        public const double camFrusY = -1;
        public const double camFrusZ = -2;

        public const double fov = 70;

        // Object defaults
        public const double objPosX = 0;
        public const double objPosY = 0;
        public const double objPosZ = 0;

        // Shape defaults
        public static readonly string[] shapeList = new string[] { "Sphere" };
        public const int shapeDefaultIndex = 0;
        public const double radius = 1;

        // Color defaults
        public static readonly string objColor = "White";
        public static readonly string backColor = "Black";

        // Render defaults
        public const double maxDist = 20;
        public const int maxMarch = 300;
        public const double imgWidth = 1024;
        public const double imgHeight = 1024;


        // Light defaults
        private static readonly AmbientLight globalAmbient1 = new AmbientLight(new Color(1.0,1.0,1.0), 0.1);

        // "Sun"
        private static readonly GlobalDiffuseLight globalDiffuseLight1 = new GlobalDiffuseLight(new Color(0.98, 0.84, 0.01), 0.5, new Vector(1, -2, -1));

        // Blue point light
        private static readonly PointLight pointLight1 = new PointLight(new Color(0.01, 0.84, 0.98), 0.5, 0.8, new Vector(-5, 7, -5));

        // Red point light
        private static readonly PointLight pointLight2 = new PointLight(new Color(0.98, 0.01, 0.01), 0.5, 0.7, new Vector(5, 10, -5));

#if AmbientOnly
        public static readonly SceneLight[] sceneLights = new SceneLight[] {globalAmbient1};
#else
        public static readonly SceneLight[] sceneLights = new SceneLight[] {globalAmbient1, globalDiffuseLight1, pointLight1, pointLight2};
#endif

        // Object defaults
        public static readonly Sphere sphere1 = new Sphere(Vector.origin, new Color(1.0, 1.0, 1.0), 1.0);
        public static readonly Sphere sphere2 = new Sphere(new Vector(3, 0, -2), new Color(1.0, 1.0, 1.0), 1.0);
        public static readonly Sphere sphere3 = new Sphere(new Vector(0, 1.25, 0), new Color(1.0, 1.0, 1.0), 0.5);

        public static readonly Box box1 = new Box(new Vector(0, 0.5, 0), new Vector(2, 0.75, 2), new Color(1.0, 1.0, 1.0));

        public static readonly Box box2 = new Box(new Vector(0, -0.5, 0), new Vector(3, 1, 3), new Color(1.0, 1.0, 1.0));

#if Boxes
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { box1 };
#elif Mixed
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { box1, sphere3 };
#else
        // Use spheres:
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { sphere1, sphere2 };
#endif


    }
}
