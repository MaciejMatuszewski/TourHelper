using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager.Devices;
using UnityEngine;

namespace TourHelper.Manager.Devices.Mock
{
    public class MockAccelerometrManager : IAccelerometerManager
    {
        private IEnumerable<Vector3> _data;
        private IEnumerator _enumerator;
        private Vector3 _stedyState;

        public MockAccelerometrManager(IEnumerable<Vector3> data)
        {
            _data = data;
            _enumerator = _data.GetEnumerator();
            _stedyState = new Vector3();
        }

        public Vector3 GetAcceleration()
        {
            if (_enumerator.MoveNext())
            {
                return (Vector3)_enumerator.Current;
            }
            return _stedyState;
                
        }

        public AccelerationEvent[] GetAccelerationEvents()
        {
            throw new NotImplementedException();
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
