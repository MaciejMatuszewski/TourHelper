using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface IUserTourQuestionRepository : IBaseRepository<UserTourQuestion>
    {
        UserTourQuestion GetByUserTourIdAndTourQuestionId(int userTourId, int tourQuestionId);
    }
}
