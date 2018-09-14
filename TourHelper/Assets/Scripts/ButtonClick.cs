
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {
    public GameObject prefab;
    public GameObject Prices;
    private UTMLocalCoordinates translate;
    private Coordinates origin;

    public void RenderTexture()
    {
        origin = new Coordinates();
        origin.Latitude = 52.463645f;
        origin.Longitude = 16.921922f;

        Coordinates[] c = new Coordinates[2];

        //-----------------
        Coordinates price1 = new Coordinates();
        price1.Latitude = 52.463812f;
        price1.Longitude = 16.921077f;
        c[0] = price1;
        //52.463812, 16.921077
        //-------------------------------
        Coordinates price2 = new Coordinates();
        price2.Latitude = 52.463554f;
        price2.Longitude = 16.921101f;
        c[1] = price2;
        //52.463554, 16.921101
        //-----------------

        translate = new UTMLocalCoordinates(origin);

        foreach (Coordinates i in c)
        {
            Transform instance= Instantiate(prefab).transform;
            
            instance.SetParent(Prices.transform);
            instance.position = translate.GetCoordinates(i);
        }
        
        
    }
}
