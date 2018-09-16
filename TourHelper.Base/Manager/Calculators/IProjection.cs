using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Manager.Calculators
{
    public interface IProjection
    {
        float ScaleFactor(Coordinate c);
        float EastingOfTrueOrigin(Coordinate c);
        float LatOfTrueOrigin(Coordinate c);
        float LonOfTrueOrigin(Coordinate c);
        float NorthingOfTrueOrigin(Coordinate c);

    }
}
