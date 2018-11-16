﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TourHelper.Base.Model.Entity;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.UI;

public class TourList : MonoBehaviour
{

    private IEnumerable<Tour> _allTours;

    public Transform ButtonsContainer;
    public GameObject TourButtonPrefab;
    public InputField FilterInput;

    // Use this for initialization
    void Start()
    {
        var tourRepository = new TourRepository();
        _allTours = tourRepository.GetAll();
        foreach (var tour in _allTours)
        {
            AddTourButton(tour.Name, tour.Id);
        }

        FilterInput.onValueChange.AddListener(delegate { UpdateFilter(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddTourButton(string buttonLabel, int tourId)
    {
        var go = Instantiate(TourButtonPrefab);
        go.transform.SetParent(ButtonsContainer);

        go.name = "TourId_" + tourId;
        var buttonText = go.GetComponentInChildren<Text>() as Text;
        buttonText.text = buttonLabel;

        var position = go.transform.localPosition;
        position.z = 0;
        go.transform.localPosition = position;

        var rotation = go.transform.localRotation = Quaternion.Euler(0, 0, 0);

        go.transform.localScale = Vector3.one;

        var button = go.GetComponent<Button>();
        button.onClick.AddListener(() => StartTour(Convert.ToInt32(go.name.Replace("TourId_", ""))));
    }

    private void UpdateFilter()
    {
        ClearList();
        var filteredTours = _allTours.Where(at => at.Name.Contains(FilterInput.text));
        foreach (var tour in filteredTours)
        {
            AddTourButton(tour.Name, tour.Id);
        }
    }

    private void ClearList()
    {
        var tourButtons = ButtonsContainer.GetComponentsInChildren<Button>();

        foreach (var tourButton in tourButtons)
        {
            Destroy(tourButton.gameObject);
        }

    }

    private void StartTour(int tourId)
    {
        PlayerPrefs.SetInt("UserId", 1);
        var userId = PlayerPrefs.GetInt("UserId");

        var userTourRepository = new UserTourRepository();
        var userTours = userTourRepository.GetByUserIdAndTourId(userId, tourId);

        if (userTours.Any())
        {
            PlayerPrefs.SetInt("TourID", userTours.OrderByDescending(ut => ut.CreatedOn).First().Id);
        }
        else
        {
            var tourPointRepository = new TourPointRepository();
            var tourpoints = tourPointRepository.GetByTourID(tourId);

            var userTour = userTourRepository.Insert(new UserTour
            {
                TourPointsCount = tourpoints.Count(),
                TourId = tourId,
                UserId = userId,
                TourStarted = DateTime.Now
            });

            PlayerPrefs.SetInt("UserTourID", userTour.Id);
        }
    }
}