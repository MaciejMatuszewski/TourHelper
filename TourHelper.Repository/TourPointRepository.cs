using System.Collections.Generic;
using System.Linq;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class TourPointRepository : BaseRepository<TourPoint>, ITourPointRepository
    {
        public List<TourPoint> GetByTourID(int tourID)
        {
            return ExecuteSelectCommand($"SELECT * FROM [dbo].[{nameof(TourPoint)}] WHERE {nameof(TourPoint.TourId)} = '{tourID}'").ToList();
        }
    }
}
