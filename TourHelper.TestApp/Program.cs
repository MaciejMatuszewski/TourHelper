using TourHelper.Base.Model.Entity;
using TourHelper.Repository;
using TourHelper.Manager.Devices.Mock;
using TourHelper.Logic.Geolocation;

namespace TourHelper.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MockCompassManager compass=new MockCompassManager();
            MockGpsManager gps = new MockGpsManager();
            BasicRotationCalculator rot=new BasicRotationCalculator(compass,gps);
            Coordinates coor = new Coordinates();
            compass.Heading = 100;

            coor.Latitude = 16.92f;
            coor.Longitude = 52.46f;

            System.Console.WriteLine(rot.Bearing(coor));
            System.Console.WriteLine(rot.RotationAngle(coor));
            System.Console.ReadKey();
        }
    }
}
