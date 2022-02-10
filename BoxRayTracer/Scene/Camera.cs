namespace Scene
{
    public class Camera
    {
        #region private fields
        // Camera position
        private Vector camPos;
        // Frustrum angle (unit vector)
        private Vector vFrus;
        // Camera rotation angle (unit vector)
        private Vector vRoll;
        
        // Frustrom length
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

        public Camera(Vector camPos, Vector vFrus, Vector vRoll, double frusLen, double vWidth, double vHeight, int dWidth, int dHeight)
        {
            this.camPos = camPos;
            this.vFrus = vFrus;
            this.vRoll = vRoll;
            this.frusLen = frusLen;
            this.vWidth = vWidth;
            this.vHeight = vHeight;
            this.dWidth = dWidth;
            this.dHeight = dHeight;
        }

        #region API
        /// <summary>
        /// Takes an x and y value of a pixel in the display grid,
        /// and returns the corresponding pixel position
        /// and ray unit vector as out params
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pixelPos"></param>
        /// <param name="rayAng"></param>
        public void RayForPixel(uint x, uint y, out Vector pixelPos, out Vector rayAng)
        {
            // Find the top-left corner (0,0) of the viewing plane
            Vector topL = camPos + vFrus * frusLen + vRoll * (vHeight / 2) - vFrus.Cross(vRoll).Unit() * (vWidth / 2);

            // Compute the given pixel's percentage location in the viewing plane,
            //  relative to the top-left corner
            double pixelRelPctX = x / dWidth;
            double pixelRelPctY = y / dHeight;

            // Compute the absolute location of the pixel using its relative position
            pixelPos = topL + vFrus.Cross(vRoll).Unit() * (pixelRelPctX * vWidth) - vRoll * (pixelRelPctY * vHeight);

            // Compute the unit vector from the camera to the pixel position
            rayAng = (pixelPos - camPos).Unit();
        }

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
            this.camPos = newPos;
            // TODO: Splined / fly functionality? Currently instantaneous move.
        }

        #endregion

    }
}