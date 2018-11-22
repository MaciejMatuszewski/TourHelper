using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.UI;

public class StatsEvent : MonoBehaviour {


    public Text _distanceText;
    public Text _visitedText;
    public Text _scoreText;
    public int PushInterval { get; set; }
    
    private DateTime lastPushToDB;

    // Use this for initialization
    void Start () {

        PushInterval =150;
        lastPushToDB = DateTime.Now;
        _distanceText.text = string.Format("{0:f} km", PlayerPrefs.GetFloat("Distance"));
        _visitedText.text = PlayerPrefs.GetInt("Visited").ToString() + " pkt.";
        _scoreText.text = PlayerPrefs.GetInt("Score").ToString() + " pkt.";
    }
	
	// Update is called once per frame
	void Update () {


        ///UWAGA JESLI WYNIKI WYCIECZKI NIE ZAOSTANA SPROWADZONE NA POCZATKU MOZE DOJSC DO NADPISANIA !!!!!!!!!!!!!!!!!!!!!
        if (DateTime.Now.Subtract(lastPushToDB).TotalSeconds>= PushInterval)
        {
            
            var userId = PlayerPrefs.GetInt("UserID");
            var tourId = PlayerPrefs.GetInt("TourID");
            
            var userTourRepo = new UserTourRepository();
            var userTours = userTourRepo.GetByUserIdAndTourId(userId, tourId);

            if (userTours.Any())
            {
                var userTour = userTours.First();

                userTour.CoinsCollected = PlayerPrefs.GetInt("Score");
                userTour.DistanceTraveled = PlayerPrefs.GetInt("Distance");
                userTour.TourPointsReached = PlayerPrefs.GetInt("Visited");

                userTourRepo.Update(userTour);
                
            }
            lastPushToDB = DateTime.Now;

        }


        _distanceText.text = string.Format("{0:f} km", PlayerPrefs.GetFloat("Distance"));
        _visitedText.text = PlayerPrefs.GetInt("Visited").ToString() + " pkt.";
        _scoreText.text = PlayerPrefs.GetInt("Score").ToString() + " pkt.";
    }
}
