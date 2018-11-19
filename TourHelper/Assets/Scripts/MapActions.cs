using UnityEngine;
using UnityEngine.UI;
using TourHelper.Base.Model.Entity;
using TourHelper.Repository;

public class MapActions : MonoBehaviour {

    private GameObject pointPanel;
    private GameObject pointName;

    public void ShowPoint(TourPoint point)
    {
        pointPanel.SetActive(true);
        GameObject.Find("ActualPointName").GetComponent<Text>().text = point.Name;
        var panel = GameObject.Find("PointPanel").GetComponent<Canvas>();
        panel.GetComponentInChildren<Text>().text = point.Description;

        GameObject.Find("ActualPoint").GetComponent<PointInfo>().point = point;
    }

    public void ShowMyLocation()
    {
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().GoToLocation();
    }

    public void SetPoint()
    {
        pointName.SetActive(true);
        pointName.GetComponent<Text>().text = "Wybrany punkt: " + GameObject.Find("ActualPoint").GetComponent<PointInfo>().point.Name;

        var point = GameObject.Find("ActualPoint").GetComponent<PointInfo>().point;
        CoordinateRepository coordinateRepo = new CoordinateRepository();
        var pointCoordinate = coordinateRepo.Get((int)point.CoordinateId);

        PlayerPrefs.SetString("PointName", point.Name);
        PlayerPrefs.SetInt("PointId", point.Id);
        PlayerPrefs.SetFloat("PointLat", (float)pointCoordinate.Latitude);
        PlayerPrefs.SetFloat("PointLon", (float)pointCoordinate.Longitude);
    }
    void Start()
    {
        pointName = GameObject.Find("PointName");
        pointName.gameObject.SetActive(false);
        pointPanel = GameObject.Find("PointPanel");
        pointPanel.gameObject.SetActive(false);
    }
}
