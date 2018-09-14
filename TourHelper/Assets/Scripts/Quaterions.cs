using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaterions : MonoBehaviour {
    public float x, y, z, w;
    public Transform t;
	// Update is called once per frame
	void Update () {
        t.rotation = new Quaternion(x, y, z, w);
	}
}
