﻿using System;
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

    //private GameObject cameraContainer;
    public InputField _lat,_lon,_gpsErr,_accErr,_path;
    private AccelerationManager acc;
    private GyroManager g;
    private Coordinate origin;
    private KalmanFilter f ;
    private Vector3 v;
    private GpsManager gps;
    private LocalPosition p;
    private UTMLocalCoordinates tra;

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

            origin = new Coordinate();
            origin.Latitude = (float)Convert.ToDouble(_lat.text);
            origin.Longitude = (float)Convert.ToDouble(_lon.text);

            tra = new UTMLocalCoordinates(origin);


            f = new KalmanFilter();
            f.GPSError = Convert.ToDouble(_gpsErr.text); 
            f.AccelerationError = Convert.ToDouble(_accErr.text);

            p = new LocalPosition(gps, g, f, tra);
            p.LogPath = Application.persistentDataPath +'/' +_path.text;

        }
        else
        {
            isOn = false;
            t.text = "START";
        }
    }
}
