using System;
using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Manager.Devices.Mock
{
    public class MockGpsManager : IGpsManager
    {
        private Coordinates position;

        public MockGpsManager()
        {
            position = new Coordinates();
            position.Latitude = 16.921937f;
            position.Longitude = 52.463563f;
        }
        public Coordinates GetCoordinates()
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
