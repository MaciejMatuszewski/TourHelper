using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public bool isOn;

    private GameObject cameraContainer;
    public InputField _lat,_lon,_gpsErr,_accErr,_path;
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

        

        _lat.text = "52.463907";
        _lon.text = "16.920955";
        _gpsErr.text = "1.0";
        _accErr.text = "1.0";
        _path.text = "Log1.txt";

        //TRANSLATOR WSPOLRZEDNYCH
        origin = new Coordinates();
        origin.Latitude = 52.463907f;
        origin.Longitude = 16.920955f;

        UTMLocalCoordinates t = new UTMLocalCoordinates(origin);
        
        //FILTR
        f = new KalmanFilter();
        f.GPSError = 5;
        f.AccelerationError = 0.5;

        p = new LocalPosition(gps, acc, f, t, g);
        p.LogPath = Application.persistentDataPath +"/PositionLog.txt";
        Debug.Log(p.LogPath);

    }
    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            p.Filter.DeltaTime = Time.deltaTime;

            v = p.GetPosition();
        }


       // text.text = v.x.ToString() + "," + v.y.ToString() + "," + v.z.ToString();
        //text.text = p.Filter.InitialPosition.GetByIndex(0,0)+" "+p.Filter.InitialPosition.GetByIndex(1, 0);
        

    }


    public void OnStartButtonClick(Text t)
    {
        if (!isOn)
        {
            isOn = true;
            t.text = "STOP";
            origin = new Coordinates();
            origin.Latitude = (float)Convert.ToDouble(_lat.text);
            origin.Longitude =(float)Convert.ToDouble(_lon.text);
            p.Origin = origin;
            p.Filter.GPSError = Convert.ToDouble(_gpsErr.text);
            p.Filter.AccelerationError = Convert.ToDouble(_accErr.text);

            p.LogPath = Application.persistentDataPath + _path.text;
        }
        else
        {
            isOn = false;
            t.text = "START";
        }
    }
}
