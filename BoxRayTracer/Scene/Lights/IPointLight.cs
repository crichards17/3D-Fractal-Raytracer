
namespace Scene
{
    public interface IPointLight : ISceneLight
    {
        public Vector Position { get; }

        public bool IsIlluminating(Vector pos);
    }
}
