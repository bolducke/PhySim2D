using PhySim2D.Sim;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PhySim2D.Tools
{
    [DebuggerDisplay("( {X}, {Y} )")]
    internal struct KVector2
    {
        private const double DEFAULT_VALUE = 0;

        public double X { get; private set; }
        public double Y { get; private set; }

        public static KVector2 Zero
        {
            get
            {
                return new KVector2(0);
            }
        }
            
        public static KVector2 One 
        {
            get
            {
                return new KVector2(1);
            }
        }

        public KVector2(double f)
        {
            this.X = f;
            this.Y = f;
        }

        public KVector2(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        #region Algebric vector operation
        public static KVector2 Add(KVector2 u ,KVector2 v)
        {
            return new KVector2(u.X + v.X, u.Y + v.Y);
        }

        public static KVector2 Substract(KVector2 u, KVector2 v)
        {
            return new KVector2(u.X - v.X, u.Y - v.Y);
        }

        public static KVector2 Multiply(double k, KVector2 v)
        {
            return new KVector2(k * v.X, k * v.Y);
        }

        public static KVector2 Multiply(KVector2 v, double k)
        {
            return new KVector2(k * v.X, k * v.Y);
        }

        public static double Dot(KVector2 u, KVector2 v)
        {
            return u.X * v.X + u.Y * v.Y;
        }

        public static KVector2 Cross(KVector2 v, double z)
        {
            return new KVector2(z * v.Y, -(z * v.X));
        }

        public static KVector2 Cross(double z, KVector2 v)
        {
            return new KVector2(-(z * v.Y), z * v.X);
        }

        public static double Cross(KVector2 u, KVector2 v)
        {
            return v.X * u.Y - v.Y * u.X;
        }
        #endregion

        #region Useful method
        public static KVector2 PerpCW(KVector2 v)
        {
            return new KVector2(v.Y, -v.X);
        }

        public static KVector2 PerpCCW(KVector2 v)
        {
            return new KVector2(-v.Y, v.X);
        }

        public static KVector2 Min(KVector2 u, KVector2 v)
        {
            return new KVector2(
                                u.X < v.X ? u.X : v.X,
                                u.Y < v.Y ? u.Y : v.Y
                                );
        }

        public static KVector2 Min(IList<KVector2> vectors)
        {
            KVector2 v = vectors[0];
            for (int i = 1; i < vectors.Count; i++)
            {
                v = KVector2.Min(v, vectors[i]);
            }
            return v;
        }

        public static KVector2 Max(KVector2 u, KVector2 v)
        {
            return new KVector2(
                                u.X < v.X ? v.X : u.X,
                                u.Y < v.Y ? v.Y : u.Y
                                );
        }

        public static KVector2 Max(IList<KVector2> vectors)
        {
            KVector2 v = vectors[0];
            for (int i = 1; i < vectors.Count; i++)
            {
                v = KVector2.Max(v, vectors[i]);
            }
            return v;
        }

        public static void Extremity(IList<KVector2> vectors, out KVector2 min, out KVector2 max)
        {
            min = max = vectors[0];
            for (int i = 1; i < vectors.Count; i++)
            {
                max = Max(max, vectors[i]);
                min = Min(min, vectors[i]);
            }
        }

        public static KVector2 Normalize(KVector2 v)
        {
            return v/v.Length();
        }

        public static KVector2 TransformPoint(KVector2 v, KMatrix3x3Opti mat)
        {
            KVector2 result = new KVector2
            {
                X = mat.A11 * v.X + mat.A12 * v.Y + mat.A13,
                Y = mat.A21 * v.X + mat.A22 * v.Y + mat.A23
            };

            return result;
        }

        public static KVector2 TransformDir(KVector2 v, KMatrix3x3Opti mat)
        {
            KVector2 result = new KVector2
            {
                X = mat.A11 * v.X + mat.A12 * v.Y,
                Y = mat.A21 * v.X + mat.A22 * v.Y
            };

            return result;
        }

        public static KVector2 TransformNormal(KVector2 v, KMatrix3x3Opti mat)
        {
            return Normalize(TransformDir(v,mat));
        }

        public static KVector2 Lerp(KVector2 a, KVector2 b, double alpha)
        {
            return a + (b - a) * alpha;
        }

        #endregion
        public double Length()
        {
            return Math.Sqrt(this * this);
        }

        public double LengthSquared()
        {
            return this * this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "( " + X + " , " + Y + " )";
        }

        public override bool Equals(Object obj)
        {
             if (obj == null)
                return false;

             if (!(obj is KVector2))
                 return false;

                KVector2 other = (KVector2)obj;

             if (!KMath.AlmostEquals(X, other.X, Config.EpsilonsFloat))
                return false;
             if (!KMath.AlmostEquals(Y, other.Y, Config.EpsilonsFloat))
                return false;

            return true;
        }

        #region Operator override

        public static KVector2 operator +(KVector2 u, KVector2 v)
        {
            return KVector2.Add(u, v);
        }

        public static KVector2 operator -(KVector2 u, KVector2 v)
        {
            return KVector2.Substract(u, v);
        }

        public static KVector2 operator -(KVector2 u)
        {
            return KVector2.Multiply(-1,u);
        }

        public static KVector2 operator %(double z, KVector2 v)
        {
            return KVector2.Cross(z, v);
        }

        public static KVector2 operator %(KVector2 v, double z)
        {
            return KVector2.Cross(v, z);
        }

        public static double operator %(KVector2 u, KVector2 v)
        {
            return KVector2.Cross(u, v);
        }

        public static double operator *(KVector2 u, KVector2 v)
        {
            return KVector2.Dot(u,v);
        }

        public static KVector2 operator *(double k, KVector2 v)
        {
            return KVector2.Multiply(k, v);
        }

        public static KVector2 operator *(KVector2 v, double k)
        {
            return KVector2.Multiply(v, k);
        }

        public static KVector2 operator /(KVector2 v, double k)
        {
            return KVector2.Multiply(v, 1/k);
        }

        public static bool operator ==(KVector2 u, KVector2 v)
        {
            return u.Equals(v);
        }

        public static bool operator !=(KVector2 u, KVector2 v)
        {
            return !u.Equals(v);
        }

        #endregion
    }
}
