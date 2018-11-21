using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface IUserProfileRepository : IBaseRepository<UserProfile>
    {
        IEnumerable<UserProfile> GetByEmail(string email);
    }
}
