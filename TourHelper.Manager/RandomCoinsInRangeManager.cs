using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourHelper.Base.Atrybuty;
using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Manager
{

    [Prefab(Name = "Coin")]
    public class RandomCoinsInRangeManager : IPointInRange
    {
        public int NumberOfCoins { get; set; }
        
        public RandomCoinsInRangeManager()
        {
            NumberOfCoins = 100;
        }

        public IEnumerable<Coordinate> GetPointsInRange(Coordinate origin, double latRange, double lonRange)
        {
            var points = new List<Coordinate>();
            var rand = new Random();

            for (int i = 0; i < NumberOfCoins; i++)
            {
                points.Add(new Coordinate()
                {
                    Id = i,
                    Latitude = origin.Latitude + (2 * rand.NextDouble() - 1) * latRange,
                    Longitude = origin.Longitude + (2 * rand.NextDouble() - 1) * latRange
                });
            }

            return points;
        }
    }
}
