using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Manager
{
    public class GpsManager : BaseLocationManager,IGpsManager
    {
        private static GpsManager instance=null;
        private static readonly object key=new object();

        public static GpsManager Instance {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new GpsManager();
                        }
                    }
                }
                return instance;
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
      }
}
