namespace Scene
{
    public class GlobalDiffuseLight : ISceneLight
    {
        public Color color { get; private set; }
        public double iAmbient { get; private set; }
        public double iDiffuse { get; private set; }    
        public double iSpecular { get; private set; }
        private Vector direction { get; set; }

        public GlobalDiffuseLight(Color color, double iDiffuse, Vector direction)
        {
            this.color = color;
            iAmbient = 0;
            this.iDiffuse = iDiffuse;
            iSpecular = 0;

            this.direction = direction.Unit();
        }
        public GlobalDiffuseLight(Color color, double iDiffuse, double iAmbient, Vector direction)
        {
            this.color = color;
            this.iAmbient = iAmbient;
            this.iDiffuse = iDiffuse;
            iSpecular = 0;

            this.direction = direction.Unit();
        }

        public Vector VToLight(Vector objPos)
        {
            return -direction;
        }
    }
}
