using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface IUserTourRepository : IBaseRepository<UserTour>
    {
        IEnumerable<UserTour> GetByUserIdAndTourId(int userId, int tourId);
    }
}
