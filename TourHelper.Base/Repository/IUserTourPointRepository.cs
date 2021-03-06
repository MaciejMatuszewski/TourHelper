﻿using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface IUserTourPointRepository : IBaseRepository<UserTourPoint>
    {
        IEnumerable<UserTourPoint> GetByUserTourId(int userTourId);
    }

}
