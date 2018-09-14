using UnityEngine;

public class ElementRotation : MonoBehaviour {

    public Transform element;
    public int speed = 1;

    private double angle = 0;
    void Update()
    {
        angle += 2 * speed * Time.deltaTime;
        angle %= 360;
        Quaternion start = element.transform.localRotation;
        Quaternion end = Quaternion.AngleAxis((float)angle, new Vector3(0, 1, 0));
        element.transform.localRotation = Quaternion.Slerp(start, end, Time.deltaTime * Mathf.Abs(speed));
    }
}
