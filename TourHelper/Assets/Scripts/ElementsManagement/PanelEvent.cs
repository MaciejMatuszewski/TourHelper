using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEvent : MonoBehaviour {

    private float dt = 0;
    private Vector3 _position;
    private Vector3 _initialPosition;
    private Vector3 _frontPosition;
    public bool inProcess = false;

    public int speed ;
    public bool isVisible = false;


	// Use this for initialization
	void Start () {
        _initialPosition = transform.localPosition;
        _frontPosition = new Vector3(0, 0, 0);
        _position = transform.localPosition;
        speed = 1;
    }
	
	// Update is called once per frame
	void Update () {

        if (inProcess)
        {
            dt += speed * Time.deltaTime;
            if (isVisible)
            {
                inProcess = ToBack();
            }
            else
            {
                inProcess = ToFront();
            }
        }
    }

    public void MovePanel()
    {
        inProcess = true;

    }

    private bool ToBack()
    {

        _position = Vector3.Lerp(_frontPosition, _initialPosition, dt);

        transform.localPosition = _position;


        var diff = _initialPosition - _position;
        if (Math.Abs(diff.x) < 0.001 && Math.Abs(diff.y) < 0.001 && Math.Abs(diff.z) < 0.001)
        {
            dt = 0;
            isVisible = false;
            return false;
        }
        return true;
    }

    private bool ToFront()
    {
        _position = Vector3.Lerp(_initialPosition, _frontPosition, dt);

        transform.localPosition = _position;

        var diff = _frontPosition - _position;
        if (Math.Abs(diff.x) < 0.001&& Math.Abs(diff.y) < 0.001&& Math.Abs(diff.z) < 0.001)
        {
            dt = 0;
            isVisible = true;
            return false;
        }
        return true;
    }

}
