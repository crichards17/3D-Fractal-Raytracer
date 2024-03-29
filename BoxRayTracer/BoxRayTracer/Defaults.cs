﻿//#define Boxes
//#define Mixed
//#define Mandlebox
#define Mandlebulb
//#define AmbientOnly
//#define Reflections

using Scene;
using Color = Scene.Color;

namespace BoxRayTracer
{
    internal class Defaults
    {
        // Camera defaults
        public static Vector camPos = new Vector(-2, 0.5, 2);

        public static Vector camFrus = new Vector(1, -0.2, -1);

        public const double fov = 50;


        // Render defaults
        public const double maxDist = 20;
        public const int maxMarch = 300;
        public const int ambientMarchCutoff = 100;
        public const double imgWidth = 1024;
        public const double imgHeight = 1024;

        // Reflections
#if Reflections
        public const int maxReflections = 8;
#else
        public const int maxReflections = 0;
#endif

        // Ambient
        private static readonly AmbientLight globalAmbient1 = new AmbientLight(new Color(0.95, 1.0, 1.0), 1.0);
        public static readonly Color globalAmbientColor = new Color(0.95, 1.0, 1.0);
        public static readonly double globalAmbientIntensity = 1.0;

        // "Sun"
        private static readonly GlobalDiffuseLight globalDiffuseLight1 = new GlobalDiffuseLight(new Color(0.98, 0.84, 0.01), 1.0, new Vector(1, -2, -1));

        // Blue point light
        private static readonly PointLight pointLight1 = new PointLight(new Color(0.01, 0.84, 0.98), 0.5, 0.8, new Vector(-5, 7, -5));

        // Red point light
        private static readonly PointLight pointLight2 = new PointLight(Color.Red, 0.5, 0.7, new Vector(5, 10, -5));

#if AmbientOnly
        public static readonly SceneLight[] sceneLights = new SceneLight[] {globalAmbient1};
#else
        public static readonly SceneLight[] sceneLights = new SceneLight[] {globalAmbient1, globalDiffuseLight1, pointLight1, pointLight2};
#endif

        // Object defaults
        public static readonly Sphere sphere1 = new Sphere(Vector.origin, new Material(new Color(1.0, 1.0, 1.0)), 1.0);
        public static readonly Sphere sphere2 = new Sphere(new Vector(1, 1.75, -1), new Material(new Color(1.0, 1.0, 1.0)), 0.75);
        public static readonly Sphere sphere3 = new Sphere(new Vector(0, 1.25, 0), new Material(new Color(1.0, 1.0, 1.0)), 0.5);

        public static readonly Box box1 = new Box(new Vector(0, 0.5, 0), new Vector(2, 0.25, 2), new Material(new Color(1.0, 1.0, 1.0)));
        public static readonly Box box2 = new Box(new Vector(0, -0.5, 0), new Vector(3, 1, 3), new Material(new Color(1.0, 1.0, 1.0)));

        public static readonly Mandlebox mandleBox1 = new Mandlebox(Vector.origin, new Material(Color.White));

        public static readonly Mandlebulb mandleBulb1 = new Mandlebulb(Vector.origin, new Material(Color.White));

#if Boxes
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { box1 };
#elif Mixed
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { box1, sphere2, sphere3 };
#elif Mandlebox
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { mandleBox1 };
#elif Mandlebulb
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { mandleBulb1 };
#else
        // Use spheres:
        public static readonly SceneObjectEstimatable[] sceneObjects = new SceneObjectEstimatable[] { sphere1, sphere2 };
#endif


    }
}
