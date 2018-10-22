
using System.IO;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager;

namespace TourHelper.TestApp.Position
{
    public class RandomCoinsTest
    {

        public void test()
        {
            RandomCoinsInRangeManager d = new RandomCoinsInRangeManager();

            var dd = d.GetPointsInRange(new Coordinate(), 0.1, 0.1);

            string path = "Random.txt";


            using (StreamWriter s = File.Exists(path) ? File.AppendText(path) : File.CreateText(path))
            {
                foreach(Coordinate c in dd)
                {
                    s.WriteLine(c.Latitude+";"+c.Longitude);
                }
            }
        }
    }
}
