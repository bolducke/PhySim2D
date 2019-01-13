namespace PhySim2D.Tools
{
    class KVector3
    {
        private const double DEFAULT_VALUE = 0;

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public static KVector3 Zero
        {
            get
            {
                return new KVector3(0);
            }
        }

        public static KVector3 One
        {
            get
            {
                return new KVector3(1);
            }
        }

        public KVector3(double f)
        {
            X = f;
            Y = f;
            Z = f;
        }

        public KVector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #region Algebric vector operation
        public static KVector3 Add(KVector3 u, KVector3 v)
        {
            return new KVector3(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static KVector3 Substract(KVector3 u, KVector3 v)
        {
            return new KVector3(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static KVector3 Multiply(double k, KVector3 v)
        {
            return new KVector3(k * v.X, k * v.Y, k * v.Z);
        }

        public static KVector3 Multiply(KVector3 v, double k)
        {
            return new KVector3(k * v.X, k * v.Y, k * v.Z);
        }

        public static double Dot(KVector3 u, KVector3 v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        }

        public static KVector3 Cross(KVector3 u, KVector3 v)
        {
            return new KVector3(u.Y * v.Z - u.Z * v.Y, u.Z * v.X - u.X * v.Z, u.X * v.Y - u.Y * v.X);
        }
        #endregion
    }
}
