
using System;
using TourHelper.Base.Manager.Calculators.MatrixTools;

namespace TourHelper.Manager.Calculators.MatrixTools
{
    class LUSolver : IMatrixSolver
    {
        public double Error { get; set; }
        public LUSolver()
        {
            Error = 0.000001;
        }
        public double[] Solve(double[,] A, double[] X)
        {
            int n = A.GetLength(0);
            LUDecomposition dec = new LUDecomposition();
            dec.BaseMatrix = A;
            if(!dec.Decompose()) return new double[X.Length]; //moze rzucic wyjatkiem

            double[,] lu = dec.DecomposedMatrix;
            // rozwiazanie Ly = b
            double[] y = new double[n];
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum = 0;
                for (int k = 0; k < i; k++)
                    sum += lu[i, k] * y[k];
                y[i] = X[i] - sum;
            }
            // rozwiazanie Ux = y
            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                if (Math.Abs(lu[i, i]) < Error) return new double[X.Length];
                sum = 0;
                for (int k = i + 1; k < n; k++)
                    sum += lu[i, k] * x[k];
                x[i] = (1 / lu[i, i]) * (y[i] - sum);
            }
            return x;

        }
    }
}
