using System.Collections.Generic;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class UserTourRepository : BaseRepository<UserTour>, IUserTourRepository
    {
        public IEnumerable<UserTour> GetByUserIdAndTourId(int userId, int tourId)
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(UserTour)}] " +
                $"WHERE {nameof(UserTour.UserId)} = {userId} " +
                    $"AND {nameof(UserTour.TourId)} = {tourId}";

            return ExecuteSelectCommand(statement);
        }
    }
}
