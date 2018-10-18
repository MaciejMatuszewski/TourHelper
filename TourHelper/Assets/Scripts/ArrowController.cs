using TourHelper.Base.Model.Entity;
using TourHelper.Logic.Geolocation;
using TourHelper.Manager;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour {

    public Transform container;
    public Transform targetArrow;
    public Transform compassArrow;
    public int scale=1;


    private GpsManager gps;
    private CompassManager compass;
    private Coordinates target;
    private BasicRotationCalculator rot;
    private GyroManager _gyro;
    private Vector3 angle;

    private void Start()
    {
        _gyro = GyroManager.Instance;
        gps = GpsManager.Instance;
        compass = CompassManager.Instance;

        compass.Delay = 100;
        compass.Precision = 2;
        compass.MaxChange = 5d;

        target = new Coordinates();
        target.Latitude = 52.463661f; 
        target.Longitude = 16.921885f;

        rot = new BasicRotationCalculator(compass, gps);
        rot.Scale = scale;

    }
    void Update () {

        
        rot.Transform(targetArrow, target);
        NorthTransformation();
    }

    private void NorthTransformation()
    {
        Quaternion start = compassArrow.transform.localRotation;
        Quaternion end = Quaternion.AngleAxis(-(float)compass.GetAngleToNorth(), new Vector3(0, 1, 0));

        // compassArrow.transform.localRotation =Quaternion.Slerp(start, end, Time.deltaTime * scale);
        
        compassArrow.transform.localRotation=Quaternion.AngleAxis(_gyro.GetRotation().eulerAngles.x,Vector3.up);
    }
}
