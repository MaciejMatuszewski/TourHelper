using TourHelper.Base.Model.Entity;
using TourHelper.Logic.Geolocation;
using TourHelper.Manager;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour {

    public Text text;
    public Transform targetArrow;
    public Transform compassArrow;
    public int scale=1;


    private GpsManager gps;
    private CompassManager compass;
    private Coordinates target;
    private BasicRotationCalculator rot;
    private GyroManager g;
    //private HaversineDistanceCalculator distance;

    private void Start()
    {
        
        gps = GpsManager.Instance;

        compass = CompassManager.Instance;
        compass.Delay = 1000;
        compass.Precision = 2;
        compass.MaxChange = 5d;
        //52.463661, 16.921885

        StartCoroutine(gps.StartService(30));
        target = new Coordinates();
        target.Latitude = 52.463661f; 
        target.Longitude = 16.921885f;

        rot = new BasicRotationCalculator(compass, gps);
        rot.Scale = scale;

        //distance = new HaversineDistanceCalculator(gps, new MeanEarthRadius());
    }
    void Update () {
        
        rot.Transform(targetArrow, target);

        NorthTransformation();
        /*
        
        text.text = compass.GetAngleToNorth().ToString()+ "\nBearing:"+ rot.Bearing(target).ToString()
            + "\nDistance:" + distance.Distance(target).ToString()
            + "\nRotation:"+ rot.RotationAngle(target).ToString() + "\nBase:"+ gps.GetCoordinates().Latitude.ToString()+","
            +  gps.GetCoordinates().Longitude.ToString()+ "\ntarget:" +target.Latitude.ToString()+','+target.Longitude.ToString()+"\n---end---";
            */
        text.text = Input.gyro.gravity.x.ToString() + "," + Input.gyro.gravity.y.ToString() + "," +
            Input.gyro.gravity.z.ToString();
    }

    private void NorthTransformation()
    {
        Quaternion start = compassArrow.transform.localRotation;
        Quaternion end = Quaternion.AngleAxis(-(float)compass.GetAngleToNorth(), new Vector3(0, 1, 0));

        compassArrow.transform.localRotation = Quaternion.Slerp(start, end, Time.deltaTime * scale);
    }
}
