using System;
using System.Collections;
using System.Collections.Generic;
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

    private KalmanFilter f ;

    // Use this for initialization
    void Start()
    {
        g = GyroManager.Instance;
        StartCoroutine(g.StartService(2));
        acc = AccelerationManager.Instance;
        StartCoroutine(acc.StartService(2));

        f = new KalmanFilter();

        

    }
    // Update is called once per frame
    void Update()
    {

        Vector3 a;

        a = g.GetRotation()*acc.GetAcceleration();

        text.text = a.x.ToString() + "," + a.y.ToString() + "," + a.z.ToString()+"\n";
    }
}
