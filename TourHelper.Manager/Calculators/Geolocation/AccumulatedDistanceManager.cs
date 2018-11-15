using System;
using TourHelper.Base.Manager.Calculators;
using UnityEngine;

namespace TourHelper.Manager.Calculators.Geolocation
{
    public class AccumulatedDistanceManager : IDistanceManager
    {
        private double _accumulatedDistance;
        private Vector3 _lastPoint;

        public AccumulatedDistanceManager(Vector3 startPoint)
        {
            _accumulatedDistance = 0;
            _lastPoint = startPoint;
        }

        public double GetAccumulatedDistance(Vector3 point)
        {
            double dx2 = Math.Pow((_lastPoint.x - point.x),2);
            double dy2 = Math.Pow((_lastPoint.y - point.y), 2);
            double dz2 = Math.Pow((_lastPoint.z - point.z), 2);

            double res = Math.Sqrt(dx2+ dy2+ dz2);
            _accumulatedDistance += res;
            _lastPoint = point;
            return _accumulatedDistance;
        }
    }
}
