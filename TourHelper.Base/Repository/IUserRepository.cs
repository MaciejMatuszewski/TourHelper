using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IEnumerable<User> GetByLogin(string login);
    }
}
