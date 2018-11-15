using System.Collections.Generic;
using System.Linq;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class TourPointRepository : BaseRepository<TourPoint>, ITourPointRepository
    {


        public IEnumerable<TourPoint> GetByTourID(int tourID)
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(TourPoint)}] " +
                $"WHERE {nameof(TourPoint.TourId)} = '{tourID}'";

            return ExecuteSelectCommand(statement);
        }

        public IEnumerable<TourPoint> GetByCoordinateID(int coordinateID)
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(TourPoint)}] " +
                $"WHERE {nameof(TourPoint.CoordinateId)} = '{coordinateID}'";

            return ExecuteSelectCommand(statement);
        }
    }
}
