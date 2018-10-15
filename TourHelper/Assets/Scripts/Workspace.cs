using System;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using TourHelper.Logic.PositionLogic;
using TourHelper.Manager;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Calculators.Kalman;
using TourHelper.Manager.Devices;
using TourHelper.Manager.Devices.Mock;
using UnityEngine;
using UnityEngine.UI;

public class Workspace : MonoBehaviour
{
    public int width;
    public int height;
    public int coefficient = 1;
    private IAccelerometerManager acc;
    private IGyroManager g;
    private IGyroManager g2;
    private Coordinates origin;
    private KalmanFilter f;
    private Vector3 v;
    private IGpsManager gps;
    private LocalPosition p;
    private UTMLocalCoordinates tra;

    private Quaternion rotation, rot;
    private GameObject cameraContainer;
    private DevicesFromFile data;
    private DevicesFromFile data2;




    private void Awake()
    {/*
        g = GyroManager.Instance;
        StartCoroutine(g.StartService(2));
        acc = AccelerationManager.Instance;
        StartCoroutine(acc.StartService(2));
        gps = GpsManager.Instance;
        StartCoroutine(gps.StartService(2));*/

        string _path = @"D:\Uczelnia\INŻYNIERKA\TESTY\Dane\zachods.txt";


        data = new DevicesFromFile() { FilePath = _path };
        data.ReadData();
        data2 = new DevicesFromFile() { FilePath = _path };
        data2.ReadData();

        g = new MockGyroManager(data.Rotation, data.Accelerations);
        g2 = new MockGyroManager(data2.Rotation, data2.Accelerations);

        gps = new MockGpsManager(data.Position);


        width = Screen.width;
        height =Screen.height;
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
        f.GPSError = 5;
        f.AccelerationError = 1;

        p = new LocalPosition(gps, g, f, tra);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if (g.IsReady())
        {
            transform.localRotation = g.GetRotation() * rotation;
            if (gps.IsReady())
            {
                p.Filter.DeltaTime = Time.deltaTime;
                transform.position = p.GetPosition();

            }
        }*/

        rot = g2.GetRotation();

       

        transform.localRotation = rot * rotation;
        p.Filter.DeltaTime = Time.deltaTime;
        cameraContainer.transform.position = p.GetPosition();


    }




}
