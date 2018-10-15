using TourHelper.Base.Logic;
using TourHelper.Base.Logic.PositionLogic;

using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using TourHelper.Logic.PositionLogic;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Calculators.Kalman;
using UnityEngine;

namespace TourHelper.Logic
{
    public class Player:IPlayer
    {
        private GameObject _cameraContainer;
        private GameObject _camera;
        private Quaternion _rotation;

        public IPositionUpdate _positionCalculator;

        private IGyroManager _gyro;
        private IGpsManager _gps;


        public Player(IGpsManager _gps, IGyroManager _gyro)
        {
            this._gyro = _gyro;
            this._gps = _gps;

            KalmanFilter _filter = new KalmanFilter();

            _filter.AccelerationError = 1;

            UTMLocalCoordinates _translator = new UTMLocalCoordinates(_gps.GetCoordinates());


            _positionCalculator = new LocalPosition(_gps, _gyro, _filter, _translator);

        }
        public Player(IGpsManager _gps, IGyroManager _gyro, IPositionUpdate _positionCalculator):this(_gps,_gyro)
        {
            this._positionCalculator = _positionCalculator;
        }


        public void InitializePlayer(GameObject camera)
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

        public void UpdatePlayer()
        {
            _camera.transform.localRotation = _gyro.GetRotation() * _rotation;
           // p.Filter.DeltaTime = Time.deltaTime;
           
            _cameraContainer.transform.position = _positionCalculator.GetPosition();
        }

        public void RebasePlayer(Coordinates _origin)
        {
            _positionCalculator.Origin = _origin;
        }
    }
}
