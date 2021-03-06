﻿using System;
using TourHelper.Base.Logic;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using UnityEngine;


namespace TourHelper.Logic.Geolocation
{
    public class BasicRotationCalculator : IRotationCalculator
    {
        
       // public ICompassManager CompassManager { get; set; }
        public IGpsManager GpsManager { get; set ; }
        public IGyroManager GyroManager { get; set; }

        public int Scale { get; set; }

        public BasicRotationCalculator(IGyroManager gyro,IGpsManager gps)
        {
            GyroManager = gyro;
            GpsManager = gps;
            Scale = 2;

        }



        public double Bearing(Coordinate coor)
        {
            //longitude (lambda)
            //latitude (phi)
            double x, y,dLon, bearing;
            Coordinate lastLocation=GpsManager.GetCoordinates();

            dLon = coor.Longitude - lastLocation.Longitude;
            
            y = Math.Sin(MathTools.rad(dLon)) *Math.Cos(MathTools.rad(coor.Latitude));
            x = Math.Cos(MathTools.rad(lastLocation.Latitude)) * Math.Sin(MathTools.rad(coor.Latitude)) 
                - Math.Sin(MathTools.rad(lastLocation.Latitude)) * Math.Cos(MathTools.rad(coor.Latitude)) 
                *Math.Cos(MathTools.rad(dLon));
            
            bearing =Math.Atan2(y,x);
            
            return (MathTools.deg(bearing)+360)%360;
        }

        public double RotationAngle(Coordinate coor)
        {

            return Bearing(coor) - (360 - (GyroManager.GetRotation()).eulerAngles.z);
        }
         
        public void Transform(Transform obj,Coordinate coor)
        {
            Quaternion start = obj.transform.localRotation;
            Quaternion end = Quaternion.AngleAxis((float)RotationAngle(coor), new Vector3(0, 1, 0));

            obj.transform.localRotation = Quaternion.Slerp(start, end, Time.deltaTime * Scale);
            //obj.transform.localRotation = Quaternion.AngleAxis((float)RotationAngle(coor), new Vector3(0, 1,0));
        }


    }
}
