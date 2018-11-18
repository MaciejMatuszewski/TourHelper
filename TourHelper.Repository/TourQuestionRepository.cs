using System.Collections.Generic;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class TourQuestionRepository : BaseRepository<TourQuestion>, ITourQuestionRepository
    {
        public IEnumerable<TourQuestion> GetByTourId(int tourId)
        {
            string statement =
                $"SELECT * " +
                $"FROM [dbo].[{nameof(TourQuestion)}] " +
                $"WHERE {nameof(TourQuestion.TourId)} = {tourId} " +
                $"ORDER BY {nameof(TourQuestion.Id)}";

            return ExecuteSelectCommand(statement);
        }
    }
}
