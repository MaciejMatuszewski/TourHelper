using System;
using System.Collections.Generic;
using System.Reflection;
using TourHelper.Base.Logic;
using TourHelper.Base.Manager;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager;
using TourHelper.Manager.Calculators;
using UnityEngine;

namespace TourHelper.Logic
{
    public class GameSpace : IGameSpace
    {
        public delegate void RebaseListener(Coordinate c);


        public bool EnforceRebuild { get; set; }

        public RebaseListener RebaseEvent;
        public IEnumerable<Assembly> Assemblies { get; private set; }
        public GameObject MainPanel { get; set; }
        public Dictionary<string, PrefabNode> Prefabs { get; private set; }
        public double LatitudeRange { get; set; }
        public double LongitudeRange { get; set; }
        private IGpsManager _gps;
        private PrefabRetrieveManager ActionsManager { get; set; }

        private Coordinate _origin;

        public UTMLocalCoordinates _translator;

        public Coordinate Origin
        {
            get => _origin;

            private set
            {
                _origin = value;
                _translator.Origin = _origin;
            }
        }

        /// <summary>
        /// Tworzy kontroler zarzadzania powtarzalnymi obiektami na scenie zależnie od położenia. 
        /// W oparciu o wspołrzędne GPS oraz założony zakres współrzędnych scena ulega przebudowaniu.  
        /// </summary>
        /// <param name="gps">Instancja obiekty zarządzajacego GPS</param>
        /// <param name="_assemblies">Lista Assemblies w której zaimplementowano IPointInRange wraz z dodatkowym
        /// atrybutem PrefabAttribute</param>
        /// 
        public GameSpace(IGpsManager gps, IEnumerable<Assembly> _assemblies)
        {
            
            _gps = gps;
            var _tmpGps = _gps.GetCoordinates();
            _translator = new UTMLocalCoordinates(_tmpGps);

            Origin = _tmpGps;
            LatitudeRange = 0.005;
            LongitudeRange = 0.01;
            Assemblies = _assemblies;
            
            Prefabs = new Dictionary<string, PrefabNode>();
            ActionsManager = new PrefabRetrieveManager(typeof(IPointInRange), Assemblies);
            RebaseEvent=delegate{ };

        }

        private PrefabNode CreateNode(string nameOfPrefab, bool _actions = true)
        {
            var newNode = new PrefabNode() { Name = nameOfPrefab };

            var newElement = new GameObject(nameOfPrefab + "_Container");
            newElement.transform.SetParent(MainPanel.transform);
            newNode.Container = newElement;
            newNode.Actions = new List<IPointInRange>();
            if (_actions)
            {
                var listOfTypes = ActionsManager.GetTypesByName(nameOfPrefab);

                

                foreach (Type type in listOfTypes)
                {
                   
                    newNode.Actions.Add((IPointInRange)Activator.CreateInstance(type));
                }
            }
            return newNode;
        }


        /// <summary>
        /// Dodaje do listy zarządzanych prefabrykatów. Konieczne jest aby Prefabrykaty o podanej nazwie 
        /// znajdowaly się w katalogu Assets/Resource/. Ważne aby definiowana nazwa odpowiadała nazwie 
        /// prefabrykatu w katalogu Resource.
        /// </summary>
        /// <param name="nameOfPrefab">Nazwa prefabrykatu do dodania.</param>
        /// <param name="defaultActions">Parametr opcjonalny. Daje możliwość wyboru czy mają zostać wgrane
        /// predefiniowane obiekty typu IPointInRange dla danego prefabrykatu.</param>
        public void AddPrefab(string nameOfPrefab, bool defaultActions = true)
        {

            if (!Prefabs.ContainsKey(nameOfPrefab))
            {
                Prefabs.Add(nameOfPrefab, CreateNode(nameOfPrefab, _actions: defaultActions));
            }
        }

