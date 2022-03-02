//#define Box
#define Mixed
//#define AmbientOnly

using Scene;
using Color = Scene.Color;

namespace BoxRayTracer
{
    internal class Defaults
    {
        // Camera defaults
        public const double camPosX = -2;
        public const double camPosY = -2;
        public const double camPosZ = 3;

        // Object defaults
        public const double objPosX = 0;
        public const double objPosY = 0;
        public const double objPosZ = 0;

        public const double camFrusX = 1;
        public const double camFrusY = 1;
        public const double camFrusZ = -2;

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

        private static readonly GlobalDiffuseLight globalDiffuseLight1 = new GlobalDiffuseLight(new Color(1.0, 0.2, 1.0), 0.5, new Vector(1, -1, -1));
        // Pink diffuse facing (1,0,0)

        private static readonly PointLight pointLight1 = new PointLight(new Color(0.2, 1.0, 1.0), 0.5, 0.5, new Vector(10, 10, 10));
        // Light blue point light at (5, 5, 5)

#if AmbientOnly
        public static readonly SceneLight[] sceneLights = new SceneLight[] {globalAmbient1};
#else
        public static readonly SceneLight[] sceneLights = new SceneLight[] {globalAmbient1, globalDiffuseLight1, pointLight1};
#endif

        // Object defaults
        public static readonly Sphere sphere1 = new Sphere(Vector.origin, new Color(1.0, 1.0, 1.0), 1.0);
        public static readonly Sphere sphere2 = new Sphere(new Vector(3, 0, -2), new Color(1.0, 1.0, 1.0), 1.0);
        public static readonly Sphere sphere3 = new Sphere(new Vector(-1, 1, 1), new Color(1.0, 1.0, 1.0), 0.25);

        public static readonly Box box1 = new Box(Vector.origin, new Vector(0.5, 0.5, 0.5), new Color(1.0, 1.0, 1.0));

#if Box
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { box1 };
#elif Mixed
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { box1, sphere3 };
#else
        // Use spheres:
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { sphere1, sphere2 };
#endif


    }
}
