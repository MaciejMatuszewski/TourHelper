using UnityEngine;
using UnityEngine.UI;
using TourHelper.Manager;


public class TextUpdate : MonoBehaviour {
    public Text text;
    private GpsManager gps;
	// Use this for initialization
	void Start () {
        text.text = "";
        gps=GpsManager.Instance;
        StartCoroutine(gps.StartService(20));
        

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
