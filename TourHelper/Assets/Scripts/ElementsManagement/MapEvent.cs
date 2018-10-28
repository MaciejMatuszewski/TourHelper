﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TourHelper.Base.Model.Entity;
using TourHelper.Repository;

public class MapEvent : MonoBehaviour {
    public string actualScene;

    IEnumerator LoadYourAsyncScene()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Map", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void OpenMap(){
        StartCoroutine(LoadYourAsyncScene());
        List<GameObject> rootObjects = new List<GameObject>();
        PlayerPrefs.SetString("MapPreviousScene", actualScene);
        Scene scene = SceneManager.GetSceneByName(actualScene);
        scene.GetRootGameObjects(rootObjects);

        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[i];
            foreach (Camera j in gameObject.GetComponentsInChildren<Camera>())
            {
                j.enabled = false;
            }

            foreach (Canvas k in gameObject.GetComponentsInChildren<Canvas>())
            {
                k.enabled = false;
            }
        }
    }

    public void CloseMap()
    {
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetSceneByName(PlayerPrefs.GetString("MapPreviousScene"));
        scene.GetRootGameObjects(rootObjects);

        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[i];
            foreach (Camera j in gameObject.GetComponentsInChildren<Camera>())
            {
                j.enabled = true;
            }

            foreach (Canvas k in gameObject.GetComponentsInChildren<Canvas>())
            {
                k.enabled = true;
            }


        }
        SceneManager.UnloadScene(SceneManager.GetSceneByName("Map"));
    }

    public void ShowPoint(TourPoint point)
    {
        Text pointName = GameObject.Find("PointName").GetComponent<Text>();
        pointName.enabled = true;
        pointName.text = point.Name;

        var panel = GameObject.Find("PointPanel").GetComponent<Canvas>();
        panel.enabled = true;
        var pointDesc = panel.GetComponentInChildren<Text>();
        pointDesc.text = point.Description;
        GameObject.Find("ActualPoint").GetComponent<PointInfo>().point = point;
    }

    public void SetPoint()
    {
        var point = GameObject.Find("ActualPoint").GetComponent<PointInfo>().point;
        CoordinateRepository coordinateRepo = new CoordinateRepository();
        var pointCoordinate = coordinateRepo.Get(point.CoordinateId);

        PlayerPrefs.SetString("PointName", point.Name);
        PlayerPrefs.SetInt("PointId", point.Id);
        PlayerPrefs.SetFloat("PointLat", (float)pointCoordinate.Latitude);
        PlayerPrefs.SetFloat("PointLon", (float)pointCoordinate.Longitude);

        Text pointName = GameObject.Find("PointName").GetComponent<Text>();
        pointName.enabled = true;
        pointName.text = point.Name;

        var panel = GameObject.Find("PointPanel").GetComponent<Canvas>();
        panel.enabled = false;
    }

    public void HidePointPanel()
    {
        if (GameObject.Find("ActualPoint").GetComponent<PointInfo>().point.Name != GameObject.Find("PointName").GetComponent<Text>().text)
        {
            Text pointName = GameObject.Find("PointName").GetComponent<Text>();
            pointName.enabled = false;
        }
        var panel = GameObject.Find("PointPanel").GetComponent<Canvas>();
        panel.enabled = false;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
