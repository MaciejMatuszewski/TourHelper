using TourHelper.Base.Model.Entity;
using TourHelper.Logic.Geolocation;
using TourHelper.Manager;
using TourHelper.Manager.Devices;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour {

 
    public Transform targetArrow;
    public Transform compassArrow;
    public int scale=1;

    private int lastPoint;
    private GpsManager gps;
    private Coordinate target;
    private BasicRotationCalculator rot;
    private GyroManager _gyro;
    private Vector3 angle;

    private void Start()
    {
        _gyro = GyroManager.Instance;
        gps = GpsManager.Instance;

        target = new Coordinate();
        target.Latitude = 0; 
        target.Longitude = 0;

        rot = new BasicRotationCalculator(_gyro, gps);
        rot.Scale = 1;

    }
    void LateUpdate () {

        InitDestination();
        rot.Transform(targetArrow, target);
        NorthTransformation();
    }

    private void NorthTransformation()
    {
        compassArrow.transform.localRotation = Quaternion.Euler(0, -(float)Heading(), 0);// Quaternion.Slerp(start, end, Time.deltaTime * scale);
    }

    private double Heading()
    {
        return 360 - (_gyro.GetRotation()).eulerAngles.z;
    }

    public void InitDestination()
    {
        int pointId = PlayerPrefs.GetInt("PointId", 1);
        if (lastPoint!= pointId)
        {
            var coorRepo = new CoordinateRepository();

            var tourPointPosition = coorRepo.GetByTourPointID(pointId);

            if (tourPointPosition != null)
            {
                target = tourPointPosition;
                lastPoint = pointId;
            }
        }

    }
}
