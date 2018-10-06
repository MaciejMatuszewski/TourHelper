
namespace TourHelper.Base.Manager.Calculators.MatrixTools
{
    public interface IMatrix
    {

        IMatrix Multiply(IMatrix B);
        IMatrix Divide(IMatrix B);
        IMatrix Add(IMatrix B);
        IMatrix Sub(IMatrix B);
        IMatrix Transpose();
        double GetByIndex(int m, int n);
        void SetByIndex(double v,int m, int n);
        void SetAll(double[,] array);
        double[,] GetAll();
        int GetLength(int n);
        IMatrix Diagonal();
        IMatrix DivideDiagonal(IMatrix B);
    }
}
