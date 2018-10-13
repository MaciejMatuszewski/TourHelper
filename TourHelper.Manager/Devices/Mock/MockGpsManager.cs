using System;
using System.Collections;
using System.Collections.Generic;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Manager.Devices.Mock
{
    public class MockGpsManager : IGpsManager
    {

        private IEnumerable<Coordinates> _data;
        private IEnumerator _enumerator;


        public MockGpsManager(IEnumerable<Coordinates> data)
        {
            _data = data;
            _enumerator = _data.GetEnumerator();

        }

        public Coordinates GetCoordinates()
        {
            _enumerator.MoveNext();
            return (Coordinates)_enumerator.Current;
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
