
namespace TourHelper.Base.Manager.Calculators.MatrixTools
{
    public interface IMatrixSolver
    {
        double[] Solve(double[,] A,double[] X);
    }
}
