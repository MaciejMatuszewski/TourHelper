
using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface ICoordinateRepository:IBaseRepository<Coordinate>
    {
        IEnumerable<Coordinate> GetByTourID(int id);
        Coordinate GetByTourPointID(int id);
    }
}
