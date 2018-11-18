using TourHelper.Repository;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    public InputField UserLoginInput;
    public InputField PasswordInput;

    public void CheckPasswordAndLogin()
    {
        var userRepository = new UserRepository();
        var user = userRepository.GetByLogin(UserLoginInput.text);

        if (user != null && PasswordInput.text == user.Password)
        {
            PlayerPrefs.SetInt("UserID", user.Id);
            SceneManager.LoadScene("MainScene");
        }
    }
}
