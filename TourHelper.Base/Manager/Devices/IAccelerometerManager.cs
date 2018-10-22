using UnityEngine;

namespace TourHelper.Base.Manager.Devices
{
    public interface IAccelerometerManager : IBaseDeviceManager
    {
        Vector3 GetAcceleration();
        AccelerationEvent[] GetAccelerationEvents();
    }
}
