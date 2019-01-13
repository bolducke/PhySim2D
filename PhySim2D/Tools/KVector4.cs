namespace PhySim2D.Tools
{
    class KVector4
    {
        private const double DEFAULT_VALUE = 0;

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double W { get; private set; }

        public static KVector4 Zero
        {
            get
            {
                return new KVector4(0);
            }
        }

        public static KVector4 One
        {
            get
            {
                return new KVector4(1);
            }
        }

        public KVector4(double f)
        {
            X = f;
            Y = f;
            Z = f;
            W = f;
        }

        public KVector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        #region Algebric vector operation
        public static KVector4 Add(KVector4 u, KVector4 v)
        {
            return new KVector4(u.X + v.X, u.Y + v.Y, u.Z + v.Z,u.W + v.W);
        }

        public static KVector4 Substract(KVector4 u, KVector4 v)
        {
            return new KVector4(u.X - v.X, u.Y - v.Y, u.Z - v.Z,u.W - v.W);
        }

        public static KVector4 Multiply(double k, KVector4 v)
        {
            return new KVector4(k * v.X, k * v.Y, k * v.Z, k* v.W);
        }

        public static KVector4 Multiply(KVector4 v, double k)
        {
            return new KVector4(k * v.X, k * v.Y, k * v.Z, k * v.W);
        }

        public static double Dot(KVector4 u, KVector4 v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z + u.W * v.W;
        }
        #endregion

        public static KVector4 operator +(KVector4 u, KVector4 v)
        {
            return Add(u, v);
        }

        public static KVector4 operator -(KVector4 u, KVector4 v)
        {
            return Substract(u, v);
        }

        public static KVector4 operator -(KVector4 u)
        {
            return Multiply(-1, u);
        }

        public static double operator *(KVector4 u, KVector4 v)
        {
            return Dot(u, v);
        }

        public static KVector4 operator *(double k, KVector4 v)
        {
            return Multiply(k, v);
        }

        public static KVector4 operator *(KVector4 v, double k)
        {
            return Multiply(v, k);
        }

        public static KVector4 operator /(KVector4 v, double k)
        {
            return Multiply(v, 1 / k);
        }
    }
}
