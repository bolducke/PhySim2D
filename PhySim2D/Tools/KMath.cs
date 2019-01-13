using System;

namespace PhySim2D.Tools
{
    public static class KMath
    {

        public static bool AlmostEquals(double k, double c, double epsilon)
        {
            k = Math.Abs(k);
            c = Math.Abs(c);
            double diff = Math.Abs(k - c);

            if (diff < epsilon)
                return true;

            return false;
        }

        public static bool AlmostEquals(float k, float c, float epsilon)
        {
            k = Math.Abs(k);
            c = Math.Abs(c);
            float diff = Math.Abs(k - c);

            if (diff < epsilon)
                return true;

            return false;
        }

        public static int Factorial(int nbr)
        {
            int result = 1;

            for (int i = 2; i <= nbr; i++)
                result *= i;

            return result;
        }

        public static double Pow(double value, int power)
        {
            return Math.Pow(value,power);
        }
    }
}
