using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TourHelper.Base.Model.Entity;


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
            gameObject.SetActive(false);
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
            gameObject.SetActive(true);
        }
        
        SceneManager.UnloadScene(SceneManager.GetSceneByName("Map"));
    }


    public void ShowMyLocation()
    {
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().GoToLocation();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
