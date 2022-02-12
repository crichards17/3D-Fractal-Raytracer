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

        /// <summary>
        /// Constructor which takes a Camera position and focal point,
        ///     and calculates vFrus and vRoll
        /// </summary>
        /// <param name="camPos"></param>
        /// <param name="lookAt"></param>
        /// <param name="frusLen"></param>
        /// <param name="vWidth"></param>
        /// <param name="vHeight"></param>
        /// <param name="dWidth"></param>
        /// <param name="dHeight"></param>
        public Camera(Vector camPos, Vector lookAt, double frusLen, double vWidth, double vHeight, int dWidth, int dHeight)
        {
            this.camPos = camPos;
            this.frusLen = frusLen;
            this.vWidth = vWidth;
            this.vHeight = vHeight;
            this.dWidth = dWidth;
            this.dHeight = dHeight;

            // PointTo the lookAt vector, with the rollTarget set to the "up" y unit vector
            // TODO: is this the correct usage of the out params?
            PointTo(camPos, lookAt, new Vector(0,1,0), out this.vFrus, out this.vRoll);
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
        /// Rotates the camera to point at the focus, and sets the vRoll to "up" (-y) if possible.
        /// </summary>
        /// <param name="focus"></param>
        public void LookAt(Vector focus)
        {
            PointTo(this.camPos, focus, this.vRoll, out this.vFrus, out this.vRoll);
        }

        /// <summary>
        /// Pan motion
        /// </summary>
        /// <param name="newPos"></param>
        public void MoveTo(Vector newPos)
        {
            this.camPos = newPos;
        }

        #endregion

        #region Helpers

        private static void PointTo(Vector pos, Vector focal, Vector rollTarget, out Vector frus, out Vector roll)
        {
            // Find the frustrum Unit vector
            frus = (focal - pos).Unit();

            // Set the roll vector closest to param roll vector
            // Equation: <roll> + <Proj(<rollTarget> onto <frus>) = <rollTarget>
            // => <roll> = <rollTarget> - <Proj(<rollTarget> onto <frus>)
            roll = (rollTarget - rollTarget.Proj(frus)).Unit();

        }

        #endregion

    }
}