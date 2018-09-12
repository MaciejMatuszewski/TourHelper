using TourHelper.Base.Model.Entity;
using UnityEngine;
using TourHelper.Base.Manager;
using System;
using TourHelper.Manager.Calculators;

namespace TourHelper.Manager
{
    public class CompassManager : BaseLocationManager,ICompassManager
    {
        private static CompassManager instance =null;
        private static readonly object key=new object();
        private DateTime TimeStamp { get; set; }
        private double  LastReading { get; set; }
        private double BufforReading { get; set; }
        public int Delay { get; set; } //max delay 60 sek
        public double Precision { get; set; }
        public double MaxChange { get; set; }
        public MeanFilter Filter { get; set; }
        public static CompassManager Instance {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            Input.compass.enabled = true;
                            instance = new CompassManager();
                            instance.MaxChange = 15d;
                            instance.Delay = 200;
                            instance.Precision = 2d;
                            instance.Filter = new MeanFilter(10);
                            instance.TimeStamp = DateTime.Now;
                            instance.BufforReading = Input.compass.trueHeading;
                            instance.LastReading = Input.compass.trueHeading;
                        }
                    }
                }
                return instance;
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

        private bool IsPrecise()
        {
            //Debug.Log("Precision:" + (Math.Abs(Input.compass.trueHeading - lastReading) >= Precision).ToString());
            if ((Math.Abs(Input.compass.trueHeading - LastReading) >= Precision))
            {
                return true;
            }
            return false;
        }
        private bool IsOverDelay()
        {
            //Debug.Log("TimeStamp:"+ timeStamp.ToString());
            TimeSpan diff = DateTime.Now.Subtract(TimeStamp);
            if ((diff.Milliseconds + diff.Seconds * 1000) >= Delay)
            {
                return true;
            }
            return false;
        }
        private bool IsChangeValid()
        {
            if ((Math.Abs((Input.compass.trueHeading - BufforReading))) <= MaxChange)
            {
                return true;
            }
            return false;
        }

        private double GetFilteredReading()
        {
            return Filter.GetValue(Input.compass.trueHeading);
        }

        public double GetAngleToNorth()
        {
           if (IsOverDelay() && IsPrecise())
            {
                BufforReading = GetFilteredReading();
                //Debug.Log("TimeStampDiff:" + (diff.Milliseconds + diff.Seconds * 1000).ToString());
                if (IsChangeValid())
                {
                    TimeStamp = DateTime.Now;
                    LastReading = GetFilteredReading();
                }

            }
            return LastReading;
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
