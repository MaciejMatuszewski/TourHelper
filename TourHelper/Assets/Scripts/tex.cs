using System.Collections;
using System.Collections.Generic;
using TourHelper.Manager.Devices;
using UnityEngine;
using UnityEngine.UI;

public class tex : MonoBehaviour
{

    private CameraManager Camera { get; set; }
    private RawImage background;
    public Text t;
    // Use this for initialization
    void Start()
    {
        Camera = CameraManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {

        background.texture = Camera.GetScreen();
        t.text = background.texture.width.ToString() + "x" + background.texture.height.ToString() + "|" +
            Screen.width.ToString() + "x" + Screen.height.ToString();
    }
}
