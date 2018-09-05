using System;
using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager;

namespace TourHelper.Manager.Devices.Mock
{
    public class MockCompassManager : ICompassManager
    {
        public double Heading { get; set; }

        public void CompassOff()
        {
            throw new NotImplementedException();
        }

        public void CompassOn()
        {
            throw new NotImplementedException();
        }

        public double GetAngleToNorth()
        {
            return Heading;
        }

        public bool IsEnabled()
        {
            throw new NotImplementedException();
        }

        public bool IsReady()
        {
            throw new NotImplementedException();
        }

        public IEnumerator StartService(int timeOut)
        {
            throw new NotImplementedException();
        }

        public ServiceStatus Status()
        {
            throw new NotImplementedException();
        }
    }
}
