using TourHelper.Base.Model.Entity;
using UnityEngine;
using TourHelper.Base.Manager;
using System;

namespace TourHelper.Manager
{
    public class CompassManager : BaseLocationManager,ICompassManager
    {
        private static CompassManager instance =null;
        private static readonly object key=new object();
        private DateTime timeStamp;
        private double  lastReading;
        public int Delay { get; set; }
        public double Precision { get; set; }
        public static CompassManager Instance {
            get
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        Input.compass.enabled = true;
                        instance = new CompassManager();
                        //instance.timeStamp = DateTime.Now;
                        instance.Delay = 200;
                        instance.Precision = 2d;
                    }
                    return instance;
                }
            }
        }
        override public bool IsEnabled()
        {
            return Input.location.isEnabledByUser && Input.compass.enabled;
        }

        override public bool IsReady()
        {
            if ((Input.location.status != LocationServiceStatus.Running) && Input.compass.enabled)
            {
                return false;
            }
            return true;
        }


        public double GetAngleToNorth()
        {

            TimeSpan diff = DateTime.Now.Subtract(timeStamp);

           if (((diff.Milliseconds+diff.Seconds*1000) >= Delay)&&(Math.Abs(Input.compass.trueHeading - lastReading)>=Precision))
            {
                //Debug.Log("TimeStampDiff:" + (diff.Milliseconds + diff.Seconds * 1000).ToString());
                timeStamp = DateTime.Now;
                lastReading = (Input.compass.trueHeading +lastReading)*0.5;
                
            }


            return lastReading;
        }

        public void CompassOn()
        {
            if (!Input.compass.enabled) Debug.Log("Compass ON");
            Input.compass.enabled = true;
        }

        public void CompassOff()
        {
            if (Input.compass.enabled) Debug.Log("Compass OFF");
            Input.compass.enabled=false;
        }
    }
}
