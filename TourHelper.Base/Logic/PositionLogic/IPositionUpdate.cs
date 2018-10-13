
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Manager.Calculators.Kalman;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Logic.PositionLogic
{
    public interface IPositionUpdate
    {
        IGyroManager Gyro { get; set; }
        IGpsManager Gps { get; set; }

        IKalman Filter { get; set; }
        Coordinates Origin { get; set; }
        IPositionTranslation Translator { get; set; }

        Vector3 GetPosition();
        bool isStanding();
    }
}
