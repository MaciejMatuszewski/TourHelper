using UnityEngine;
using UnityEngine.UI;
using TourHelper.Manager;
using TourHelper.Manager.Devices;

public class TextUpdate : MonoBehaviour {
    public Text text;
    private GpsManager gps;
	// Use this for initialization
	void Start () {
        text.text = "";
        Debug.Log(Input.compass.enabled.ToString());
        Input.compass.enabled = true;
        Debug.Log(Input.compass.enabled.ToString());
        gps =GpsManager.Instance;
        StartCoroutine(gps.StartService(20));
        Debug.Log(Input.compass.enabled.ToString());

    }
	
	// Update is called once per frame
	void Update () {
        if (gps.IsReady())
        {           
            text.text = gps.GetCoordinates().Latitude.ToString();
        }
        else
        {
            text.text = gps.Status().ToString();
        }
	}
}
