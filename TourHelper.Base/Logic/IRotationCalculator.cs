using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Logic
{
    public interface IRotationCalculator
    {
        double Bearing(Coordinates coor);
        double RotationAngle(Coordinates coor);
        void Transform(Transform obj);
    }
}
