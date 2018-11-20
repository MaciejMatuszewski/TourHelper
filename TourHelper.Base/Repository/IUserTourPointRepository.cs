using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface IUserTourPointRepository : IBaseRepository<UserTourPoint>
    {
        IEnumerable<UserTourPoint> GetByUserTourID(int userTourID);
    }

}
