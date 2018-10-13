using System;
using TourHelper.Base.Model.Entity;
using TourHelper.Logic.PositionLogic;
using TourHelper.Manager;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Calculators.Kalman;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class Workspace : MonoBehaviour
{

    
 
    private AccelerationManager acc;
    private GyroManager g;
    private Coordinates origin;
    private KalmanFilter f;
    private Vector3 v;
    private GpsManager gps;
    private LocalPosition p;
    private UTMLocalCoordinates tra;

    private Quaternion rotation;
    private GameObject cameraContainer;


    private void Awake()
    {
        g = GyroManager.Instance;
        StartCoroutine(g.StartService(2));
        acc = AccelerationManager.Instance;
        StartCoroutine(acc.StartService(2));
        gps = GpsManager.Instance;
        StartCoroutine(gps.StartService(2));
    }


    // Use this for initialization
    void Start()
    {


        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        cameraContainer.transform.rotation = Quaternion.Euler(90f, 180f, 0);

        rotation = new Quaternion(0, 0, 1, 0);


        origin = new Coordinates();
        origin.Latitude = 52.463907f;
        origin.Longitude = 16.920955f;

        tra = new UTMLocalCoordinates(origin);


        f = new KalmanFilter();
        f.GPSError = 1;
        f.AccelerationError = 1;

        p = new LocalPosition(gps, acc, f, tra, g);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (g.IsReady())
        {
            transform.localRotation = g.GetRotation() * rotation;
            if (gps.IsReady())
            {
                p.Filter.DeltaTime = Time.deltaTime;
                transform.position = p.GetPosition();

            }
        }
    }


}
