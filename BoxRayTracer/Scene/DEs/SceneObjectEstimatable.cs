
namespace Scene
{
    public abstract class SceneObjectEstimatable
    {
        /// <summary>
        /// The object's position in the Scene
        /// </summary>
        public Vector position { get; protected set; }
        
        /// <summary>
        /// The object's material Color
        /// </summary>
        public Color color { get; protected set; }
        
        /// <summary>
        /// Estimates the minimum distance to the object
        /// </summary>
        /// <param name="position">The point from which to estimate distance to the object</param>
        /// <returns>Minimum distance, as double</returns>
        public abstract double DE(Vector position);
        
        /// <summary>
        /// The Normal vector at the given fragment position
        /// </summary>
        /// <param name="surfacePos">The fragment position at which to calculate the Normal</param>
        /// <returns>Vector, normalized</returns>
        public Vector Normal(Vector surfacePos)
        {
            return GetNormal(surfacePos).Unit();
        }

        protected abstract Vector GetNormal(Vector sufracePos);
    }
}
