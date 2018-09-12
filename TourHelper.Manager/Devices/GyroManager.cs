﻿using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager;
using UnityEngine;

namespace TourHelper.Manager.Devices
{

    public class GyroManager : IGyroManager
    {
        private ServiceStatus status;

        private static GyroManager instance = null;
        private static readonly object key = new object();

        public static GyroManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new GyroManager();
                        }
                    }
                }
                return instance;
            }
        }

        public Quaternion GetRotation()
        {
            return Input.gyro.attitude;
        }

        public Vector3 GetRotationRate()
        {
            return Input.gyro.rotationRate;
        }

        public bool IsEnabled()
        {
            return Input.gyro.enabled;
        }

        public bool IsReady()
        {
            return Status() == ServiceStatus.Running && IsEnabled();
        }

        public IEnumerator StartService(int timeOut)
        {
            if (SystemInfo.supportsGyroscope)
            {

                Input.gyro.enabled = true;
                status = ServiceStatus.Running;
                yield break;
            }
            status = ServiceStatus.Initializing;
            yield return new WaitForSeconds(1);
            timeOut--;
            Debug.Log(timeOut);
            if (timeOut < 0)
            {
                Input.gyro.enabled = false;
                status = ServiceStatus.Failed;
                yield break;
            }
        }

        public ServiceStatus Status()
        {
            if (SystemInfo.supportsGyroscope && (!Input.gyro.enabled))
            {
                return ServiceStatus.Stopped;
            }
            return status;
        }
    }
}