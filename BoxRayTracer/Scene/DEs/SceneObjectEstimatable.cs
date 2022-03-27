
namespace Scene
{
    public abstract class SceneObjectEstimatable
    {
        /// <summary>
        /// The object's position in the Scene
        /// </summary>
        public Vector position { get; protected set; }
        
        /// <summary>
        /// The object's material
        /// </summary>
        public Material material { get; protected set; }
        
        /// <summary>
        /// Minimum evaluation distance, for use by the ray marcher
        /// </summary>
        public double minDist { get; protected set; }

        /// <summary>
        /// Estimates the minimum distance to the object
        /// </summary>
        /// <param name="rayOrigin">The point from which to estimate distance to the object</param>
        /// <returns>Minimum distance, as double</returns>
        public abstract double DE(Vector rayOrigin);
        
        /// <summary>
        /// The Normal vector at the given fragment position
        /// </summary>
        /// <param name="surfacePos">The fragment position at which to calculate the Normal</param>
        /// <param name="incidence">The vector incident with the fragment, "coming toward the object"</param>
        /// <returns>Vector, normalized</returns>
        public Vector Normal(Vector surfacePos, Vector incidence)
        {
            Vector startSample = surfacePos - 2 * Utilities.eps * incidence.Unit();
            Vector normal = new Vector(this.DE(startSample + Utilities.eps * Vector.unitX) - this.DE(startSample - Utilities.eps * Vector.unitX), this.DE(startSample + Utilities.eps * Vector.unitY) - this.DE(startSample - Utilities.eps * Vector.unitY), this.DE(startSample + Utilities.eps * Vector.unitZ) - this.DE(startSample - Utilities.eps * Vector.unitZ));
            return normal.Unit();
        }
    }
}
