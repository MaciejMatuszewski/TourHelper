using TourHelper.Base.Model.Entity;
using TourHelper.Logic.Geolocation;
using TourHelper.Manager;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour {

    public Text text;
    public Transform arrow;

    private GpsManager gps;
    private CompassManager compass;
    private Coordinates target;
    private BasicRotationCalculator rot;

    private HaversineDistanceCalculator distance;

    private void Start()
    {
        
        gps = GpsManager.Instance;
        compass = CompassManager.Instance;
        
        StartCoroutine(gps.StartService(30));
        target = new Coordinates();
        target.Latitude = 52.46f;
        target.Longitude = 16.92f;

        rot = new BasicRotationCalculator(compass, gps);
        compass.Delay = 300;
        distance = new HaversineDistanceCalculator(gps, new MeanEarthRadius());
    }
    void Update () {

        rot.Transform(arrow, target);
        //arrow.transform.rotation = Quaternion.AngleAxis((float)rot.RotationAngle(target), new Vector3(0, 1 , 0));
        text.text = compass.GetAngleToNorth().ToString()+ "\nBearing:"+ rot.Bearing(target).ToString()
            + "\nRotation:"+ rot.RotationAngle(target).ToString() + "\nBase:"+ gps.GetCoordinates().Latitude.ToString()+","
            +  gps.GetCoordinates().Longitude.ToString()+ "\ntarget:" +target.Latitude.ToString()+','+target.Longitude.ToString()+"\n---end---";

    }
}
