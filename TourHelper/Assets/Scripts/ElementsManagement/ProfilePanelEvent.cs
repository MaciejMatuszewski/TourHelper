using System.Collections;
using System.Collections.Generic;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanelEvent : MonoBehaviour {

    public Text LoginText;
    public Text PasswordText;
    public Text FirstNameText;
    public Text LastNameText;
    public Text EmailText;

    public void FillProfile()
    {
        var userId = PlayerPrefs.GetInt("UserID");
        var userRepository = new UserRepository();
        var userProfileRepository = new UserProfileRepository();

        var user = userRepository.Get(userId);
        var userProfile = userProfileRepository.Get(user.UserProfileId);

        LoginText.text = user.Login;
        PasswordText.text = user.Password;
        FirstNameText.text = userProfile.FirstName;
        LastNameText.text = userProfile.LastName;
        EmailText.text = userProfile.Email;
    }
}
