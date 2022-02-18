
namespace Scene
{
    public struct Color
    {
        private readonly Vector v;

        public Color(double r, double g, double b)
        {
            this.v = new Vector(r,g,b);
        }

        private Color(Vector v)
        {
            this.v = v;
        }

        public double R { get { return v.x; } }
        public double G { get { return v.y; } }
        public double B { get { return v.z; } }

        public static Color operator +(Color a, Color b) => new Color(a.v + b.v);
        public static Color operator *(Color c, double s) => new Color(c.v * s);
        public static Color operator *(double s, Color c) => c * s;
        public static Color operator *(Color a, Color b) => new Color(a.v.x * b.v.x, a.v.y * b.v.y, a.v.z * b.v.z);
    }
}
