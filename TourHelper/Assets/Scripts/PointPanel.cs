using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TourHelper.Repository;
using UnityEngine.UI;

public class PointPanel : MonoBehaviour {

    public void HidePointPanel()
    {
        GameObject.Find("PointPanel").SetActive(false);       
    }

    public void SetPoint()
    {
        MapActions mapActions = GameObject.Find("Map").GetComponent<MapActions>();
        mapActions.SetPoint();

        HidePointPanel();
    }
}
