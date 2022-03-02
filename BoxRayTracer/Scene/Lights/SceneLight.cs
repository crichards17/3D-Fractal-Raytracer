namespace Scene
{
    public abstract class SceneLight
    {
        public Color color { get; protected set; }
        public double iAmbient { get; protected set; }
        public double iDiffuse { get; protected set; }
        public double iSpecular { get; protected set; }

        /// <summary>
        /// Gets the Vector from the given position to the light
        /// </summary>
        /// <param name="objPos"></param>
        /// <returns>Vector, normalized; Vector.origin if no valid path</returns>
        public Vector VToLight(Vector objPos)
        {
            return GetVToLight(objPos).Unit();
        }

        /// <summary>
        /// Calculates the vector from the given position to the light position
        /// </summary>
        /// <param name="objPos"></param>
        /// <returns>Vector; Vector.origin if no valid path</returns>
        protected abstract Vector GetVToLight(Vector objPos);
    }
}
