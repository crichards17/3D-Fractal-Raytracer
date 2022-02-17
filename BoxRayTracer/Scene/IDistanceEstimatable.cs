namespace Scene
{
    public interface IDistanceEstimatable
    {
        public double DE(Vector position);

        public Vector Normal(Vector surfacePos);
    }
}
