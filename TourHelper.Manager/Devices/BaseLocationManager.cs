using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager;
using UnityEngine;

namespace TourHelper.Manager
{
    public abstract class BaseLocationManager : IBaseDeviceManager
    {
 
        virtual public bool IsEnabled()
        {
            return Input.location.isEnabledByUser;
        }

        virtual public bool IsReady()
        {
            if ((Input.location.status != LocationServiceStatus.Running))
            {
                return false;
            }
            return true;
        }

        public IEnumerator StartService(int timeOut)
        {
            //nalezy zastosowac corutine
            if (!IsEnabled())
            {
                Debug.Log("Location disabled by user");
                yield break;
            }
            
            Input.location.Start();

            while (Status() == ServiceStatus.Initializing && timeOut > 0)
            {
                yield return new WaitForSeconds(1);
                timeOut--;
            }
            if (timeOut <= 0)
            {
                Debug.Log("Time out");
                yield break;
            }
            if (Status() == ServiceStatus.Failed)
            {
                Debug.Log("Unable to get location");
                yield break;
            }
        }

        public ServiceStatus Status()
        {
            return (ServiceStatus)Input.location.status;
        }
    }
}
