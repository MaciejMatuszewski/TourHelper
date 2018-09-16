
using UnityEngine;

namespace TourHelper.Base.Manager.Devices
{
    public interface IGyroManager:IBaseDeviceManager

    {
        Quaternion GetRotation();
        Vector3 GetRotationRate();
        Vector3 GetGravity();
        void SetUpdateInterval(float interval);
        float GetUpdateInterval();
    }
}
