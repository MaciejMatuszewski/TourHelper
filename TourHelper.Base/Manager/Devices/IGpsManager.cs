using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Manager.Devices
{
    public interface IGpsManager:IBaseDeviceManager
    {
        
        Coordinate GetCoordinates();

    }
}
