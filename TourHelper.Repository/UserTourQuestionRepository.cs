using System.Linq;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class UserTourQuestionRepository : BaseRepository<UserTourQuestion>, IUserTourQuestionRepository
    {
        public UserTourQuestion GetByUserTourIdAndTourQuestionId(int userTourId, int tourQuestionId)
        {
            string statement =
                $"SELECT TOP 1 * " +
                    $"FROM [dbo].[{nameof(UserTourQuestion)}] " +
                    $"WHERE {nameof(UserTourQuestion.UserTourId)} = {userTourId} " +
                        $"AND {nameof(UserTourQuestion.TourQuestionId)} = {tourQuestionId}";

            return ExecuteSelectCommand(statement).SingleOrDefault();
        }
    }
}
