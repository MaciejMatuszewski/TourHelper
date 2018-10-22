
using TourHelper.Base.Manager.Devices;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public bool menuDown = false;
    public Image gpsOn;
    public Image gpsOff;

    private IGpsManager _gps;

    private void Start()
    {
        _gps = GpsManager.Instance;
    }

    private void Update()
    {
        if (_gps.IsEnabled())
        {
            gpsOn.enabled=true;
            gpsOff.enabled=false;
        }
        else
        {
            gpsOn.enabled = false;
            gpsOff.enabled = true;
        }

    }

    public void OnSettingClick(RectTransform panel)
    {
        if (menuDown)
        {
            panel.sizeDelta= new Vector2(panel.sizeDelta.x, 270);
            menuDown = false;
        }
        else
        {
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, 800);
            menuDown = true;
        }
    }

}
