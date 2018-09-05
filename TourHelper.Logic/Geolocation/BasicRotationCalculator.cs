using System;
using TourHelper.Base.Logic;
using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;
using UnityEngine;


namespace TourHelper.Logic.Geolocation
{
    public class BasicRotationCalculator : IRotationCalculator
    {
        public ICompassManager CompassManager { get; set; }
        public IGpsManager GpsManager { get; set; }
        public BasicRotationCalculator(ICompassManager compass,IGpsManager gps)
        {
            CompassManager = compass;
            GpsManager = gps;
        }

        public double Bearing(Coordinates coor)
        {
            //latitude (lambda)
            //longitude (phi)
            double x, y,dLat, bearing;
            Coordinates lastLocation=GpsManager.GetCoordinates();

            dLat = coor.Latitude - lastLocation.Latitude;
            
            y = Math.Sin(MathTools.rad(dLat)) *Math.Cos(MathTools.rad(coor.Longitude));
            x = Math.Cos(MathTools.rad(lastLocation.Longitude)) * Math.Sin(MathTools.rad(coor.Longitude)) 
                - Math.Sin(MathTools.rad(lastLocation.Longitude)) * Math.Cos(MathTools.rad(coor.Longitude)) 
                *Math.Cos(MathTools.rad(dLat));
            
            bearing =Math.Atan2(y,x);
            
            return (MathTools.deg(bearing)+360)%360;
        }

        public double RotationAngle(Coordinates coor)
        {
            return CompassManager.GetAngleToNorth()-Bearing(coor);
        }

        public void Transform(Transform obj)
        {
            throw new NotImplementedException();
        }
    }
}
