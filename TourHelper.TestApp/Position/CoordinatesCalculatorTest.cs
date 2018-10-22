using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using UnityEngine;

namespace TourHelper.TestApp.Position
{
    public class CoordinatesCalculatorTest
    {

        public void test()
        {
            /*Dane 
            Origin :52.407264, 16.926583

            pkt na polnoc 52.407873, 16.926617
            pkt na poludnie 52.406690, 16.926502
            pkt na wschod 52.407205, 16.927991
            pkt na zachod 52.407321, 16.925357
             
             
             */
            UTMLocalCoordinates translator =new UTMLocalCoordinates(new Coordinate() { Latitude= 52.5, Longitude= 16.5 });

            Vector3 N = translator.GetCoordinates(new Coordinate() { Latitude = 52.505, Longitude = 16.5 });
            Vector3 S = translator.GetCoordinates(new Coordinate() { Latitude = 52.495, Longitude = 16.5 });
            Vector3 W = translator.GetCoordinates(new Coordinate() { Latitude = 52.5, Longitude = 16.49 });
            Vector3 E = translator.GetCoordinates(new Coordinate() { Latitude = 52.5, Longitude = 16.51 });

            Console.WriteLine("Polnoc:" + N.x + "," + N.y + "," + N.z);
            Console.WriteLine("Poludnie:" + S.x + "," + S.y + "," + S.z);
            Console.WriteLine("wschod:" + E.x + "," + E.y + "," + E.z);
            Console.WriteLine("zachod:" + W.x + "," + W.y + "," + W.z);

            Console.ReadKey();
        }
    }
}
