namespace Scene
{
    public class GlobalDiffuseLight : SceneLight
    {
        private Vector direction { get; set; }

        public GlobalDiffuseLight(Color color, double iDiffuse, Vector direction)
        {
            this.color = color;
            this.iAmbient = 0;
            this.iDiffuse = iDiffuse;
            this.iSpecular = 0;

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

        protected override Vector GetVToLight(Vector objPos)
        {
            return -direction;
        }
    }
}
