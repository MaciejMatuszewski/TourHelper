using System;
using System.Collections;
using System.Collections.Generic;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class Acceleration : MonoBehaviour
{
    private GameObject cameraContainer;
    public Text text;
    private AccelerationManager acc;
    private GyroManager g;
    private IMUFilter f ;
    private double[,] q;
    // Use this for initialization
    void Start()
    {
        g = GyroManager.Instance;
        StartCoroutine(g.StartService(2));
        acc = AccelerationManager.Instance;
        StartCoroutine(acc.StartService(2));

        f = new IMUFilter();

        f.Accelometer = acc;
        f.Gyroscope = g;

        f.Sampling = 512f;
        f.Beta = 0.1f;


        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        cameraContainer.transform.rotation = Quaternion.Euler(90f, 180f, 0);
    }
    // Update is called once per frame
    void Update()
    {
        f.Sampling = (int)(1/Time.deltaTime);
        f.UpdateFilter();
        q = f.GetRotationMatrix();

        double[] c = { 0, 0, 0 };
        double[] b = { 0,0,1};

        for (int i = 0; i < 3; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                c[i] += q[i, k] * b[k];
            }
        }
       // text.text = f.GetOrientation().x.ToString() + "," + f.GetOrientation().y.ToString() + "," + f.GetOrientation().z.ToString()+ "," + f.GetOrientation().w.ToString() + "\n";
          


        
                text.text = g.GetRotation().x.ToString() + "," + g.GetRotation().y.ToString() + "," + g.GetRotation().z.ToString() + "," + g.GetRotation().w.ToString() + "\n" +
                    f.GetOrientation().x.ToString() + "," + f.GetOrientation().y.ToString() + "," + f.GetOrientation().z.ToString() + "," + f.GetOrientation().w.ToString() + "\n";
                    
    }
}
