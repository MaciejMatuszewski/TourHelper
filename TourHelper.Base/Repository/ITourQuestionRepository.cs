using TourHelper.Base.Model.Entity;
using System.Collections.Generic;

namespace TourHelper.Base.Repository
{
    public interface ITourQuestionRepository : IBaseRepository<TourQuestion>
    {
        IEnumerable<TourQuestion> GetByTourId(int tourId);
    }
}
