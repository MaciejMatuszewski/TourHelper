
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Logic
{
    public interface IDistanceCalculator
    {
        double Distance(Coordinate coor);
    }
}
