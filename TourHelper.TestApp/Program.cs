using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using TourHelper.Repository;

namespace TourHelper.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new UserRepository();
            var user = userRepository.GetByLogin("cycu");

            int a = 1;
            double[] output;
            TMConverter conv = new TMConverter();
            Coordinate c = new Coordinate();
            c.Latitude = 52.657570f;
            c.Longitude = 1.717922f;

            output = conv.ConvertCoordinates(c);

            System.Console.WriteLine("X:" + output[0].ToString() + "\nY:" + output[1].ToString() + "\nZ:");
            System.Console.ReadKey();
        }
    }
}
