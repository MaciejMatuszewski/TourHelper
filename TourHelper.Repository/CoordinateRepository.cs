using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Coordinate> GetUnvisited(int userTourId)
        {
            string statement = 
                $"SELECT C.* " +
                $"FROM [dbo].[{nameof(UserTour)}] UT JOIN [dbo].[{nameof(TourPoint)}] TP ON TP.[{nameof(TourPoint.TourId)}] = UT.[{nameof(UserTour.TourId)}] " +
                $"JOIN [dbo].[{nameof(Coordinate)}] C ON C.[{nameof(Coordinate.Id)}] = TP.[{nameof(TourPoint.CoordinateId)}] " +
                $"WHERE UT.[{nameof(UserTour.Id)}] = {userTourId} AND NOT EXISTS(SELECT * FROM [dbo].[{nameof(UserTourPoint)}]  UTP " +
                $"WHERE UTP.[{nameof(UserTourPoint.TourPointId)}]= TP.[{nameof(TourPoint.Id)}] AND UTP.[{nameof(UserTourPoint.UserTourId)}] = UT.[{nameof(UserTour.Id)}])";
            //string statement =
            //$" SELECT C.* " +
            //$"FROM [dbo].[{nameof(Coordinate)}] C" +
            //$"JOIN [dbo].[{nameof(Coordinate)}] on [dbo].[{nameof(TourPoint)}]" +
            //$".[{nameof(TourPoint.CoordinateId)}]=[dbo].[{nameof(Coordinate)}]" +
            //$".[{nameof(Coordinate.Id)}]" +
            //$"WHERE [dbo].[{nameof(TourPoint)}].[{nameof(TourPoint.TourId)}]={id}";

            return ExecuteSelectCommand(statement);
           // throw new NotImplementedException();
        }

        public Coordinate GetByTourPointID(int id)
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
                    $".[{nameof(Coordinate.Id)}]" +
                    $"WHERE [dbo].[{nameof(TourPoint)}].[{nameof(TourPoint.Id)}]={id}";

            return ExecuteSelectCommand(statement).SingleOrDefault(); ;
        }
    }
}
