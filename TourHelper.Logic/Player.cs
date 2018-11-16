using System;
using TourHelper.Base.Logic;
using TourHelper.Base.Logic.PositionLogic;
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using TourHelper.Logic.PositionLogic;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Calculators.Geolocation;
using TourHelper.Manager.Calculators.Kalman;
using UnityEngine;

namespace TourHelper.Logic
{
    public class Player:IPlayer
    {
        private GameObject _cameraContainer;
        private Camera _camera;
        private Quaternion _rotation;

        public IPositionUpdate _positionCalculator;
        public IDistanceManager _accumulatedDistance;
        public double AccumulatedDistance { get; set; }
        public int StartDelay { get; set; }
        public int DistanceInterval { get; set; }

        private IGyroManager _gyro;
        private IGpsManager _gps;
        private DateTime _timeStamp;
        private DateTime _startTimeStamp;

        /// <summary>
        /// Kostruktor obiektu do zarzadzania graczem. Posiada predefiniowany moduł obliczania lolalizacji w układzi
        /// lokalnym gry na podstawie GPS oraz odczytów z sensorów.
        /// </summary>
        /// <param name="_gps">Instancja obiektu zarządzajacego odczytami GPS</param>
        /// <param name="_gyro">Instancja obiektu zarządzajacego odczytami Żyroskopem</param>
        public Player(IGpsManager _gps, IGyroManager _gyro,int _startDelay,int _distanceInterval)
        {

            StartDelay = _startDelay;
            DistanceInterval = _distanceInterval;

            _timeStamp = DateTime.Now;
            _startTimeStamp = DateTime.Now;

            this._gyro = _gyro;
            this._gps = _gps;

            KalmanFilter _filter = new KalmanFilter();

            _filter.AccelerationError = 1;
          
            UTMLocalCoordinates _translator = new UTMLocalCoordinates(_gps.GetCoordinates());


            _positionCalculator = new LocalPosition(_gps, _gyro, _filter, _translator);
            _accumulatedDistance = new AccumulatedDistanceManager(
                _translator.GetCoordinates(_gps.GetCoordinates()));
            
        }

        /// <summary>
        ///Kostruktor obiektu do zarzadzania graczem. Poza managerami sensorów nalezy zdefiniować obiekt obliczajacy położenie
        ///w układzie lokalnym gry.
        /// </summary>
        /// <param name="_gps">Instancja obiektu zarządzajacego odczytami GPS</param>
        /// <param name="_gyro">Instancja obiektu zarządzajacego odczytami Żyroskopem</param>
        /// <param name="_positionCalculator">Instancja obiektu zarządzajacego obliczeniami położenia</param>
        public Player(IGpsManager _gps, IGyroManager _gyro, int _startDelay, int _distanceInterval, IPositionUpdate _positionCalculator):this(_gps,_gyro, _startDelay, _distanceInterval)
        {
            this._positionCalculator = _positionCalculator;
        }

        /// <summary>
        /// Wywołanie powoduje Inicializację gracza w przestrzeni gry
        /// </summary>
        /// <param name="camera">Obiekt typu Camera który odpowiada za kamerę w przestrzeni gry</param>
        public void InitializePlayer(Camera camera)
        {
            _camera = camera;
            _cameraContainer = new GameObject(_camera.name+"_Container");
            _cameraContainer.transform.position = _camera.transform.position;
            _camera.transform.SetParent(_cameraContainer.transform);

            _cameraContainer.transform.rotation = Quaternion.Euler(90f, 180f, 0);

            _rotation = new Quaternion(0, 0, 1, 0);

            RebasePlayer(_gps.GetCoordinates());

            UpdatePlayer();
        }

        /// <summary>
        /// Wywołanie powoduje zmienę położenia oraz rotacji kamery w przestrzeni gry
        /// </summary>
        public void UpdatePlayer()
        {
            _camera.transform.localRotation = _gyro.GetRotation() * _rotation;
           // p.Filter.DeltaTime = Time.deltaTime;
           var position= _positionCalculator.GetPosition();
            _cameraContainer.transform.position = position;

            if (DateTime.Now.Subtract(_timeStamp).TotalSeconds>=DistanceInterval&& DateTime.Now.Subtract(_startTimeStamp).TotalSeconds >= StartDelay)
            {
                AccumulatedDistance = _accumulatedDistance.GetAccumulatedDistance(position);
                _timeStamp = DateTime.Now;
            }
            
        }

        /// <summary>
        /// Wywołanie funkcji powoduje zmianę środka układu współrzednych względem którego ustalony jest układ lokalny.
        /// Może zostać zarejestowany w delegacie obiektu zarządzajacego sceną RebaseListener.
        /// </summary>
        /// <param name="_origin">Parametr względem którego przeliczane są wszpółrzędne X,Z</param>

        public void RebasePlayer(Coordinate _origin)
        {
            _positionCalculator.Origin = _origin;
        }

        public void ResetDistanceAccumulator()
        {
            _accumulatedDistance = new AccumulatedDistanceManager(_positionCalculator.GetPosition());
        }
    }
}
