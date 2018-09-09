using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager;
using UnityEngine;


namespace TourHelper.Manager.Devices
{


    public class CameraManager : ICameraManager
    {
        private WebCamTexture BackCam { get; set; }

        private bool CamAvailable { get; set; }
        private static CameraManager instance=null;
        private static readonly object key = new object();

        public static CameraManager Instance {

            get
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        instance = new CameraManager();
                    }
                    return instance;
                }
            }
        }

        public bool IsEnabled()
        {
            return CamAvailable;
        }

        public bool IsReady()
        {
            return CamAvailable&&BackCam.isPlaying;
        }

        public ServiceStatus Status()
        {
            if (IsReady())
            {
                return ServiceStatus.Running;
            }
            if (CamAvailable)
            {
                return ServiceStatus.Stopped;
            }
            return ServiceStatus.Failed;
        }

        public IEnumerator StartService(int timeOut)
        {
            WebCamDevice[] devices;

            if (IsReady())
            {
                yield break;
            }

            while (timeOut > 0)
            {
                devices = WebCamTexture.devices;

                if (devices.Length == 0)
                {
                    Debug.Log("Camera unavailable");
                    CamAvailable = false;
                    yield return new WaitForSeconds(1);
                    timeOut--;
                }
                for (int i = 0; i < devices.Length; i++)
                {
                    if (!devices[i].isFrontFacing)
                    {
                        BackCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);

                    }
                }
                if (BackCam == null)
                {
                    Debug.Log("Back camera unavailable");
                    yield return new WaitForSeconds(1);
                    timeOut--;
                }
            }
            BackCam.Play();
            CamAvailable = true;
        }

        public WebCamTexture GetScreen()
        {
            return BackCam;
        }
    }
}
