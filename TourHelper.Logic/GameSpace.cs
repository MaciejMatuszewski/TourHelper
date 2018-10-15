using System;
using System.Collections.Generic;
using TourHelper.Base.Logic;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Logic
{
    public class GameSpace : IGameSpace
    {

        public GameObject MainPanel { get; set; }

        public Dictionary<string, GameObject> Containers { get; set; }

        public double LatitudeRange { get; set; }
        public double LongitudeRange { get; set; }
        private IGpsManager _gps;
        public  Coordinates Origin { get; set; }


        public GameSpace(IGpsManager gps)
        {
            _gps = gps;
            Origin = gps.GetCoordinates();
            LatitudeRange = 0.005;
            LongitudeRange = 0.01;
    }

        public bool NeedRebuild()
        {
            Coordinates _reading = _gps.GetCoordinates();

            if (Math.Abs(Origin.Latitude-_reading.Latitude)> LatitudeRange||
                Math.Abs(Origin.Longitude - _reading.Longitude) > LatitudeRange)
            {
                return true;
            }
            return false;
        }

        public void BuildScheme(IEnumerable<string> prefabs)
        {
            Containers = new Dictionary<string, GameObject>();

            foreach (string prefab in prefabs)
            {
                if (!Containers.ContainsKey(prefab))
                {
                    GameObject newElement = new GameObject(prefab + "_Container");

                    newElement.transform.SetParent(MainPanel.transform);

                    Containers.Add(prefab, newElement);
                }
            }
        }

        public void DestroyScheme()
        {

            foreach(string k in Containers.Keys)
            {
                GameObject.Destroy(Containers[k]);
            }
            Containers.Clear();
        }


        public void ClearScheme()
        {
            foreach (string k in Containers.Keys)
            {
                ClearContainer(k);
            }
        }

        public void ClearContainer(string key)
        {
            if (Containers.ContainsKey(key))
            {
                GameObject.Destroy(Containers[key]);

                GameObject newElement = new GameObject(key + "_Container");

                newElement.transform.SetParent(MainPanel.transform);

                Containers.Add(key, newElement);
            }
        }

        public void UpdateGameSpace()
        {
            
        }

    }
}
