
using System;
using TourHelper.Base.Logic;
using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Logic.Geolocation
{
    public class HaversineDistanceCalculator : IDistanceCalculator
    {
        public IGpsManager GpsManager { get; set; }
        HaversineDistanceCalculator(IGpsManager gps)
        {
            GpsManager = gps;

        }
        public double Distance(Coordinates coor)
        {
            double dLat,dLon;
            Coordinates lastLocation = GpsManager.GetCoordinates();

            dLat = coor.Latitude - lastLocation.Latitude;
            dLon = coor.Longitude - lastLocation.Longitude;

            return dLon;
        } 
    }
}
