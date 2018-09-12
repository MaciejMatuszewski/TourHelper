using UnityEngine;
using UnityEngine.UI;

public class TargetMark : MonoBehaviour {

    public Text t;
    private void OnMouseDown()
    {
        t.text = "Down";
    }
    private void OnMouseUp()
    {
        t.text = "UP";
    }
}
