namespace Scene
{
    public struct Material
    {
        public Color ambient { get; set; }
        public Color diffuse { get; set; }
        public Color specular { get; set; }

        public Material(Color ambient, Color diffuse, Color specular)
        {
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
        }
    }
}
