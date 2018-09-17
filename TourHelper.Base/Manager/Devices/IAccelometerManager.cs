using UnityEngine;

namespace TourHelper.Base.Manager.Devices
{
    public interface IAccelometerManager : IBaseDeviceManager
    {
        Vector3 GetAcceleration();
        AccelerationEvent[] GetAccelerationEvents();
    }
}
