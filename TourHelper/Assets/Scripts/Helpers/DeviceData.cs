using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;


public class DeviceData : MonoBehaviour
{
    public InputField _path;
    private string _logFile;
    public bool isOn;
    private AccelerationManager acc;
    private GyroManager g;
    private GpsManager gps;
    private int counter = 0;

    private void Awake()
    {
        
        g = GyroManager.Instance;
        StartCoroutine(g.StartService(2));
        acc = AccelerationManager.Instance;
        StartCoroutine(acc.StartService(2));
        gps = GpsManager.Instance;
        StartCoroutine(gps.StartService(2));
       

    }



    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            logToFile(Application.persistentDataPath + '/' + _logFile);
        }

    }

    public void OnStartButtonClick(Text t)
    {

        if (!isOn)
        {
            isOn = true;
            t.text = Input.accelerationEventCount.ToString();
            counter++;

            _logFile= string.Format("DataLog{0}.txt", counter);
            _path.text = _logFile;



        }
        else
        {
            isOn = false;
            t.text = "START";
        }
    }

    public void logToFile(string _logPath)
    {
        using (StreamWriter s = File.Exists(_logPath) ? File.AppendText(_logPath) : File.CreateText(_logPath))
        {

            Vector3 _acc;
            Quaternion _rot;
            Coordinate _coor;
            double dt = Time.deltaTime;

            _acc = g.GetFusedAccelerations();
            _rot = g.GetRotation();
            _coor = gps.GetCoordinates();

            s.Write(dt.ToString() + "|");
            s.Write(_acc.x.ToString() + ";" + _acc.y.ToString() +";"+ _acc.z.ToString() +"|");
            s.Write(_rot.x.ToString() + ";" + _rot.y.ToString() + ";" + _rot.z.ToString() + ";" + _rot.w.ToString() + "|");
            s.Write(_coor.Latitude.ToString() + ";" + _coor.Longitude.ToString() + "|");
            s.Write(_coor.VerticalAccuracy+ "|");
            s.Write("\n");
            s.Close();
        }
    }
}
