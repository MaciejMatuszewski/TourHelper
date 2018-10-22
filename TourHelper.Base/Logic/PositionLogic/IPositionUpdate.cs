
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Manager.Calculators.Kalman;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Logic.PositionLogic
{
    public interface IPositionUpdate
    {
        Coordinate Origin { get; set; }
        Vector3 GetPosition();
        bool isStanding();
    }
}
