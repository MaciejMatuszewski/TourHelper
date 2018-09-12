using System.Collections;
using System.Collections.Generic;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class Workspace : MonoBehaviour {


    private Quaternion rotation;
    private GameObject cameraContainer;
    private GyroManager gyroscope;

	// Use this for initialization
	void Start () {
        gyroscope = GyroManager.Instance;
        StartCoroutine(gyroscope.StartService(3));

        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        cameraContainer.transform.rotation = Quaternion.Euler(90f, 0, 0);
        rotation = new Quaternion(0, 0, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (gyroscope.IsReady())
        {
            transform.localRotation=gyroscope.GetRotation()*rotation;
        }
	}

}
