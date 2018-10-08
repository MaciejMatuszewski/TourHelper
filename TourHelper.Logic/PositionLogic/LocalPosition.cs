using System;
using System.IO;
using TourHelper.Base.Logic.PositionLogic;
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Manager.Calculators.Kalman;
using TourHelper.Base.Manager.Calculators.MatrixTools;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Calculators.MatrixTools;
using UnityEngine;

namespace TourHelper.Logic.PositionLogic
{
    public class LocalPosition : IPositionUpdate
    {
        public string LogPath { get; set; }
        public IGpsManager Gps { get; set; }
        public IAccelerometerManager Accelerometer { get; set; }
        public IKalman Filter { get; set; }
        // private Coordinates _origin { get; set; }
        public IPositionTranslation Translator { get; set; }
        public IGyroManager Gyro { get; set; }
        private Coordinates _lastGpsReading, _bufforedGpsReading;

        private Vector3 _lastAccReading;
        private Quaternion _lastOrientation;
        private DateTime _timeStamp, _bufforedTimeStamp;
        private Vector3 _gpsPosition, _worldAccelerations, _bufforedGpsPosition, _gpsVelocity;
        private TimeSpan _gpsTimeDiff;
        private MeanFilter _velocityX;
        private MeanFilter _velocityY;
        private double _counter = 0;

        public Coordinates Origin
        {
            get
            {

                return Translator.Origin;
            }
            set
            {
                //Debug.Log("definicja origin:"+ Translator.GetCoordinates(value).x.ToString()+' '+ Translator.GetCoordinates(value).z.ToString());
                Translator.Origin = value;
                IMatrix _newPosition = new Matrix(2,1);
                _newPosition.SetByIndex(Translator.GetCoordinates(_lastGpsReading).x, 0, 0);
                _newPosition.SetByIndex(Translator.GetCoordinates(_lastGpsReading).z, 1, 0);
                Filter.InitialPosition= _newPosition;


            }
        }

        public LocalPosition(IGpsManager _gps, IAccelerometerManager _accelerometer,
            IKalman _filter, IPositionTranslation _translator, IGyroManager _gyro)
        {

            Gps = _gps;
            Accelerometer = _accelerometer;

            Translator = _translator;
            Gyro = _gyro;
            Filter = _filter;

            _gpsVelocity = new Vector3();
            _lastGpsReading = Gps.GetCoordinates();
            _timeStamp = DateTime.Now;
            Origin = _translator.Origin;

            _velocityX = new MeanFilter(15);
            _velocityY = new MeanFilter(15);
        }

        public Vector3 GetPosition()
        {
            _bufforedGpsReading = Gps.GetCoordinates();
            _bufforedTimeStamp = DateTime.Now;

            _lastAccReading = Accelerometer.GetAcceleration();
            _lastOrientation = Gyro.GetRotation();


            _bufforedGpsPosition = Translator.GetCoordinates(_bufforedGpsReading);
            _gpsTimeDiff = _bufforedTimeStamp - _timeStamp;

            if (_gpsTimeDiff.Seconds > 0)
            {
                _gpsVelocity.x = (float)_velocityX.GetValue((_bufforedGpsPosition.x - _gpsPosition.x) / _gpsTimeDiff.Seconds);
                _gpsVelocity.z = (float)_velocityX.GetValue((_bufforedGpsPosition.z - _gpsPosition.z) / _gpsTimeDiff.Seconds);
            }

            _lastGpsReading = _bufforedGpsReading;
            _gpsPosition = Translator.GetCoordinates(_lastGpsReading);//Pozycja x,y,z w ukladzie lokalnym; ruch odbywa się w plaszczyznie xz
            _timeStamp = _bufforedTimeStamp;


            _worldAccelerations = _lastOrientation * _lastAccReading;//obrot przyspieszen do kierunkow globalnych


            IMatrix accelerationMatrix = new Matrix(2, 1);

            accelerationMatrix.SetByIndex(_worldAccelerations.x, 0, 0); // nalezy przeprowadzic testy na jakie kierunki rzutowane sa przyspieszenia
            accelerationMatrix.SetByIndex(_worldAccelerations.y, 1, 0);

            IMatrix gpsMatrix = new Matrix(4, 1);

            gpsMatrix.SetByIndex(_gpsPosition.x, 0, 0);
            gpsMatrix.SetByIndex(_gpsPosition.z, 1, 0);


            gpsMatrix.SetByIndex(_gpsVelocity.x, 2, 0);
            gpsMatrix.SetByIndex(_gpsVelocity.z, 3, 0);

            Filter.Predict(accelerationMatrix);

            Filter.Update(gpsMatrix);


            using (StreamWriter s = File.Exists(LogPath) ? File.AppendText(LogPath) : File.CreateText(LogPath))
            {
                //Debug.Log("I am in");
                _counter += Filter.DeltaTime;
                s.Write(_counter.ToString() + "|");
                s.Write(_gpsPosition.x.ToString() + ';' + _gpsPosition.y.ToString() + ';' + _gpsPosition.z.ToString() + "|");

                s.Write(Filter.Prediction.GetByIndex(0, 0).ToString() + ';' + 0.ToString() + ';' + Filter.Prediction.GetByIndex(1, 0).ToString() + "|");

                //s.Write(_lastAccReading.x.ToString() + ';' + _lastAccReading.y.ToString() + ';' + _lastAccReading.z.ToString() + "|");
                //s.Write(_lastOrientation.x.ToString() + ';' + _lastOrientation.y.ToString() + ';' + _lastOrientation.z.ToString() + ';' + _lastOrientation.w.ToString() + "|");
                s.Write(_worldAccelerations.x.ToString() + ';' + _worldAccelerations.y.ToString() + ';' + _worldAccelerations.z.ToString() + "|");

                s.Write(Filter.Prediction.GetByIndex(2, 0).ToString() + ';' + Filter.Prediction.GetByIndex(3, 0).ToString() + "|");

                double v = Math.Sqrt(Math.Pow(Filter.Prediction.GetByIndex(2, 0), 2) + Math.Pow(Filter.Prediction.GetByIndex(3, 0), 2));


                s.Write(v.ToString()+"\n");


            }

            return new Vector3((float)Filter.Prediction.GetByIndex(0, 0), 0, (float)Filter.Prediction.GetByIndex(1, 0));
        }
    }
}
