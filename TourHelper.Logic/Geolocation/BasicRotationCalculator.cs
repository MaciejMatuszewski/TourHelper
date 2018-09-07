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
        public IGpsManager GpsManager { get; set ; }

        
        public BasicRotationCalculator(ICompassManager compass,IGpsManager gps)
        {
            CompassManager = compass;
            GpsManager = gps;

        }


        public double Bearing(Coordinates coor)
        {
            //longitude (lambda)
            //latitude (phi)
            double x, y,dLon, bearing;
            Coordinates lastLocation=GpsManager.GetCoordinates();

            dLon = coor.Longitude - lastLocation.Longitude;
            
            y = Math.Sin(MathTools.rad(dLon)) *Math.Cos(MathTools.rad(coor.Latitude));
            x = Math.Cos(MathTools.rad(lastLocation.Latitude)) * Math.Sin(MathTools.rad(coor.Latitude)) 
                - Math.Sin(MathTools.rad(lastLocation.Latitude)) * Math.Cos(MathTools.rad(coor.Latitude)) 
                *Math.Cos(MathTools.rad(dLon));
            
            bearing =Math.Atan2(y,x);
            
            return (MathTools.deg(bearing)+360)%360;
        }

        public double RotationAngle(Coordinates coor)
        {
            return Bearing(coor) - CompassManager.GetAngleToNorth();
        }
         
        public void Transform(Transform obj,Coordinates coor)
        {

            obj.transform.rotation = Quaternion.AngleAxis((float)RotationAngle(coor), new Vector3(0, 1, 0));
        }


    }
}
