namespace Scene
{
    public readonly struct Vector : IEquatable<Vector>
    {
        public readonly double x;
        public readonly double y;
        public readonly double z;

        public static readonly Vector origin = new Vector(0, 0, 0);
        public static readonly Vector unitY = new Vector(0, 1, 0);

        public Vector( double x, double y, double z )
        {
            this.x = x;
            this.y = y;
            this.z = z;

        }

        #region API

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
        /// Returns the dot product of the instance Vector
        ///     and the param Vector b
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public double Dot(Vector b)
        {
            return (this.x * b.x + this.y * b.y + this.z + b.z);
        }

        /// <summary>
        /// Returns the projection of the instance Vector
        ///     onto the param Vector b
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Vector Proj(Vector b)
        {
            return this.Dot(b) / (Math.Pow(b.Length(),2)) * b;
        }

        /// <summary>
        /// Returns the unit vector equivalent for this Vector
        /// </summary>
        /// <returns></returns>
        public Vector Unit()
        {
            double len = this.Length();
            return new Vector(this.x / len, this.y / len, this.z / len);
        }

        /// <summary>
        /// Returns true if the two Vectors are parallel
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsParallel(Vector b)
        {
            return this.Unit().IsEqualApprox(b.Unit());
        }
        
        /// <summary>
        /// Returns true if the two Vectors are orthogonal
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsOrtho(Vector b)
        {
            return Utilities.IsEqualApprox(this.Dot(b), 0);
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector vector && this.Equals(vector);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }

        public bool Equals(Vector other)
        {
            return (this.x == other.x && this.y == other.y && this.z == other.z);
        }

        public bool IsEqualApprox(Vector other)
        {
            return (Utilities.IsEqualApprox(this.x, other.x) && Utilities.IsEqualApprox(this.y, other.y) && Utilities.IsEqualApprox(this.z, other.z));
        }

        #endregion

        #region operators
        public static Vector operator -(Vector a) => new Vector(-a.x, -a.y, -a.z);
        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector operator -(Vector a, Vector b) => a + (-b);
        public static Vector operator *(Vector a, double b) => new Vector(a.x * b, a.y * b, a.z * b);
        public static Vector operator *(double b, Vector a) => a * b;
        #endregion
    }
}
