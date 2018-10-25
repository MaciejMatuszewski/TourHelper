using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager.Devices;
using UnityEngine;


namespace TourHelper.Manager.Devices
{


    public class CameraManager : ICameraManager
    {
        private WebCamTexture BackCam { get; set; }

        private bool CamAvailable { get; set; }
        private static CameraManager instance = null;
        private static readonly object key = new object();

        public static CameraManager Instance
        {

            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new CameraManager();
                            instance.CamAvailable = false;
                        }
                    }
                }
                return instance;
            }
        }

        public bool IsEnabled()
        {
            return instance.CamAvailable;
        }

        public bool IsReady()
        {
            return instance.CamAvailable && instance.BackCam.isPlaying;
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
                //Debug.Log(" Camera is ready ");
                yield break;
            }

            while ((BackCam == null) && timeOut > 0 && WebCamTexture.devices.Length == 0)
            {
                //Debug.Log("Camera unavailable");
                CamAvailable = false;
                yield return new WaitForSeconds(1);
                timeOut--;
            }

 
            if (timeOut <= 0)
            {
                yield break;
            }
            
            devices = WebCamTexture.devices;
            for (int i = 0; i < devices.Length; i++)
            {
                if (!devices[i].isFrontFacing)
                {

                    BackCam = new WebCamTexture(devices[i].name, Screen.height, Screen.width);
                }
            }
            //Debug.Log(BackCam.ToString());
            while ((BackCam == null) && timeOut > 0)
            {
                
                //Debug.Log(timeOut.ToString());
                yield return new WaitForSeconds(1);
                timeOut--;
            }
            if (timeOut <= 0)
            {
                //Debug.Log("Back camera unavailable");
                yield break;
            }
            
            instance.BackCam.Play();
            instance.CamAvailable = true;
            //Debug.Log(" Camera is ready to use ");
        }

        public WebCamTexture GetScreen()
        {
            if (instance.IsReady())
                return instance.BackCam;

            return new WebCamTexture(); //uzupelnic o default bacground

        }
    }
}
