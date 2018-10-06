

using TourHelper.Base.Manager.Calculators.MatrixTools;

namespace TourHelper.Manager.Calculators.MatrixTools
{
    public class MatrixMultiIKJ: IMatixMultiplication
    {

        public double[,] Multiply(double[,] matrix_a, double[,] matrix_b)
        {
            if (matrix_a.GetLength(1)== matrix_b.GetLength(0))
            {
                double[,] matrix_r = new double[matrix_a.GetLength(0), matrix_b.GetLength(1)];
                for (int i = 0; i < matrix_a.GetLength(0); i++)
                    for (int k = 0; k < matrix_b.GetLength(0); k++)
                        for (int j = 0; j < matrix_b.GetLength(1); j++)
                            matrix_r[i, j] += matrix_a[i, k] * matrix_b[k, j];

                return matrix_r;
            }
            throw new System.Exception();
        }
    }
}
