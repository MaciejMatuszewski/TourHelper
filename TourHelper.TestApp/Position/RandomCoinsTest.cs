using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager;

namespace TourHelper.TestApp.Position
{
    public class RandomCoinsTest
    {

        public void test()
        {
            RandomCoinsInRangeManager d = new RandomCoinsInRangeManager();

            var dd = d.GetPointsInRange(new Coordinates(), 0.1, 0.1);

            string path = "Random.txt";


            using (StreamWriter s = File.Exists(path) ? File.AppendText(path) : File.CreateText(path))
            {
                foreach(Coordinates c in dd)
                {
                    s.WriteLine(c.Latitude+";"+c.Longitude);
                }
            }
        }
    }
}
