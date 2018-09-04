using System.Collections;
using TourHelper.Base.Enum;

namespace TourHelper.Base.Manager
{
    public interface IBaseDeviceManager
    {


        IEnumerator StartService(int timeOut);
        
        ServiceStatus Status();

        bool IsEnabled();

        bool IsReady();
    }
}
