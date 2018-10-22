
using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Manager
{
    public interface IPointInRange
    {
        IEnumerable<Coordinate> GetPointsInRange(Coordinate origin, double latRange, double lonRange);
    }
}
