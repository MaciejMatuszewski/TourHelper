using System;
using System.Linq;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private const string WrongLoginOrPass = "ZŁY LOGIN LUB HASŁO";

    public InputField UserLoginInput;
    public InputField PasswordInput;
    public Text WrongCredentialText;

    private DateTime _wrongLoginTry;

    private void Update()
    {
        if (!string.IsNullOrEmpty(WrongCredentialText.text) && _wrongLoginTry.AddSeconds(3) < DateTime.Now)
        {
            WrongCredentialText.text = string.Empty;
        }

    }

    public void CheckPasswordAndLogin()
    {
        var userRepository = new UserRepository();
        var user = userRepository.GetByLogin(UserLoginInput.text);

        if (user.Any() && PasswordInput.text == user.Single().Password)
        {
            if (user.Single().Id != PlayerPrefs.GetInt("UserID"))
            {
                PlayerPrefs.SetInt("UserTourID", 0);
            }
            PlayerPrefs.SetInt("UserID", user.Single().Id);
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            WrongCredentialText.text = WrongLoginOrPass;
            _wrongLoginTry = DateTime.Now;
        }
    }
}
