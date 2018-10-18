
using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Manager
{
    public interface IPointInRange
    {
        IEnumerable<Coordinates> GetPointsInRange(Coordinates origin, double latRange, double lonRange);
    }
}
