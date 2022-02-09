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

        public double Length()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        #region operators
        public static Vector operator -(Vector a) => new Vector(-a.x, -a.y, -a.z);
        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector operator -(Vector a, Vector b) => a + (-b);
        public static Vector operator *(Vector a, double b) => new Vector(a.x * b, a.y * b, a.z * b);
        #endregion
    }
}
