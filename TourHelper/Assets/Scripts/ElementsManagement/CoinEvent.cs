
using UnityEngine;
using UnityEngine.UI;

public class CoinEvent : MonoBehaviour {

    public Text t;
    private void OnMouseDown()
    {
        Destroy(transform.gameObject);
        //odpalenie mechanizmu uruchamiajacego dodanie punktów dla uzytkownika
    }
}
