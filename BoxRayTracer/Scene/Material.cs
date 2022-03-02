namespace Scene
{
    public struct Material
    {
        public Color ambient { get; private set; }
        public Color diffuse { get; private set; }
        public Color specular { get; private set; }
        public double reflectivity { get; private set; }

        public Material(Color ambient, Color diffuse, Color specular, double reflectivity)
        {
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.reflectivity = reflectivity;
        }
    }
}
