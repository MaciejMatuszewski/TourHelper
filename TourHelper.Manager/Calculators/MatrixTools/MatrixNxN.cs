using System;
using TourHelper.Base.Manager.Calculators.MatrixTools;

namespace TourHelper.Manager.Calculators.MatrixTools
{
    public class MatrixNxN : Matrix,IMatrixNxN
    {
        public MatrixNxN(int n) : base(n, n) { }

        public IMatrixNxN Inverse()
        {
            IMatrixNxN resMatrix;
            LUSolver solver = new LUSolver();
            double[] x = new double[GetAll().GetLength(0)]; // kolumna potrzebna do wyznaczenia poszczegolnch kolumn
            double[] temp; //kolumna odwroconej macierzy
            resMatrix = new MatrixNxN(GetAll().GetLength(0));


            for (int i = 0; i < GetAll().GetLength(0); i++)
            {
                if (i != 0) x[i - 1] = 0;
                x[i] = 1;
                temp = solver.Solve(GetAll(), x);
                for (int j = 0; j < temp.Length; j++)
                {
                    resMatrix.SetByIndex(temp[j], j, i);
                }
            }

            return resMatrix;
        }
    }
}
