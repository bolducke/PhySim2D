using PhySim2D.Sim;
using System;
using System.Runtime.Serialization;

namespace PhySim2D.Tools
{
    [DataContract]
    class KMatrix3x3Opti
    {

        public bool IsIdentity
        {
            get
            {
                return KMath.AlmostEquals(A11,1,Config.EpsilonsFloat) &&
                    KMath.AlmostEquals(A12, 0, Config.EpsilonsFloat) &&
                    KMath.AlmostEquals(A13, 0, Config.EpsilonsFloat) &&
                    KMath.AlmostEquals(A21, 0, Config.EpsilonsFloat) &&
                    KMath.AlmostEquals(A22, 1, Config.EpsilonsFloat) &&
                    KMath.AlmostEquals(A23, 0, Config.EpsilonsFloat);
            }
        }
        [DataMember]
        public double A11 { get; set; }

        [DataMember]
        public double A12 { get; set; }

        [DataMember]
        public double A13 { get; set; }

        [DataMember]
        public double A21 { get; set; }

        [DataMember]
        public double A22 { get; set; }

        [DataMember]
        public double A23 { get; set; }

        public KMatrix3x3Opti()
        {
            A11 = 0;
            A12 = 0;
            A21 = 0;
            A22 = 0;
            A13 = 0;
            A23 = 0;
        }

        public KMatrix3x3Opti(double a11, double a12, double a21, double a22, double a31, double a32)
        {
            A11 = a11;
            A12 = a12;
            A21 = a21;
            A22 = a22;
            A13 = a31;
            A23 = a32;
        }

        public static KMatrix3x3Opti CreateScale(KVector2 scale)
        {
            return new KMatrix3x3Opti(scale.X, 0, 0, scale.Y,0,0);
        }

        public static KMatrix3x3Opti CreateTranslation(double x, double y)
        {
            return new KMatrix3x3Opti(1, 0, 0, 1, x, y);
        }

        public static KMatrix3x3Opti CreateTranslation(KVector2 translation)
        {
            return CreateTranslation(translation.X, translation.Y);
        }

        public static KMatrix3x3Opti CreateRotation(double radians)
        {
            return new KMatrix3x3Opti(Math.Cos(radians), Math.Sin(radians), -Math.Sin(radians), Math.Cos(radians), 0, 0);
        }

        #region Algebric vector operation
        public static KMatrix3x3Opti Add(KMatrix3x3Opti A, KMatrix3x3Opti B)
        {
            return new KMatrix3x3Opti(
                A.A11 + B.A11,
                A.A12 + B.A12,
                A.A21 + B.A21,
                A.A22 + B.A22,
                A.A13 + B.A13,
                A.A23 + B.A23);
        }

        public static KMatrix3x3Opti Substract(KMatrix3x3Opti A, KMatrix3x3Opti B)
        {
            return new KMatrix3x3Opti(
                    A.A11 - B.A11,
                    A.A12 - B.A12,
                    A.A21 - B.A21,
                    A.A22 - B.A22,
                    A.A13 - B.A13,
                    A.A23 - B.A23);
        }

        public static KMatrix3x3Opti Multiply(double k, KMatrix3x3Opti A)
        {
            return new KMatrix3x3Opti(
                k * A.A11,
                k * A.A12,
                k * A.A21,
                k * A.A22,
                k * A.A13,
                k * A.A23);
        }

        public static KMatrix3x3Opti Multiply(KMatrix3x3Opti A, double k)
        {
            return new KMatrix3x3Opti(
                k * A.A11,
                k * A.A12,
                k * A.A21,
                k * A.A22,
                k * A.A13,
                k * A.A23);
        }

        public static KMatrix3x3Opti Multiply(KMatrix3x3Opti A, KMatrix3x3Opti B)
        {
            double c11 = A.A11 * B.A11 + A.A12 * B.A21;
            double c12 = A.A11 * B.A12 + A.A12 * B.A22;
            double c13 = A.A11 * B.A13 + A.A12 * B.A23 + A.A13;

            double c21 = A.A21 * B.A11 + A.A22 * B.A21;
            double c22 = A.A21 * B.A12 + A.A22 * B.A22;
            double c23 = A.A21 * B.A13 + A.A22 * B.A23 + A.A23;

            return new KMatrix3x3Opti(c11,c12,c21,c22,c13,c23);
        }

        public static KVector2 Multiply(KVector2 A, KMatrix3x3Opti B)
        {
            double X = A.X * B.A11 + A.Y * B.A21;
            double Y = A.X * B.A12 + A.Y * B.A22;

            return new KVector2(X, Y);
        }

        public static KVector2 Multiply(KMatrix3x3Opti A, KVector2 B)
        {
            double X = A.A11 * B.X + A.A12 * B.Y;
            double Y = A.A21 * B.X + A.A22 * B.Y;

            return new KVector2(X, Y);
        }

        public static KVector3 Multiply(KVector3 A, KMatrix3x3Opti B)
        {
            double X = A.X * B.A11 + A.Y * B.A21;
            double Y = A.X * B.A12 + A.Y * B.A22;
            double Z = A.X * B.A13 + A.Y * B.A23 + A.Z;

            return new KVector3(X,Y,Z);
        }

        public static KVector3 Multiply(KMatrix3x3Opti A, KVector3 B)
        {
            double X = A.A11 * B.X + A.A12 * B.Y + A.A13 * B.Z;
            double Y = A.A21 * B.X + A.A22 * B.Y + A.A23 * B.Z;
            double Z = B.Z;

            return new KVector3(X, Y, Z);
        }

        #endregion

        public static KMatrix3x3Opti operator +(KMatrix3x3Opti A, KMatrix3x3Opti B)
        {
            return Add(A, B);
        }

        public static KMatrix3x3Opti operator -(KMatrix3x3Opti A, KMatrix3x3Opti B)
        {
            return Substract(A, B);
        }

        public static KMatrix3x3Opti operator -(KMatrix3x3Opti A)
        {
            return Multiply(-1, A);
        }

        public static KMatrix3x3Opti operator *(double k, KMatrix3x3Opti A)
        {
            return Multiply(k, A);
        }

        public static KMatrix3x3Opti operator *(KMatrix3x3Opti A, double k)
        {
            return Multiply(A, k);
        }

        public static KMatrix3x3Opti operator *(KMatrix3x3Opti A, KMatrix3x3Opti B)
        {
            return Multiply(A, B);
        }

        public static KVector3 operator *(KMatrix3x3Opti A, KVector3 B)
        {
            return Multiply(A, B);
        }

        public static KVector3 operator *(KVector3 A, KMatrix3x3Opti B)
        {
            return Multiply(A, B);
        }

        public static KVector2 operator *(KMatrix3x3Opti A, KVector2 B)
        {
            return Multiply(A, B);
        }

        public static KVector2 operator *(KVector2 A, KMatrix3x3Opti B)
        {
            return Multiply(A, B);
        }
    }
}
