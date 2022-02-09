namespace Scene
{
    public class Camera
    {
        #region private fields
        // Camera position
        private Vector pos;
        // Camera angle
        private Vector angle;
        // Frustrum length
        private double frus;
        // Viewport height
        private double vHeight;
        // Viewport width
        private double vWidth;
        #endregion

        public Camera(Vector pos, Vector angle, double frus, double vHeight, double vWidth)
        {
            this.pos = pos;
            this.angle = angle;
            this.frus = frus;
            this.vHeight = vHeight;
            this.vWidth = vWidth;
        }
        
        public Vector RayForPixel(uint x, uint y)
        {
            //TODO: Math
            return new Vector(0, 0, 0);
        }

        #region API
        /// <summary>
        /// Rotates the camera to point at the focus
        /// </summary>
        /// <param name="focus"></param>
        public void LookAt(Vector focus)
        {
            // TODO: Implement
        }

        /// <summary>
        /// Pan motion
        /// </summary>
        /// <param name="newPos"></param>
        public void MoveTo(Vector newPos)
        {
            this.pos = newPos;
        }

        /// <summary>
        /// Moves the camera to a new point, maintaining focus on the current focus 
        /// </summary>
        /// <param name="newPos"></param>
        public void Orbit(Vector newPos, Vector focus)
        {
            MoveTo(newPos);
            LookAt(focus);
        }
        #endregion

    }
}