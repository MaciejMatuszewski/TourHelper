using System.Linq;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User GetByLogin(string login)
        {
            return ExecuteSelectCommand($"SELECT * FROM [dbo].[{nameof(User)}] WHERE {nameof(User.Login)} = '{login}'").SingleOrDefault();
        }
    }
}
