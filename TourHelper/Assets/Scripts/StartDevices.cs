using System.Collections;
using System.Collections.Generic;
using TourHelper.Manager;
using TourHelper.Manager.Devices;
using UnityEngine;

public class StartDevices : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //var Camera = CameraManager.Instance;
        //StartCoroutine(Camera.StartService(1));

        var _gps = GpsManager.Instance;
        StartCoroutine(_gps.StartService(5));

        var _gyro = GyroManager.Instance;
        StartCoroutine(_gyro.StartService(1));

        var _compass = CompassManager.Instance;
        StartCoroutine(_compass.StartService(1));

        DontDestroyOnLoad(gameObject);
    }
	

}
