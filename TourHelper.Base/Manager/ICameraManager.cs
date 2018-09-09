using UnityEngine;

namespace TourHelper.Base.Manager
{
    public interface ICameraManager:IBaseDeviceManager
    {
        WebCamTexture GetScreen();
    }
}
