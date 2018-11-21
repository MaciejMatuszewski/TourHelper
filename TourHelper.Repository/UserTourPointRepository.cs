using System.Collections.Generic;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;
using System.Collections.Generic;
using System.Linq;

namespace TourHelper.Repository
{
    public class UserTourPointRepository : BaseRepository<UserTourPoint>, IUserTourPointRepository
    {
        public IEnumerable<UserTourPoint> GetByUserTourId(int userTourId)
        {
            string statement =
                $"SELECT * " +
                    $"FROM [dbo].[{nameof(UserTourPoint)}] " +
                    $"WHERE {nameof(UserTourPoint.UserTourId)} = {userTourId} ";

            return ExecuteSelectCommand(statement);
        }
    }
}
