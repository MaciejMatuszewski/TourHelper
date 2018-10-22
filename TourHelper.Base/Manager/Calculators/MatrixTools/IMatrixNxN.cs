
namespace TourHelper.Base.Manager.Calculators.MatrixTools
{
    public interface IMatrixNxN: IMatrix
    {
        IMatrixNxN Inverse();
    }
}
