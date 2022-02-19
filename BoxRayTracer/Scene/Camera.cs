using System;
using System.Diagnostics;

namespace Scene
{
    public class Camera
    {
        #region private fields
        // Camera position
        public Vector camPos { get; private set; }
        
        // Frustrum angle (unit vector)
        public Vector vFrus { get; private set; }
        // Camera rotation angle (unit vector)
        public Vector vRoll { get; private set; }
        // Stores the orthogonal vector to the vFrus and vRoll (unit vector)
        public Vector viewOrtho { get; private set; }
        
        // Frustrom length
        private double frusLen;
        
        // Viewport width
        private readonly double vWidth;
        // Viewport height
        private readonly double vHeight;
        
        // Display width in pixels
        private readonly int dWidth;
        // Display height in pixels
        private readonly int dHeight;
        #endregion

        #region constructors

        /// <summary>
        /// Constructor which takes a Camera position and focal point,
        ///     and calculates vFrus and vRoll
        /// </summary>
        /// <param name="camPos"></param>
        /// <param name="lookAt"></param>
        /// <param name="fov"></param>
        /// <param name="vWidth"></param>
        /// <param name="vHeight"></param>
        /// <param name="dWidth"></param>
        /// <param name="dHeight"></param>
        public Camera(Vector camPos, Vector lookAt, double fov, double vWidth, double vHeight, int dWidth, int dHeight)
        {
            this.camPos = camPos;
            this.vWidth = vWidth;
            this.vHeight = vHeight;
            this.dWidth = dWidth;
            this.dHeight = dHeight;

            // Set the frustrum length for the given FOV
            this.frusLen = vWidth / (2 * Math.Tan(Utilities.DegreesToRadians(fov / 2)));

            // Point to the lookAt vector, with the rollTarget set to the "up" y unit vector
            LookAt(lookAt, Vector.unitY);

            AssertIsConsistent();
        }

        #endregion

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
            Vector topL = camPos + vFrus * frusLen + vRoll * (vHeight / 2) - viewOrtho * (vWidth / 2);

            // Compute the given pixel's percentage location in the viewing plane,
            //  relative to the top-left corner
            double pixelRelPctX = (double) x / dWidth;
            double pixelRelPctY = (double) y / dHeight;

            // Compute the absolute location of the pixel using its relative position
            pixelPos = topL + viewOrtho * (pixelRelPctX * vWidth) - vRoll * (pixelRelPctY * vHeight);

            // Compute the unit vector from the camera to the pixel position
            rayAng = (pixelPos - camPos).Unit();

            AssertIsConsistent();
        }

        /// <summary>
        /// Rotates the camera to point at the focus, and sets the vRoll to
        ///     maximize the component along the given rollTarget Vector.
        /// </summary>
        /// <param name="focus"></param>
        public void LookAt(Vector focus, Vector rollTarget)
        {
            // Find the frustrum Unit vector
            this.vFrus = (focus - camPos).Unit();

            // Set the roll vector closest to param roll vector
            // Equation: <roll> + <Proj(<rollTarget> onto <frus>) = <rollTarget>
            // => <roll> = <rollTarget> - <Proj(<rollTarget> onto <frus>)
            this.vRoll = (rollTarget - rollTarget.Proj(vFrus)).Unit();

            viewOrtho = vFrus.Cross(vRoll).Unit();

            AssertIsConsistent();
        }

        /// <summary>
        /// Pan motion
        /// </summary>
        /// <param name="newPos"></param>
        public void MoveTo(Vector newPos)
        {
            this.camPos = newPos;

            AssertIsConsistent();
        }

        #endregion

        #region Helpers

        [Conditional("Debug")]
        private void AssertIsConsistent()
        {
            Debug.Assert(Utilities.IsEqualApprox(vFrus.Length(), 1));
            Debug.Assert(Utilities.IsEqualApprox(vRoll.Length(), 1));
            Debug.Assert(Utilities.IsEqualApprox(viewOrtho.Length(), 1));
            Debug.Assert(vFrus.IsOrtho(vRoll));
            Debug.Assert(vFrus.IsOrtho(viewOrtho));
            Debug.Assert(vRoll.IsOrtho(viewOrtho));
        }
        #endregion

    }
}