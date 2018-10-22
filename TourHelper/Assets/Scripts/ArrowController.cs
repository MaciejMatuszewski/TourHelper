using TourHelper.Base.Model.Entity;
using TourHelper.Logic.Geolocation;
using TourHelper.Manager;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour {

    public Text container;
    public Transform targetArrow;
    public Transform compassArrow;
    public int scale=1;


    private GpsManager gps;
    private CompassManager compass;
    private Coordinate target;
    private BasicRotationCalculator rot;
    private GyroManager _gyro;
    private Vector3 angle;

    private void Start()
    {
        _gyro = GyroManager.Instance;
        gps = GpsManager.Instance;
        compass = CompassManager.Instance;

        compass.Delay = 100;
        compass.Precision = 1;
        compass.MaxChange = 2d;

        target = new Coordinate();
        target.Latitude = 52.463661f; 
        target.Longitude = 16.921885f;

        rot = new BasicRotationCalculator(compass, gps);
        rot.Scale = scale;

    }
    void Update () {

        
        //rot.Transform(targetArrow, target);
        NorthTransformation();
    }

    private void NorthTransformation()
    {
        Quaternion start = compassArrow.transform.localRotation;
        Quaternion end = Quaternion.AngleAxis(-(float)compass.GetAngleToNorth(), new Vector3(0, 1, 0));
        
        compassArrow.transform.localRotation = Quaternion.Euler(0, -(float)compass.GetAngleToNorth(), 0);// Quaternion.Slerp(start, end, Time.deltaTime * scale);
        container.text = compass.GetAngleToNorth().ToString();
       // compassArrow.transform.localRotation = Quaternion.Euler(0, -Quaternion.Inverse(_gyro.GetRotation()).eulerAngles.z, 0);//; 
    }
}
