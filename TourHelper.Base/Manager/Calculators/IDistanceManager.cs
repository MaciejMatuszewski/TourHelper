
using UnityEngine;

namespace TourHelper.Base.Manager.Calculators
{
    public interface IDistanceManager
    {
        double GetAccumulatedDistance(Vector3 point);
    }
}
