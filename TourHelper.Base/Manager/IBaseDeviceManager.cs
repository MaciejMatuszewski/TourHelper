using System.Collections;
using TourHelper.Base.Enum;

namespace TourHelper.Base.Manager
{
    public interface IBaseDeviceManager
    {


        IEnumerable StartService(int timeOut);
        
        ServiceStatus Status();

        bool IsReady();
    }
}
