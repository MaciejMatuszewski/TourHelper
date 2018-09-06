using TourHelper.Base.Logic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Logic.Geolocation
{
    public class MeanEarthRadius : IEarthRadiusCalculator
    {
        public double GetEarthRadius(Coordinates coor)
        {
            return 6371001d;
        }
    }
}
