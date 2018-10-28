using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }



    public void OpenMap(){
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetSceneByName("MainScene");
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
