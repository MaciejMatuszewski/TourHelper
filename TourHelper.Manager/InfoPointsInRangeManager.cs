﻿using System;
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
            int tourId = PlayerPrefs.GetInt("TourID");
            int userTourId = PlayerPrefs.GetInt("UserTourID");
            var coordinateRepository = new CoordinateRepository();
            IEnumerable<Coordinate> tourPoints = coordinateRepository.GetUnvisited(userTourId);
            //IEnumerable<Coordinate> tourPoints = coordinateRepository.GetByTourID(tourId);

            var tourPointsFiltered = tourPoints
                 .Where(n => (Math.Abs(n.Latitude - origin.Latitude) < latRange)
                 && (Math.Abs(n.Longitude - origin.Longitude) < lonRange)).ToList();
            return tourPointsFiltered;
        }
    }
}
