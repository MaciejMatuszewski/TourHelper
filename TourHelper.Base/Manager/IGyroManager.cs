
using UnityEngine;

namespace TourHelper.Base.Manager
{
    public interface IGyroManager:IBaseDeviceManager

    {
        Quaternion GetRotation();
        Vector3 GetRotationRate();
    }
}
