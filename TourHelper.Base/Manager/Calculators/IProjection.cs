using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Manager.Calculators
{
    public interface IProjection
    {
        float ScaleFactor(Coordinates c);
        float EastingOfTrueOrigin(Coordinates c);
        float LatOfTrueOrigin(Coordinates c);
        float LonOfTrueOrigin(Coordinates c);
        float NorthingOfTrueOrigin(Coordinates c);

    }
}
