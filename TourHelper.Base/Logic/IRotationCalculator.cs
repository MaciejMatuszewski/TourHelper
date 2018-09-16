using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Logic
{
    public interface IRotationCalculator
    {
        double Bearing(Coordinate coor);
        double RotationAngle(Coordinate coor);
        void Transform(Transform obj, Coordinate coor);
    }
}
