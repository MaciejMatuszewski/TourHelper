using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager.Devices;
using UnityEngine;

namespace TourHelper.Manager.Devices
{
    public class AccelerationManager : IAccelerometerManager
    {

        private ServiceStatus status;

        private static AccelerationManager instance = null;
        private static readonly object key = new object();

        public static AccelerationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new AccelerationManager();
                        }
                    }
                }
                return instance;
            }
        }

        public Vector3 GetAcceleration()
        {
            return Input.acceleration;
        }

        public bool IsEnabled()
        {
            if (SystemInfo.supportsAccelerometer)
            {
                return true;
            }
            return false;
        }

        public bool IsReady()
        {
            if (SystemInfo.supportsAccelerometer)
            {
                return true;
            }
            return false;
        }

        public IEnumerator StartService(int timeOut)
        {
            if (SystemInfo.supportsAccelerometer)
            {
                status = ServiceStatus.Running;
                yield break;
            }
            status = ServiceStatus.Initializing;
            yield return new WaitForSeconds(1);
            timeOut--;
            //Debug.Log(timeOut);
            if (timeOut < 0)
            {
                status = ServiceStatus.Failed;
                yield break;
            }
        }

        public ServiceStatus Status()
        {

            if (SystemInfo.supportsAccelerometer)
            {
                return ServiceStatus.Running;
            }
            return status;
        }

        public AccelerationEvent[] GetAccelerationEvents()
        {
            return Input.accelerationEvents;
        }
    }
}
