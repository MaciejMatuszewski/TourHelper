using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideMapComponents : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("PointName").GetComponent<Text>().enabled = false;
        GameObject.Find("PointPanel").GetComponent<Canvas>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
