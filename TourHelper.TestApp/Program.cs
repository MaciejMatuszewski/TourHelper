using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using UnityEngine;

namespace TourHelper.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*int a = 1;
            double[] output;
            TMConverter conv=new TMConverter();
            Coordinates c = new Coordinates();
            c.Latitude = 52.657570f;
            c.Longitude = 1.717922f;
           
            output = conv.ConvertCoordinates(c);

            System.Console.WriteLine("X:"+output[0].ToString()+"\nY:" + output[1].ToString() + "\nZ:");

            Vector3 offset = new Vector3(0.5f, 1);

            Vector3 vec = new Vector3(1, 1);

            Vector3 ddd = vec - offset;*/

            UTMLocalCoordinates translate;
            Coordinates origin;

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
                Vector3 o = translate.GetCoordinates(origin);
                Vector3 v = translate.GetCoordinates(i);

            }

            System.Console.ReadKey();
        }
    }
}
