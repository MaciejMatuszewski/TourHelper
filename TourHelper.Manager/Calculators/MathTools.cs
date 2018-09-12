using System;

namespace TourHelper.Manager.Calculators
{
    public class MathTools
    {
        public static double rad(double deg)
        {
            return 0.00555555555555556 * deg * Math.PI;
        }

        public static double deg(double rad)
        {
            return rad * 180 / Math.PI;
        }
        
    }
}
