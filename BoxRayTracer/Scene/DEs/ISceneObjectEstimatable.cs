
namespace Scene
{
    public interface ISceneObjectEstimatable
    {
        public Vector position { get; }
        public Color color { get; }
        public double DE(Vector position);
        public Vector Normal(Vector surfacePos);
    }
}
