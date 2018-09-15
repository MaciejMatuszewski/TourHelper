
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Text debugText;

    public GameObject prefab;
    public GameObject Prices;
    private UTMLocalCoordinates translate;
    private Coordinates origin;

    public void RenderTexture()
    {
        origin = new Coordinates();
        origin.Latitude = 52.463f;
        origin.Longitude = 16.92f;

        Coordinates[] c = new Coordinates[3];


        //-----------------
        Coordinates price0 = new Coordinates();
        price0.Latitude = 52.463907f;
        price0.Longitude = 16.920955f;
        c[0] = price0;
        //52.463907, 16.920955
        //-----------------
        Coordinates price1 = new Coordinates();
        price1.Latitude = 52.463659f;
        price1.Longitude = 16.920783f;
        c[1] = price1;
        //52.463659, 16.920783
        //-------------------------------
        Coordinates price2 = new Coordinates();
        price2.Latitude = 52.463765f;
        price2.Longitude = 16.919549f;
        c[2] = price2;
        //52.463765, 16.919549
        //-----------------

        translate = new UTMLocalCoordinates(origin);

        foreach (Coordinates i in c)
        {
            Transform instance = Instantiate(prefab).transform;

            instance.SetParent(Prices.transform);
            instance.position = translate.GetCoordinates(i);

            debugText.text += "\n"+instance.position.x.ToString() + "," +
            instance.position.y.ToString() + "," +instance.position.z.ToString();
        }


    }
}
