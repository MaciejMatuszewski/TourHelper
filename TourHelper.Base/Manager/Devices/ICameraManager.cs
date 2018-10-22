using UnityEngine;

namespace TourHelper.Base.Manager.Devices
{
    public interface ICameraManager:IBaseDeviceManager
    {
        WebCamTexture GetScreen();
    }
}
