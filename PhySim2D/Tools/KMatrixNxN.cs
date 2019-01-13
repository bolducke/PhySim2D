namespace PhySim2D.Tools
{
    class KMatrixNxN
    {
        public int N { get; private set; }

        public double[,] Mat { get; set; }

        public KMatrixNxN(int size)
        {
            Mat = new double[size,size];
            N = size;

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    Mat[i, j] = 0;
        }

        #region Algebric vector operation
        public static KMatrixNxN Add(KMatrixNxN A, KMatrixNxN B)
        {
            if (A.N == B.N)
            {
                int n = A.N;
                KMatrixNxN m = new KMatrixNxN(n);

                for (int i = 0; i < n; i++)
                    for (int p = 0; p < n; i++)
                        m.Mat[i, p] = A.Mat[i, p] + B.Mat[i, p];

                return m;
            }

            return new KMatrixNxN(0);
        }

        public static KMatrixNxN Substract(KMatrixNxN A, KMatrixNxN B)
        {
            if (A.N == B.N)
            {
                int n = A.N;
                KMatrixNxN m = new KMatrixNxN(n);

                for (int i = 0; i < n; i++)
                    for (int p = 0; p < n; i++)
                        m.Mat[i, p] = A.Mat[i, p] - B.Mat[i, p];

                return m;
            }

            return new KMatrixNxN(0);
        }

        public static KMatrixNxN Multiply(double k, KMatrixNxN A)
        {
                int n = A.N;
                KMatrixNxN m = new KMatrixNxN(n);

                for (int i = 0; i < n; i++)
                    for (int p = 0; p < n; i++)
                        m.Mat[i, p] = k * A.Mat[i, p];

                return m;
        }

        public static KMatrixNxN Multiply(KMatrixNxN A, double k)
        {
            int n = A.N;
            KMatrixNxN m = new KMatrixNxN(n);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    m.Mat[i, j] = k * A.Mat[i, j];

            return m;
        }

        public static KMatrixNxN Multiply(KMatrixNxN A, KMatrixNxN B)
        {
            if (A.N == B.N)
            {
                int n = A.N;
                KMatrixNxN m = new KMatrixNxN(n);

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        for (int l = 0; l < n; l++)
                            m.Mat[i, j] += A.Mat[i, l] * B.Mat[l, j];

                return m;
            }

            return new KMatrixNxN(0);
        }
        #endregion

        public static KMatrixNxN operator +(KMatrixNxN A, KMatrixNxN B)
        {
            return Add(A, B);
        }

        public static KMatrixNxN operator -(KMatrixNxN A, KMatrixNxN B)
        {
            return Substract(A, B);
        }

        public static KMatrixNxN operator -(KMatrixNxN A)
        {
            return Multiply(-1, A);
        }

        public static KMatrixNxN operator *(double k, KMatrixNxN A)
        {
            return Multiply(k, A);
        }

        public static KMatrixNxN operator *(KMatrixNxN A, double k)
        {
            return Multiply(A, k);
        }

        public static KMatrixNxN operator *(KMatrixNxN A, KMatrixNxN B)
        {
            return Multiply(A, B);
        }
    }
}
