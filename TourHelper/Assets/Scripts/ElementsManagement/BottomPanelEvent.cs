using System.Collections;
using System.Collections.Generic;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanelEvent : MonoBehaviour
{

    public Text tour;
    public Text point;
    private int lastTourID;

    void Update()
    {

        var tourId = PlayerPrefs.GetInt("TourID");

        if (lastTourID != tourId)
        {
            var tourRepo = new TourRepository();
            var tourEnt = tourRepo.Get(tourId);

            if (tour != null)
            {
                tour.text = tourEnt.Name;
            }
            else
            {
                tour.text = "";
            }
            lastTourID = tourId;
        }

        point.text = PlayerPrefs.GetString("PointName");
    }
}
