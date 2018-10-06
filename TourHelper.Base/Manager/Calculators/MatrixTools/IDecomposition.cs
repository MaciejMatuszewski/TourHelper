
namespace TourHelper.Base.Manager.Calculators.MatrixTools
{
    public interface IDecomposition
    {
        bool Decompose();
        void SetBaseMatrix(double[,] A);
        double[,] GetBaseMatrix();

        double[,] GetDecomposedMatrix();
    }
}
