using System;
using System.Collections;
using System.Collections.Generic;

using TourHelper.Base.Enum;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Manager.Devices.Mock
{
    public class MockGpsManager : IGpsManager
    {

        private IEnumerable<Coordinates> _data;
        private IEnumerator _enumerator;
        private int count=0;
        private Coordinates _buffored;
        public MockGpsManager(IEnumerable<Coordinates> data)
        {
            _data = data;
            _enumerator = _data.GetEnumerator();

        }

        public MockGpsManager(DevicesFromFile data)
        {
            _data = data.Position;
            _enumerator = _data.GetEnumerator();

        }

        public Coordinates GetCoordinates()
        {
            //Debug.Log("GPS:"+ (count++));
            if (_enumerator.MoveNext())
            {
                _buffored = (Coordinates)_enumerator.Current;

            }

            return _buffored;
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
