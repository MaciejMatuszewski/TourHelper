
using TourHelper.Base.Manager.Calculators.MatrixTools;

namespace TourHelper.Base.Manager.Calculators.Kalman
{
    public interface IKalman
    {
        IMatrix InitialPosition { get; set; }
        IMatrix Prediction { get; }
        double DeltaTime { get; set; }
        double AccelerationError { get; set; }
        double GPSError { get; set; }
        void Predict(IMatrix accelerations);
        void Update(IMatrix gpsMesurements);
        void ResetVelocity();
    }
}
