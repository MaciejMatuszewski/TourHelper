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

        public static CompassManager Instance {
            get
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        Input.compass.enabled = true;
                        instance = new CompassManager();
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
            return Input.compass.trueHeading;
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
