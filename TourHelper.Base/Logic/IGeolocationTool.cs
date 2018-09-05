using TourHelper.Base.Model.Entity;
using UnityEngine;


namespace TourHelper.Base.Logic
{
    interface IGeolocationTool
    {
        double Bearing(Coordinates coor);
        double Distance(Coordinates coor);
        double RotationAngle(Coordinates coor);
        void Transform(Transform obj);
    }
}
