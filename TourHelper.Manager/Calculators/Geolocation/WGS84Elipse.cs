using System;
using TourHelper.Base.Manager.Calculators;

namespace TourHelper.Manager.Calculators
{
    class WGS84Elipse : IElipsoid
    {
        public float GetEccentricity()
        {
            float powA, powB;

            powA = (float)Math.Pow(GetSemiMajor(), 2);
            powB = (float)Math.Pow(GetSemiMinor(), 2);
    
            return (powA- powB) /powA;
        }

        public float GetSemiMajor()
        {
            return 6378137.000f;
        }

        public float GetSemiMinor()
        {
            return 6356752.3141f;
        }

        public float GetSqrtEccentricity()
        {
            return (float)Math.Sqrt(GetEccentricity());
        }
    }
}
