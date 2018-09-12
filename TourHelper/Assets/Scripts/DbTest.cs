using TourHelper.Repository;
using UnityEngine;
using UnityEngine.UI;

public class DbTest : MonoBehaviour
{

    public Text UiText;

    // Use this for initialization
    void Start()
    {
        var x = new UserRepository();
        var a = x.GetByLogin("cycu");
        UiText.text = a.Password;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
