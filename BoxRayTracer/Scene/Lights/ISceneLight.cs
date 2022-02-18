namespace Scene
{
    public interface ISceneLight
    {
        public Color color { get; }
        public double iAmbient { get; }
        public double iDiffuse { get; }
        public double iSpecular { get; }

        public Vector VToLight(Vector objPos);
    }
}
