using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;
using System.Collections.Generic;
using System.Linq;

namespace TourHelper.Repository
{
    public class UserTourPointRepository : BaseRepository<UserTourPoint>, IUserTourPointRepository
    {
        public IEnumerable<UserTourPoint> GetByUserTourID(int userTourID)
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(UserTourPoint)}] " +
                $"WHERE {nameof(UserTourPoint.UserTourId)} = '{userTourID}'";

            return ExecuteSelectCommand(statement);
        }
    }
}
