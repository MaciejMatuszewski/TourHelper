using System.Collections.Generic;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        public IEnumerable<UserProfile> GetByEmail(string email)
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(UserProfile)}] " +
                $"WHERE {nameof(UserProfile.Email)} = '{email}'";

            return ExecuteSelectCommand(statement);
        }
    }
}
