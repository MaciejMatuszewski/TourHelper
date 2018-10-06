
using System;
using TourHelper.Base.Manager.Calculators.MatrixTools;

namespace TourHelper.Manager.Calculators.MatrixTools
{
    class LUDecomposition : IDecomposition
    {

        public double[,] DecomposedMatrix { get; private set; }
        public double[,] BaseMatrix { get; set; }
        public double Error { get; set; }

        public LUDecomposition()
        {
            Error = 0.000001;
        }
        public bool Decompose()
        {
            // decomposition of matrix
            int len = BaseMatrix.GetLength(0);
            if (len!=BaseMatrix.GetLength(1)) return false;
            DecomposedMatrix = new double[len, len];
            double sum = 0;
            for (int i = 0; i < len; i++)
            {
                if (Math.Abs(BaseMatrix[i, i]) <Error) return false;
                for (int j = i; j < len; j++)
                {
                    sum = 0;
                    for (int k = 0; k < i; k++)
                        sum += DecomposedMatrix[i, k] * DecomposedMatrix[k, j];
                    DecomposedMatrix[i, j] = BaseMatrix[i, j] - sum;
                }
                for (int j = i + 1; j < len; j++)
                {
                    sum = 0;
                    for (int k = 0; k < i; k++)
                        sum += DecomposedMatrix[j, k] * DecomposedMatrix[k, i];
                    DecomposedMatrix[j, i] = (1 / DecomposedMatrix[i, i]) * (BaseMatrix[j, i] - sum);
                }
            }
            return true;
        }

        public void SetBaseMatrix(double[,] A)
        {
            BaseMatrix=A;
        }

        public double[,] GetBaseMatrix()
        {
            return BaseMatrix;
        }

        public double[,] GetDecomposedMatrix()
        {
            return DecomposedMatrix;
        }
    }
}
