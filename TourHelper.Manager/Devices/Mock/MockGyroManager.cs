using System;
using System.Collections;
using System.Collections.Generic;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager.Devices;
using UnityEngine;

namespace TourHelper.Manager.Devices.Mock
{
    public class MockGyroManager : IGyroManager
    {

        private IEnumerable<Quaternion> _dataRot;
        private IEnumerable<Vector3> _dataAcc;
        private IEnumerator _enumeratorR;
        private IEnumerator _enumeratorA;
        private Quaternion _stedyStateR;
        private Vector3 _stedyStateA;

        public MockGyroManager(IEnumerable<Quaternion> rotation, IEnumerable<Vector3> fusedAcceleration)
        {
            _dataRot = rotation;
            _enumeratorR = _dataRot.GetEnumerator();
            _stedyStateR= new Quaternion();

            _dataAcc = fusedAcceleration;
            _enumeratorA = _dataAcc.GetEnumerator();
            _stedyStateA = new Vector3();
        }

        public MockGyroManager(DevicesFromFile data)
        {
            data.ReadData();
            _dataRot = data.Rotation;
            _enumeratorR = _dataRot.GetEnumerator();
            _stedyStateR = new Quaternion();

            _dataAcc = data.Accelerations;
            _enumeratorA = _dataAcc.GetEnumerator();
            _stedyStateA = new Vector3();
        }

        public Vector3 GetFusedAccelerations()
        {
            //Debug.Log("Acc:" + (countA++));
            if (_enumeratorA.MoveNext())
            {
                return (Vector3)_enumeratorA.Current;
            }
            return _stedyStateA;
        }

        public Vector3 GetGravity()
        {
            throw new NotImplementedException();
        }

        public Quaternion GetRotation()
        {
            //Debug.Log("Rot:" + (countR++));
            if (_enumeratorR.MoveNext())
            {
                return (Quaternion)_enumeratorR.Current;
            }
            return _stedyStateR;
        }

        public double[,] GetRotationMatrix()
        {
            throw new NotImplementedException();
        }

        public Vector3 GetRotationRate()
        {
            throw new NotImplementedException();
        }

        public float GetUpdateInterval()
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

        public void SetUpdateInterval(float interval)
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
