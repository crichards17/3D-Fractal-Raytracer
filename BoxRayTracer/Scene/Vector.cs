using System;

namespace Scene
{
    public readonly struct Vector : IEquatable<Vector>
    {
        public readonly double x;
        public readonly double y;
        public readonly double z;

        public static readonly Vector origin = new Vector(0, 0, 0);
        public static readonly Vector unitX = new Vector(1, 0, 0);
        public static readonly Vector unitY = new Vector(0, 1, 0);
        public static readonly Vector unitZ = new Vector(0, 0, 1);

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
        ///     and param Vector other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector Cross(Vector other)
        {
            double cX = this.y * other.z - this.z * other.y;
            double cY = this.z * other.x - this.x * other.z;
            double cZ = this.x * other.y - this.y * other.x;
            return new Vector(cX, cY, cZ);
        }

        /// <summary>
        /// Returns the dot product of the instance Vector
        ///     and the param Vector other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double Dot(Vector other)
        {
            return (this.x * other.x + this.y * other.y + this.z * other.z);
        }

        /// <summary>
        /// Returns the projection of the instance Vector
        ///     onto the param Vector other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector Proj(Vector other)
        {
            return this.Dot(other) / (Math.Pow(other.Length(),2)) * other;
        }

        /// <summary>
        /// Returns the reflection of the instance Vector
        ///     from the plane defined by the Vector other.
        /// </summary>
        /// <param name="normal"></param>
        /// <returns></returns>
        public Vector ReflectAbout(Vector normal)
        {
            if (normal.Length() == 0)
            {
                throw new ArgumentException("Reflection normal must be non-zero");
            }
                return this - 2 * this.Dot(normal) / Math.Pow(normal.Length(), 2) * normal;
        }

        /// <summary>
        /// Returns the angle, in radians, between the instance Vector
        ///     and the param Vector other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double GetTheta(Vector other)
        {
            return this.Dot(other) / (this.Length() * other.Length());
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
        /// Returns a Vector whose components are the absolute values of this Vector's components
        /// </summary>
        /// <returns></returns>
        public Vector Abs()
        {
            return new Vector(Math.Abs(this.x), Math.Abs(this.y), Math.Abs(this.z));
        }

        /// <summary>
        /// Returns true if the instance Vector is parallel to
        ///     the param Vector other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsParallel(Vector other)
        {
            return this.Unit().IsEqualApprox(other.Unit());
        }

        /// <summary>
        /// Returns true if the instance Vector is otrhogonal to
        ///     the param Vector other
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsOrtho(Vector other)
        {
            return Utilities.IsEqualApprox(this.Dot(other), 0);
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

        /// <summary>
        /// Returns true if all components of the other Vector are within
        ///     the Utilities.eps value of the respective component of this Vector
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsEqualApprox(Vector other)
        {
            return (Utilities.IsEqualApprox(this.x, other.x) && Utilities.IsEqualApprox(this.y, other.y) && Utilities.IsEqualApprox(this.z, other.z));
        }

        #endregion

        #region operators
        public static Vector operator -(Vector a) => new Vector(-a.x, -a.y, -a.z);
        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector operator -(Vector a, Vector b) => a + (-b);
        public static Vector operator *(Vector v, double s) => new Vector(v.x * s, v.y * s, v.z * s);
        public static Vector operator *(double s, Vector v) => v * s;
        #endregion
    }
}
