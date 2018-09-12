
namespace TourHelper.Base.Manager.Calculators
{
    public interface IElipsoid
    {
        float GetSemiMinor();
        float GetSemiMajor();
        float GetEccentricity();
        float GetSqrtEccentricity();
    }
}