        /// <summary>
        /// Dodaje do listy zarządzanych prefabrykatów. Konieczne jest aby Prefabrykaty o podanej nazwie 
        /// znajdowaly się w katalogu Assets/Resource/. Ważne aby definiowana nazwa odpowiadała nazwie 
        /// prefabrykatu w katalogu Resource. Możliwe jest dodanie własnego obiektu do obsługi pobierania współrzędnych
        /// w danym zakresie. Obiekt obsługujący musi implementować IPointInRange. 
        /// </summary>
        /// <param name="nameOfPrefab">Nazwa prefabrykatu do dodania.</param>
        /// <param name="action">Obiekt implementujący IPointInRange</param>
        /// <param name="defaultActions">
        /// Parametr opcjonalny. Daje możliwość wyboru czy mają zostać wgrane
        /// predefiniowane obiekty typu IPointInRange dla danego prefabrykatu.</param>
        public void AddPrefab(string nameOfPrefab, IPointInRange action, bool defaultActions = true)
        {
            AddPrefab(nameOfPrefab, defaultActions: defaultActions);
            Prefabs[nameOfPrefab].Actions.Add(action);
        }

        public void ClearContainer(string key)
        {
            if (Prefabs.ContainsKey(key))
            {
                GameObject.Destroy(Prefabs[key].Container);

                GameObject newElement = new GameObject(key + "_Container");

                newElement.transform.SetParent(MainPanel.transform);

                Prefabs[key].Container = newElement;
            }
        }

        public void DestroyScheme()
        {

            foreach (string k in Prefabs.Keys)
            {
                GameObject.Destroy(Prefabs[k].Container);
            }
            Prefabs.Clear();
        }

        public void ClearScheme()
        {
            foreach (string k in Prefabs.Keys)
            {
                ClearContainer(k);
            }
        }

        /// <summary>
        /// Kontrola czy konieczna jest zmiana współrzędnych odniesienia i przebudowanie sceny.
        /// </summary>
        /// <returns>Zwraca true/false zależnie od tego czy konieczne jest przebudowanie sceny</returns>
        public bool NeedRebuild()
        {
            var _reading = _gps.GetCoordinates();

            if (Math.Abs(Origin.Latitude - _reading.Latitude) > LatitudeRange ||
                Math.Abs(Origin.Longitude - _reading.Longitude) > LatitudeRange)
            {
                return true;
            }
            return false;
        }

        private void InstantiatePrefab(string prefab, IEnumerable<Coordinate> coordinates)
        {

            var prefabType = Resources.Load(prefab);
            
            if (Prefabs.ContainsKey(prefab)&& prefabType!=null)
            {
                
                foreach (Coordinate c in coordinates)
                {
                    
                    var position = _translator.GetCoordinates(c);
                    var element = (GameObject)GameObject.Instantiate(prefabType);
                    element.name = prefab +"_"+ c.Id.ToString();
                    element.transform.SetParent(Prefabs[prefab].Container.transform);
                    element.transform.Translate(position);
                }
            }

        }

        /// <summary>
        /// Przy każdym wywołaniu funkcji sprawdzany jest warunek na konieczność przebudowania sceny. 
        /// Jeeśli konieczne jest przebudowanie, kasowane są wszystkie prefabrykaty wraz z kontenerami,
        /// które je przechowują oraz tworzone są nowe egzemplarze w oparciu o zdefiniowane obiekty implementujące
        /// IPointInRange. 
        /// </summary>
        public void UpdateGameSpace()
        {
            if (NeedRebuild()|| EnforceRebuild)
            {
                Initialize();
                EnforceRebuild = false;
            }
        }

        public void Initialize()
        {
            Origin = _gps.GetCoordinates();
            foreach (string key in Prefabs.Keys)
            {
                //Debug.Log(key);
                ClearContainer(key);
                
                foreach (IPointInRange action in Prefabs[key].Actions)
                {
                    
                    var list = action.GetPointsInRange(Origin, LatitudeRange, LongitudeRange);
                    
                    InstantiatePrefab(key , list);

                }
                    
                RebaseEvent(Origin);
            }
        }
    }
}
