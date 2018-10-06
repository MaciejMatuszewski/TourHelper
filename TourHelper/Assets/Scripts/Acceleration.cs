using System;
using System.Collections;
using System.Collections.Generic;
using TourHelper.Base.Model.Entity;
using TourHelper.Logic.PositionLogic;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Calculators.Kalman;
using TourHelper.Manager.Calculators.MatrixTools;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class Acceleration : MonoBehaviour
{
    private GameObject cameraContainer;
    public Text text;
    private AccelerationManager acc;
    private GyroManager g;
    private Coordinates origin;
    private KalmanFilter f ;
    private Vector3 v;
    private GpsManager gps;
    private LocalPosition p;

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


        //TRANSLATOR WSPOLRZEDNYCH
        origin = new Coordinates();
        origin.Latitude = 52.463907f;
        origin.Longitude = 16.920955f;

        UTMLocalCoordinates t = new UTMLocalCoordinates(origin);
        
        //FILTR
        f = new KalmanFilter();
        f.GPSError = 10;
        f.AccelerationError = 1;

        p = new LocalPosition(gps, acc, f, t, g);
        

    }
    // Update is called once per frame
    void Update()
    {

        p.Filter.DeltaTime = Time.deltaTime;

        v=p.GetPosition();

        text.text = v.x.ToString() + "," + v.y.ToString() + "," + v.z.ToString();
    }
}
