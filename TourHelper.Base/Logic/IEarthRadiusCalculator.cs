
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Logic
{
    public interface IEarthRadiusCalculator
    {
        double GetEarthRadius(Coordinates coor);
    }
}
