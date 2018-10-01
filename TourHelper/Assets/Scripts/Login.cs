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
        //Debug.Log(UserLoginInput.text);
        //Debug.Log(PasswordInput.text);
        //Debug.Log(user.Login);

        Debug.Log("BEFORE IF");
        if (user != null && PasswordInput.text == user.Password)
        {
            Debug.Log("INSIDE IF");
            SceneManager.LoadScene("MainScene");
        }
    }
}
