using System.Collections;
using System.Collections.Generic;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanelEvent : MonoBehaviour {

    public Text tour;
    public Text point;
    private int lastTourID;

	void Update () {

        if (lastTourID!= PlayerPrefs.GetInt("TourID"))
        {
            var tourRepo = new TourRepository();
            var tourEnt = tourRepo.Get(PlayerPrefs.GetInt("TourID"));

            if (tour != null)
            {
                tour.text = tourEnt.Name;
            }
            else
            {
                tour.text = "";
            }
            lastTourID = PlayerPrefs.GetInt("TourID");
        }

        point.text = PlayerPrefs.GetString("PointName");

    }
}
