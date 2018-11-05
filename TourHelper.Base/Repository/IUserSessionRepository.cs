using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface IUserSessionRepository : IBaseRepository<UserSession>
    {
        IEnumerable<UserSession> GetLogInUsers();

        IEnumerable<UserSession> GetLogInUsers(int lastSeenSeconds);

        IEnumerable<UserSession> GetUserSessions(int userId);

        UserSession GetLastUserSession(int userId);
    }
}
