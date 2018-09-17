using System;
using System.Collections;
using System.Collections.Generic;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class Acceleration : MonoBehaviour
{
    public Text text;
    private AccelerationManager acc;
    private GyroManager g;
    private Vector3 v;
    private Vector3 grav;
    private Vector3 res;
    DateTime ttt;
    SignalIntegral integ;
    // Use this for initialization
    void Start()
    {
        g = GyroManager.Instance;
        StartCoroutine(g.StartService(2));
        acc = AccelerationManager.Instance;
        StartCoroutine(acc.StartService(2));
        integ=new SignalIntegral();
    }
    // Update is called once per frame
    void Update()
    {
        v = acc.GetAcceleration() * 9.81f;

        text.text = v.x.ToString() + "," + v.y.ToString() + "," +
            v.z.ToString();

        grav=g.GetGravity()*9.81f;
        text.text += "\n"+ grav.x.ToString() + "," + grav.y.ToString() + "," +
             grav.z.ToString();

        res = (v - grav);
        ttt = DateTime.Now;
        integ.UpdateResult(res.x, ttt);
        /*if ((grav.x!=0) && (grav.y!=0) &&(grav.z != 0))
        {
            res.x = res.x / grav.x * 100;
            res.y = res.y / grav.y * 100;
            res.z = res.z / grav.z * 100;
        }*/
        text.text += "\n" + integ.GetResult().ToString();
    }
}
