
using UnityEngine;
using UnityEngine.UI;

public class CoinEvent : MonoBehaviour {


    private void OnMouseDown()
    {
        Destroy(transform.gameObject);

        int score = PlayerPrefs.GetInt("Score");

        PlayerPrefs.SetInt("Score", ++score);
  

        //odpalenie mechanizmu uruchamiajacego dodanie punktów dla uzytkownika
    }
}
