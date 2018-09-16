using System;
using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Manager.Devices.Mock
{
    public class MockGpsManager : IGpsManager
    {
        private Coordinate position;

        public MockGpsManager()
        {
            position = new Coordinate();
            position.Latitude = 52.46374f;
            position.Longitude = 16.92118f;
        }
        public Coordinate GetCoordinates()
        {
            return position;
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
