using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface ITourPointRepository : IBaseRepository<TourPoint>
    {
        IEnumerable<TourPoint> GetByTourID(int tourID);
        IEnumerable<TourPoint> GetByCoordinateID(int coordinateID);
    }
}
