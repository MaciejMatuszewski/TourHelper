﻿
using System;
using TourHelper.Base.Logic;
using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Logic.Geolocation
{
    public class HaversineDistanceCalculator : IDistanceCalculator
    {
        public IEarthRadiusCalculator EarthRadius { get; set; }
        public IGpsManager GpsManager { get; set; }
         
        public HaversineDistanceCalculator(IGpsManager gps,IEarthRadiusCalculator radius)
        {
            GpsManager = gps;
            EarthRadius = radius;

        }
        public double Distance(Coordinates coor)
        {
            double dLat,dLon,a,c;
            Coordinates lastLocation = GpsManager.GetCoordinates();

            dLat = MathTools.rad(coor.Latitude - lastLocation.Latitude);
            dLon = MathTools.rad(coor.Longitude - lastLocation.Longitude);

            a = Math.Pow(Math.Sin(dLon / 2),2) + Math.Cos(MathTools.rad(lastLocation.Longitude)) 
                * Math.Cos(MathTools.rad(coor.Longitude)) *Math.Pow(Math.Sin(dLat / 2),2);
            c = 2*Math.Atan2(Math.Sqrt(a),Math.Sqrt(1-a));


            return c*EarthRadius.GetEarthRadius(coor);
        } 
    }
}
