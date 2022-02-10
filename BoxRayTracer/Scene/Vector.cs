namespace Scene
{
    public readonly struct Vector
    {
        public readonly double x;
        public readonly double y;
        public readonly double z;

        public Vector( double x, double y, double z )
        {
            this.x = x;
            this.y = y;
            this.z = z;

        }

        /// <summary>
        /// Returns the length scalar of this vector
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }
        
        /// <summary>
        /// Returns the cross product between this Vector
        /// and given Vector, "b"
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Vector Cross(Vector b)
        {
            double cX = this.y * b.z - this.z * b.y;
            double cY = this.z * b.x - this.x * b.z;
            double cZ = this.x * b.y - this.y * b.x;
            return new Vector(cX, cY, cZ);
        }

        /// <summary>
        /// Returns the unit vector equivalent for this Vector
        /// </summary>
        /// <returns></returns>
        public Vector Unit()
        {
            return new Vector(this.x / this.Length(), this.y / this.Length(), this.z / this.Length());
        }

        #region operators
        public static Vector operator -(Vector a) => new Vector(-a.x, -a.y, -a.z);
        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector operator -(Vector a, Vector b) => a + (-b);
        public static Vector operator *(Vector a, double b) => new Vector(a.x * b, a.y * b, a.z * b);
        #endregion
    }
}
