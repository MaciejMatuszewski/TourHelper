using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Manager.Devices
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
                            instance.DesiredAccuracy = 3;
                            instance.DesiredChange = 3;
                        }
                    }
                }
                return instance;
            }
        }


        public Coordinate GetCoordinates()
        {
            //Gdzie obsluzyc blad jesli urzadzenie nie ready ?!

            var output = new Coordinate();

            output.Longitude = Input.location.lastData.longitude;
            output.Latitude = Input.location.lastData.latitude;
            output.Altitude = Input.location.lastData.altitude;
            output.VerticalAccuracy = Input.location.lastData.verticalAccuracy;
            output.HorizontalAccuracy = Input.location.lastData.horizontalAccuracy;

            return output;
        }
      }
}
