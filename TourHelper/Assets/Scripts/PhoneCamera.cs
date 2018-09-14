using UnityEngine;
using TourHelper.Manager.Devices;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    private CameraManager camera;
    public RawImage background;

    public AspectRatioFitter fit;


    // Use this for initialization
    private void Start()
    {
        camera = CameraManager.Instance;
        StartCoroutine(camera.StartService(10));

        //background.texture = camera.GetScreen();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!camera.IsReady())
        {
            return;
        }

        background.texture = camera.GetScreen();
        float ratio = (float)(camera.GetScreen().width / (float)camera.GetScreen().height);
        fit.aspectRatio = ratio;

        float scaleY = camera.GetScreen().videoVerticallyMirrored ? -1f : 1f;


        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -camera.GetScreen().videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

    }
}
