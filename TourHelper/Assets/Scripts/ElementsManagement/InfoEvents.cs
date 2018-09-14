using UnityEngine;
using UnityEngine.UI;

public class InfoEvents : MonoBehaviour {

    public Text t;
    private void OnMouseDown()
    {
        Destroy(transform.gameObject);
        //odpalenie mechanizmu uruchamiajacego informacje o punkcie
    }
}
