using UnityEngine;
using TourHelper.Manager.Devices;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
 
    private CameraManager Camera { get; set; }
    public RawImage background;

    public AspectRatioFitter fit;


    // Use this for initialization
    private void Start()
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

        float scaleY = Camera.GetScreen().videoVerticallyMirrored ? -1f : 1f;


        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -Camera.GetScreen().videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

    }
}
