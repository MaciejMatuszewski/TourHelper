

using System;
using TourHelper.Base.Manager.Calculators.MatrixTools;

namespace TourHelper.Manager.Calculators.MatrixTools
{
    public class Matrix:IMatrix

    {
        private double[,] matrix;

        public Matrix(int m,int n)
        {
            matrix = new double[m, n];
        }


        public IMatrix Divide(IMatrix B)
        {
            int row = Math.Min(matrix.GetLength(0), B.GetLength(0));
            int col = Math.Min(matrix.GetLength(1), B.GetLength(1));
            IMatrix res = new Matrix(row, col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Math.Abs(B.GetByIndex(i, j)) < 0.000001)
                    {
                        //res.SetByIndex(0, i, j);
                        throw new Exception();
                    }
                    res.SetByIndex(matrix[i, j] / B.GetByIndex(i, j), i, j);
                }
            }
            return res;
        }

        public IMatrix Add(IMatrix B)
        {
            int row = Math.Min(matrix.GetLength(0), B.GetLength(0));
            int col = Math.Min(matrix.GetLength(1), B.GetLength(1));
            IMatrix res = new Matrix(row, col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    res.SetByIndex(matrix[i, j] + B.GetByIndex(i, j), i, j);
                }
            }
            return res;
        }

        public IMatrix Sub(IMatrix B)
        {
            int row = Math.Min(matrix.GetLength(0), B.GetLength(0));
            int col = Math.Min(matrix.GetLength(1), B.GetLength(1));
            IMatrix res = new Matrix(row, col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    res.SetByIndex(matrix[i, j] - B.GetByIndex(i, j), i, j);
                }
            }
            return res;
        }



        public IMatrix Multiply(IMatrix B)
        {
            IMatixMultiplication mm = new MatrixMultiIKJ();
            IMatrix res = new Matrix(matrix.GetLength(0), B.GetLength(1));
            res.SetAll(mm.Multiply(matrix, B.GetAll()));
            return res;

        }

        public void SetByIndex(double v, int m, int n)
        {
            if (m < matrix.GetLength(0) && n < matrix.GetLength(1))
            {
                matrix[m, n] = v;
                return;
            }
            throw new Exception();
        }
        public double GetByIndex(int m, int n)
        {
            if (m < matrix.GetLength(0) && n < matrix.GetLength(1))
            {
                return matrix[m, n];
            }
            throw new Exception();
        }

        public IMatrix Transpose()
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            IMatrix res = new Matrix(col, row);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    res.SetByIndex(matrix[i, j], j, i);
                }
            }
            return res;
        }
        public IMatrix Diagonal()
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            IMatrix res = new Matrix(col, row);
            for (int i = 0; i < row; i++)
            {
                res.SetByIndex(matrix[i, i], i, i);
            }
            return res;
        }

        public void SetAll(double[,] array)
        {
            if ((array.GetLength(0) == matrix.GetLength(0)) && (array.GetLength(1) == matrix.GetLength(1)))
            {
                matrix = array;
                return;
            }
            throw new Exception();
        }

        public int GetLength(int n)
        {
            return matrix.GetLength(n);
        }

        public double[,] GetAll()
        {
            return matrix;
        }

        public IMatrix DivideDiagonal(IMatrix B)
        {
            int row = Math.Min(matrix.GetLength(0), B.GetLength(0));
            int col = Math.Min(matrix.GetLength(1), B.GetLength(1));
            IMatrix res = new Matrix(row, col);
            for (int i = 0; i < row; i++)
            {
                res.SetByIndex(matrix[i, i] / B.GetByIndex(i, i), i, i);
            }
            return res;
        }
    }
}
