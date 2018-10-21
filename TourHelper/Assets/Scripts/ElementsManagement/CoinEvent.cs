
using UnityEngine;
using UnityEngine.UI;

public class CoinEvent : MonoBehaviour {


    private void OnMouseDown()
    {
        Destroy(transform.gameObject);
        //odpalenie mechanizmu uruchamiajacego dodanie punktów dla uzytkownika
    }
}
