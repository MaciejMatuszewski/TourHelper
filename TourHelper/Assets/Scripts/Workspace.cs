using System;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class Workspace : MonoBehaviour
{

    public Text debugText;

    private Quaternion rotation;
    private GameObject cameraContainer;
    private GyroManager gyroscope;
    private UTMLocalCoordinates translate;
    private Coordinates origin;
    private GpsManager gps;
    private DateTime updateTimeStamp;

    // Use this for initialization
    void Start()
    {
        gyroscope = GyroManager.Instance;
        StartCoroutine(gyroscope.StartService(3));

        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        cameraContainer.transform.rotation = Quaternion.Euler(90f, 180f, 0);

        rotation = new Quaternion(0, 0, 1, 0);

        //origin setup
        origin = new Coordinates();
        origin.Latitude = 52.463907f;
        origin.Longitude = 16.920955f;
        translate = new UTMLocalCoordinates(origin);
        //52.403412, 16.949333
        //GPS setup
        gps = GpsManager.Instance;
        StartCoroutine(gps.StartService(10));
        //init of position

        //updateTimeStamp = DateTime.Now;

    }

    // Update is called once per frame
    void Update()
    {
        if (gyroscope.IsReady())
        {
            transform.localRotation = gyroscope.GetRotation() * rotation;
            if (gps.IsReady())
            {
                transform.position = translate.GetCoordinates(gps.GetCoordinates());
                debugText.text = translate.GetCoordinates(gps.GetCoordinates()).x.ToString() + "," +
                translate.GetCoordinates(gps.GetCoordinates()).y.ToString() + "," +
                translate.GetCoordinates(gps.GetCoordinates()).z.ToString();
                //SmoothPositionChange();
            }
        }
    }

    private void SmoothPositionChange()
    {
        Vector3 start, end;
        float speed = 1f;
        float eps = 0.5f;
        float distance = 0.5f;

        start = transform.position;
        end = translate.GetCoordinates(gps.GetCoordinates());
        distance = Vector3.Distance(start, end);


        if (distance > eps)
        {
            TimeSpan diff = DateTime.Now.Subtract(updateTimeStamp);
            speed = distance / (float)diff.TotalSeconds;
            transform.position = Vector3.Lerp(start, end, speed);
            updateTimeStamp = DateTime.Now;
        }


    }

}
