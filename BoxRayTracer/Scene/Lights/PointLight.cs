namespace Scene
{
    public class PointLight : ISceneLight
    {
        public Color color { get; private set; }
        public double iAmbient { get; private set; }
        public double iDiffuse { get; private set; }
        public double iSpecular { get; private set; }
        private Vector position { get; set; }

        public PointLight(Color color, double iDiffuse, double iSpecular, Vector position)
        {
            this.color = color;
            this.iDiffuse = iDiffuse;
            this.iSpecular = iSpecular;
            iAmbient = 0;

            this.position = position;
        }
        public PointLight(Color color, double iDiffuse, double iSpecular, double iAmbient, Vector position)
        {
            this.color = color;
            this.iDiffuse = iDiffuse;
            this.iSpecular = iSpecular;
            this.iAmbient = iAmbient;

            this.position = position;
        }

        public Vector VToLight(Vector objPos)
        {
            return (position - objPos).Unit();
        }
    }
}
