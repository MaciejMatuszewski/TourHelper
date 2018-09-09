using System.Collections;
using UnityEngine;

public class GpsService : MonoBehaviour {
    public static GpsService Instance { get; set; }
    public double longitude;
    public double latitude;
    public double altitude;
    public double verticalAccuracy;
    public double horizontalAccuracy;
    public int maxTime=15;
    private bool isReady = false;
    public double northDir;
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }



	// Use this for initialization
	private void Start () {
        Instance = this;
        DontDestroyOnLoad(gameObject);
		StartCoroutine(StartLocationService());
        
	}

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {

            Debug.Log("GPS disabled");
            yield break;
        }
        Input.compass.enabled = true;
        
        Input.location.Start();
        while (Input.location.status==LocationServiceStatus.Initializing&&maxTime>0)
        {
            yield return new WaitForSeconds(1);
            maxTime--;
        }
        if (maxTime <= 0)
        {
            Debug.Log("Time out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to get location");
            yield break;
        }
        
        isReady = true;
        
    }

    private void Update()
    {
        if (IsReady)
        {
            longitude = Input.location.lastData.longitude;
            latitude = Input.location.lastData.latitude;
            altitude = Input.location.lastData.altitude;
            verticalAccuracy = Input.location.lastData.verticalAccuracy;
            horizontalAccuracy = Input.location.lastData.horizontalAccuracy ;
            northDir = Input.compass.trueHeading;
        }
        IsRunning();
    }

    private void IsRunning()
    {
        if (Input.location.status != LocationServiceStatus.Running)
        {
            isReady = false;
        }
        isReady=true;
    }
}
