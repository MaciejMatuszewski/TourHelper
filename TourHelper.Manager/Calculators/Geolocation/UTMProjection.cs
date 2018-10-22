using System;
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Manager.Calculators
{
    public class UTMProjection:IProjection
    {

        public float ScaleFactor(Coordinate c)
        {
              return 0.9996f;
        }

        public float EastingOfTrueOrigin(Coordinate c)
        {
                return 500000;
        }
        public float LatOfTrueOrigin(Coordinate c)
        {
            return 0;
        }

        public float LonOfTrueOrigin(Coordinate c)
        {
            return 3 + (float)Math.Floor(c.Longitude / 6) * 6;
        }

        public float NorthingOfTrueOrigin(Coordinate c)
        {
            if (c.Latitude < 0)
            {
                return 10000000;
            }
            return 0;
        }
    }
}
