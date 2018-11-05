using System;
using System.Collections.Generic;
using System.Linq;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class UserSessionRepository : BaseRepository<UserSession>, IUserSessionRepository
    {
        public UserSession GetLastUserSession(int userId)
        {
            string statement =
                $"SELECT TOP 1 * " +
                $"FROM [dbo].[{nameof(UserSession)}] " +
                $"WHERE {nameof(UserSession.UserId)} = {userId} " +
                $"ORDER BY {nameof(UserSession.Id)} DESC";

            return ExecuteSelectCommand(statement).SingleOrDefault();
        }

        public IEnumerable<UserSession> GetLogInUsers()
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(UserSession)}] " +
                $"WHERE {nameof(UserSession.LogIn)} IS NOT NULL " +
                    $"AND {nameof(UserSession.LogOut)} IS NULL";

            return ExecuteSelectCommand(statement);
        }

        public IEnumerable<UserSession> GetLogInUsers(int lastSeenSeconds)
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(UserSession)}] " +
                $"WHERE {nameof(UserSession.LogIn)} IS NOT NULL " +
                    $"AND {nameof(UserSession.LogOut)} IS NULL " +
                    $"AND {nameof(UserSession.LastSeen)} >= '{DateTime.Now.AddSeconds(-lastSeenSeconds)}'";

            return ExecuteSelectCommand(statement);
        }

        public IEnumerable<UserSession> GetUserSessions(int userId)
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(UserSession)}] " +
                $"WHERE {nameof(UserSession.UserId)} = {userId}";

            return ExecuteSelectCommand(statement);
        }
    }
}
