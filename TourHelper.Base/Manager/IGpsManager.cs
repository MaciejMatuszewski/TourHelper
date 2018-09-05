using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Manager
{
    public interface IGpsManager:IBaseDeviceManager
    {
        
        Coordinates GetCoordinates();

    }
}
