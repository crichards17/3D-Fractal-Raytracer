using System;

namespace Scene
{
    public struct Material
    {
        public Color ambientColor { get; private set; }
        public Color diffuseColor { get; private set; }
        public Color specularColor { get; private set; }
        public double reflectivity { get; private set; }

        public Material(Color ambient, Color diffuse, Color specular, double reflectivity)
        {
            this.ambientColor = ambient;
            this.diffuseColor = diffuse;
            this.specularColor = specular;
            this.reflectivity = Math.Clamp(reflectivity, 1, 64);
        }

        public Material (Color color, double reflectivity)
        {
            this.ambientColor = this.diffuseColor = this.specularColor = color;
            this.reflectivity = Math.Clamp(reflectivity, 1, 64);
        }

        public Material (Color color)
        {
            this.ambientColor = this.diffuseColor = this.specularColor = color;
            this.reflectivity = 32;
        }
    }
}
