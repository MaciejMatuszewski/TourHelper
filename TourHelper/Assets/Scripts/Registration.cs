using System;
using System.Linq;
using System.Text.RegularExpressions;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Enum;
using TourHelper.Repository;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    private const string FillAllFields = "WYPEŁNIJ WSZYSTKIE POLA\n";
    private const string LoginAlreadyExists = "LOGIN JUŻ ISTNIEJE\n";
    private const string EmailAlreadyExists = "EMAIL JUŻ ISTNIEJE\n";
    private const string FillCorrectEmail = "WPISZ POPRAWNY EMAIL\n";

    public InputField UserLoginInput;
    public InputField PasswordInput;
    public InputField FirstNameInput;
    public InputField LastNameInput;
    public InputField EmailInput;
    public Text InputValidationText;

    private DateTime _wrongLoginTry;

    private void Update()
    {
        if (!string.IsNullOrEmpty(InputValidationText.text) && _wrongLoginTry.AddSeconds(3) < DateTime.Now)
        {
            InputValidationText.text = string.Empty;
        }

    }

    public void Register()
    {
        if (Validate())
        {
            var userProfileRepository = new UserProfileRepository();
            var userProfile = userProfileRepository.Insert(new UserProfile
            {
                Email = EmailInput.text,
                FirstName = FirstNameInput.text,
                LastName = LastNameInput.text
            });

            var userRepository = new UserRepository();
            userRepository.Insert(new User
            {
                Login = UserLoginInput.text,
                Password = PasswordInput.text,
                UserProfileId = userProfile.Id,
                Permission = (int)UserPermissions.User
            });

            SceneManager.LoadScene("LoginScene");
        }
    }

    private bool Validate()
    {
        bool result = true;
        InputValidationText.text = string.Empty;


        if (string.IsNullOrEmpty(UserLoginInput.text)
            || string.IsNullOrEmpty(PasswordInput.text)
            || string.IsNullOrEmpty(FirstNameInput.text)
            || string.IsNullOrEmpty(LastNameInput.text)
            || string.IsNullOrEmpty(EmailInput.text))
        {
            InputValidationText.text += FillAllFields;
            result = false;
        }

        var userRepository = new UserRepository();
        var users = userRepository.GetByLogin(UserLoginInput.text);

        if (users.Any())
        {
            InputValidationText.text += LoginAlreadyExists;
            result = false;
        }

        var userProfileRepository = new UserProfileRepository();
        var emails = userProfileRepository.GetByEmail(EmailInput.text);

        if (emails.Any())
        {
            InputValidationText.text += EmailAlreadyExists;
            result = false;
        }

        if (!IsEmailValid(EmailInput.text))
        {
            InputValidationText.text += FillCorrectEmail;
            result = false;
        }

        _wrongLoginTry = DateTime.Now;

        return result;
    }

    private bool IsEmailValid(string emailAddress)
    {
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        Regex ValidEmailRegex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

        bool isValid = ValidEmailRegex.IsMatch(emailAddress);

        return isValid;
    }
}
