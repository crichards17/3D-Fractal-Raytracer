namespace Scene
{
    public class Camera
    {
        #region private fields
        // Camera position
        private Vector pos;
        // Frustrum angle
        private Vector frusAng;
        // Camera rotation angle - vector aligns to the top center of the screen
        private Vector topAngle;
        // Frustrum length
        private double frusLen;
        // Viewport width
        private double vWidth;
        // Viewport height
        private double vHeight;
        // Display width in pixels
        private int dWidth;
        // Display height in pixels
        private int dHeight;
        #endregion

        public Camera(Vector pos, Vector frusAng, Vector topAngle, double frusLen, double vWidth, double vHeight, int dWidth, int dHeight)
        {
            this.pos = pos;
            this.frusAng = frusAng;
            this.topAngle = topAngle;
            this.frusLen = frusLen;
            this.vWidth = vWidth;
            this.vHeight = vHeight;
            this.dWidth = dWidth;
            this.dHeight = dHeight;
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