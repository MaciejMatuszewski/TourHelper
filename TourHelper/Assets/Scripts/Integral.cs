using System;

using TourHelper.Manager.Calculators;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class Integral : MonoBehaviour {
	public bool isOn;
	public InputField _path;
	private AccelerationManager acc;
	private GyroManager g;
	private SignalIntegral s;
	private Vector3 _acceleration;
	private Quaternion _oriantation;
	private DateTime _stamp;
	private bool _drifReduction;
	private void Awake()
	{
		g = GyroManager.Instance;
		StartCoroutine(g.StartService(2));
		acc = AccelerationManager.Instance;
		StartCoroutine(acc.StartService(2));

	}
	// Use this for initialization
	void Start () {
		_path.text = "Log1.txt";
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn)
		{
			_acceleration = acc.GetAcceleration();
			_oriantation = g.GetRotation();
			_stamp = DateTime.Now;
			_acceleration= _oriantation* _acceleration;
			_acceleration.z += 1f;
			//s.UpdateResult(_acceleration* 9.8123f, _stamp);
			
		}
	}

	public void OnStartButtonClick(Text t)
	{
		if (!isOn)
		{
			isOn = true;
			t.text = "STOP";

			s = new SignalIntegral();
			s.LogPath = Application.persistentDataPath + '/' + _path.text;
			s.LoginMode = true;
			s.Eps = 0.08f;
			s.DriftReduction= _drifReduction;
 
			s.AccelerationIncluded = true;
		}
		else
		{
			isOn = false;
			t.text = "START";
		}
	}

	public void OnChangeToggle(Toggle t)
	{
		_drifReduction =t.isOn;
	}
}
