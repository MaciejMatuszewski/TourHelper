using System.Linq;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User GetByLogin(string login)
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(User)}] " +
                $"WHERE {nameof(User.Login)} = '{login}'";

            return ExecuteSelectCommand(statement).SingleOrDefault();
        }
    }
}
