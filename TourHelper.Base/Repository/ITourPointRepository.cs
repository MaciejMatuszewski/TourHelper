using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface ITourPointRepository : IBaseRepository<TourPoint>
    {
        List<TourPoint> GetByTourID(int tourID);
    }
}
