
using TourHelper.Base.Manager.Calculators.MatrixTools;

namespace TourHelper.Base.Manager.Calculators.Kalman
{
    public interface IKalman
    {
        void Predict(IMatrix accelerations);
        void Update(IMatrix gpsMesurements);

    }
}
