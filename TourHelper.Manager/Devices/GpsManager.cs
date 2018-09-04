using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Manager
{
    public class GpsManager : IGpsManager
    {
        private static GpsManager instance=null;
        private static readonly object key=new object();

        public static GpsManager Instance {
            get
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        instance = new GpsManager();
                    }
                    return instance;
                }
            }
        }


        public Coordinates GetCoordinates()
        {
            //Gdzie obsluzyc blad jesli urzadzenie nie ready ?!

            Coordinates output = new Coordinates();

            output.Longitude = Input.location.lastData.longitude;
            output.Latitude = Input.location.lastData.latitude;
            output.Altitude = Input.location.lastData.altitude;
            output.VerticalAccuracy = Input.location.lastData.verticalAccuracy;
            output.HorizontalAccuracy = Input.location.lastData.horizontalAccuracy;

            return output;
        }

        public bool IsEnabled()
        {
            return Input.location.isEnabledByUser;
        }

        public bool IsReady()
        {
            if (Input.location.status != LocationServiceStatus.Running)
            {
                return false;
            }
            return true;
        }

        public IEnumerator StartService(int timeOut)
        {
            //nalezy zastosowac corutine
            if (!IsEnabled())
            {
                Debug.Log("Location disabled by user");
                yield break;
            }
            
            Input.location.Start();

            while (Status() == ServiceStatus.Initializing && timeOut > 0)
            {
                yield return new WaitForSeconds(1);
                timeOut--;
            }
            if (timeOut <= 0)
            {
                Debug.Log("Time out");
                yield break;
            }
            if (Status() == ServiceStatus.Failed)
            {
                Debug.Log("Unable to get location");
                yield break;
            }
        }

        public ServiceStatus Status()
        {
            return (ServiceStatus)Input.location.status;
        }
    }
}
