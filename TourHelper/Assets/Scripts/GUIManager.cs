
using TourHelper.Base.Manager.Devices;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public bool menuDown = false;

    public bool mapInProcess = false;


    public Image gpsOn;
    public Image gpsOff;


    private Transform mapPanel;
    private IGpsManager _gps;

    private void Start()
    {
        _gps = GpsManager.Instance;

        float aspect = (float)Screen.width / ((float)Screen.height);

        Component[] fitters = GetComponentsInChildren(typeof(AspectRatioFitter),true);

        foreach (Component fitter in fitters)
        {
            ((AspectRatioFitter)fitter).aspectRatio = aspect;
        }
        
    }

    private void Update()
    {
        if (_gps.IsEnabled())
        {
            gpsOn.enabled = true;
            gpsOff.enabled = false;
        }
        else
        {
            gpsOn.enabled = false;
            gpsOff.enabled = true;
        }

    }

    public void OnSettingClickDropDown(RectTransform panel)
    {
        if (menuDown)
        {
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, 270);
            menuDown = false;
        }
        else
        {
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, 700);
            menuDown = true;
        }
    }

    public void OnSettingClickMenu(GameObject dropDown)
    {
        if (menuDown)
        {
            dropDown.SetActive(true);
        }
        else
        {
            dropDown.SetActive(false);
        }
    }



}
