using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TourHelper.Base.Model.Entity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PointInfo : MonoBehaviour {

    public TourPoint point;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowInfo()
    {
        MapEvent mapEvent = new MapEvent();
        mapEvent.ShowPoint(point);
    }
}
