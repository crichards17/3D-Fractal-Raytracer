namespace Scene
{
    public class PointLight : SceneLight
    {
        private Vector position { get; set; }

        public PointLight(Color color, double iDiffuse, double iSpecular, Vector position)
        {
            this.color = color;
            this.iDiffuse = iDiffuse;
            this.iSpecular = iSpecular;
            this.iAmbient = 0;

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

        protected override Vector GetVToLight(Vector objPos)
        {
            return (position - objPos);
        }
    }
}
