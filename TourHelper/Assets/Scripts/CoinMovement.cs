using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour {

    public Transform coin;
    public int speed=1;
    private double angle = 0;
	// Update is called once per frame
	void Update () {
        angle += 2*speed*Time.deltaTime;
        angle %= 360;
        Quaternion start = coin.transform.rotation;
        Quaternion end = Quaternion.AngleAxis((float)angle,new Vector3(0,1,0));
        coin.transform.rotation = Quaternion.Slerp(start, end, Time.deltaTime * speed);
	}
}
