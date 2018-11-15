using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourHelper.Base.Atrybuty;
using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;
using TourHelper.Repository;
using UnityEngine;

namespace TourHelper.Manager
{
    [Prefab(Name = "InfoPoint")]
    public class InfoPointsInRangeManager : IPointInRange
    {
        public IEnumerable<Coordinate> GetPointsInRange(Coordinate origin, double latRange, double lonRange)
        {
            int tourId = 1;//PlayerPrefs.GetInt["TourID"];
            var repoPoints = new CoordinateRepository();

           var tourPoints=repoPoints.GetByTourID(tourId)
                .Where(n=>(Math.Abs(n.Latitude-origin.Latitude)< latRange)
                && (Math.Abs(n.Longitude - origin.Longitude) < lonRange)).ToList();
            return tourPoints;
        }
    }
}
