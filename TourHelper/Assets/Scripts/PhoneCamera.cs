using UnityEngine;
using TourHelper.Manager.Devices;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{

    private CameraManager Camera { get; set; }
    public RawImage background;

    public AspectRatioFitter fit;
    private float scaleY, scaler;
    private int orient;
    // Use this for initialization
    private void Awake()
    {
        Camera = CameraManager.Instance;
        StartCoroutine(Camera.StartService(10));

    }

    // Update is called once per frame
    private void Update()
    {
        if (!Camera.IsReady())
        {
            return;
        }

        background.texture = Camera.GetScreen();
        float ratio = (float)(Camera.GetScreen().width / (float)Camera.GetScreen().height);
        fit.aspectRatio = ratio;

        scaleY = Camera.GetScreen().videoVerticallyMirrored ? -1f : 1f;

        scaler = (float)Screen.width / (float)background.texture.width;

        background.rectTransform.localScale = new Vector3(scaler, scaler * scaleY, 1f);
        orient = -Camera.GetScreen().videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

    }
}
