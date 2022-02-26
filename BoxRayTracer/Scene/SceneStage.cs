namespace Scene
{
    public class SceneStage
    {
        public ISceneObjectEstimatable[] sceneObjects { get; private set; }
        public ISceneLight[] sceneLights { get; private set; }
        public Scene.Color backColor { get; set; }

        public SceneStage(ISceneObjectEstimatable[] sceneObjects, ISceneLight[] sceneLights, Color backColor)
        {
            this.sceneObjects = sceneObjects;
            this.sceneLights = sceneLights;
            this.backColor = backColor;
        }
        /// <summary>
        /// Adds newObj to the sceneObjects array.
        /// </summary>
        /// <param name="newObj">The scene object to be added</param>
        public void AddObject(ISceneObjectEstimatable newObj)
        {
            sceneObjects = AddToExactArray<ISceneObjectEstimatable>(sceneObjects, newObj);
            // Maintains an exact-length array for memory access performance.
            //  Object additions will be significantly less frequent than access requests to this array (full iteration per ray march)
        }

        /// <summary>
        /// Removes the object from the given index in the sceneObjects array.
        /// </summary>
        /// <param name="index">Index in the sceneObjects array of the object to be removed</param>
        public void RemoveObject(int removeIdx)
        {
            sceneObjects = RemoveFromExactArray<ISceneObjectEstimatable>(sceneObjects, removeIdx);
            // Maintains an exact-length array for memory access performance.
            //  Object removal will be significantly less frequent than access requests to this array (full iteration per ray march)
        }

        /// <summary>
        /// Adds newLight to the sceneLights array.
        /// </summary>
        /// <param name="newObj">The scene Light to be added</param>
        public void AddLight(ISceneLight newLight)
        {
            sceneLights = AddToExactArray<ISceneLight>(sceneLights, newLight);
            // Maintains an exact-length array for memory access performance.
            //  Object additions will be significantly less frequent than access requests to this array (full iteration per ray march)
        }

        /// <summary>
        /// Removes the light from the given index in the sceneLights array.
        /// </summary>
        /// <param name="index">Index in the sceneLights array of the light to be removed</param>
        public void RemoveLight(int removeIdx)
        {
            sceneLights = RemoveFromExactArray<ISceneLight>(sceneLights, removeIdx);
            // Maintains an exact-length array for memory access performance.
            //  Object removal will be significantly less frequent than access requests to this array (full iteration per ray march)
        }

        private static T[] AddToExactArray<T>(T[] sceneArr, T newItem)
        {
            T[] newArr = new T[sceneArr.Length + 1];
            for (int i = 0; i < sceneArr.Length; i++)
            {
                newArr[i] = sceneArr[i];
            }
            newArr[sceneArr.Length] = newItem;
            return newArr;
        }

        private static T[] RemoveFromExactArray<T>(T[] sceneArr, int removeIdx)
        {
            T[] newArr = new T[sceneArr.Length - 1];
            int insertIdx = 0;
            for (int i = 0; i < sceneArr.Length; i++)
            {
                if (i != removeIdx)
                {
                    newArr[insertIdx] = sceneArr[i];
                    insertIdx++;
                }
            }
            return newArr;
        }

    }
}
