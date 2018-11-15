using System.Collections.Generic;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public class CoordinateRepository : BaseRepository<Coordinate>, ICoordinateRepository
    {
        public IEnumerable<Coordinate> GetByTourID(int id)
        {
            string statement =
            $" SELECT [dbo].[{nameof(Coordinate)}].[{nameof(Coordinate.Id)}]" +
            $",[dbo].[{nameof(Coordinate)}].[{nameof(Coordinate.Latitude)}]" +
            $",[dbo].[{nameof(Coordinate)}].[{nameof(Coordinate.Longitude)}]" +
            $",[dbo].[{nameof(Coordinate)}].[{nameof(Coordinate.Altitude)}]" +
            $",[dbo].[{nameof(Coordinate)}].[{nameof(Coordinate.VerticalAccuracy)}]" +
            $",[dbo].[{nameof(Coordinate)}].[{nameof(Coordinate.HorizontalAccuracy)}]" +
            $",[dbo].[{nameof(Coordinate)}].[{nameof(Coordinate.CreatedOn)}]" +
            $"FROM [dbo].[{nameof(TourPoint)}]" +
            $"LEFT JOIN [dbo].[{nameof(Coordinate)}] on [dbo].[{nameof(TourPoint)}]" +
            $".[{nameof(TourPoint.CoordinateId)}]=[dbo].[{nameof(Coordinate)}]" +
            $".[{nameof(Coordinate.Id)}]"+
            $"WHERE [dbo].[{nameof(TourPoint)}].[{nameof(TourPoint.TourId)}]={id}";

            return ExecuteSelectCommand(statement);
        }
    }
}
