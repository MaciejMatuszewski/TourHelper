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
        public const float GRAVITY = 9.8123f;

        private Coordinate _lastGpsReading;
        private Vector3 _lastAccReading;
        private Quaternion _lastOrientation;
        private Vector3 _gpsPosition, _worldAccelerations;
        private double _accXF, _accYF, _accZF;
        private double _timeCounter = 0;
        private double _idleCounter = 0;
        private double _predictionCounter = 0;

        public IGpsManager Gps { get; set; }
        public IGyroManager Gyro { get; set; }
        public IKalman Filter { get; set; }
        public IPositionTranslation Translator { get; set; }
        public string LogPath { get; set; }
        public bool LogMode { get; set; }
        public int StandingCycles { get; set; }
        public int PredictionCycles { get; set; }
        public float StandingLimit { get; set; }
        public IFilter<double> AccelerationFilterX { get; set; }
        public IFilter<double> AccelerationFilterY { get; set; }
        public IFilter<double> AccelerationFilterZ { get; set; }
        public Coordinate Origin
        {
            get
            {

                return Translator.Origin;
            }
            set
            {
                Translator.Origin = value;
                IMatrix _newPosition = new Matrix(2, 1);
                _newPosition.SetByIndex(Translator.GetCoordinates(_lastGpsReading).x, 0, 0);
                _newPosition.SetByIndex(Translator.GetCoordinates(_lastGpsReading).z, 1, 0);
                Filter.InitialPosition = _newPosition;


            }
        }

        public LocalPosition(IGpsManager _gps, IGyroManager _gyro,
            IKalman _filter, IPositionTranslation _translator)
        {
            Gps = _gps;
            Gyro = _gyro;
            Translator = _translator;
            Filter = _filter;
            _lastGpsReading = _translator.Origin;//Gps.GetCoordinates();

            Origin = _translator.Origin;

            double[] a_l = { 1, -0.85408069 };
            double[] b_l = { 0.07295966, 0.07295966 };
            AccelerationFilterX = new IIRFilter(a_l, b_l);
            AccelerationFilterY = new IIRFilter(a_l, b_l);
            AccelerationFilterZ = new IIRFilter(a_l, b_l);

            StandingLimit = 0.1f;
            StandingCycles = 5;
            PredictionCycles = 10;
        }

        public Vector3 GetPosition()
        {
            _lastAccReading = Gyro.GetFusedAccelerations();
            _lastOrientation = Gyro.GetRotation();

            _worldAccelerations = _lastOrientation * _lastAccReading;//obrot przyspieszen do kierunkow globalnych

            _accXF = AccelerationFilterX.GetValue(_worldAccelerations.x) * GRAVITY;
            _accYF = AccelerationFilterY.GetValue(_worldAccelerations.y) * GRAVITY;
            _accZF = AccelerationFilterZ.GetValue(_worldAccelerations.z) * GRAVITY;

            
            if (isStanding())
            {
                Filter.ResetVelocity();
            }
            else
            {
                //-------------------Filter.DeltaTime = Time.deltaTime;
                _lastGpsReading = Gps.GetCoordinates();

                Filter.GPSError = 5;//---------------------_lastGpsReading.VerticalAccuracy;


                _gpsPosition = Translator.GetCoordinates(_lastGpsReading);//Pozycja x,y,z w ukladzie lokalnym; ruch odbywa się w plaszczyznie xz
                                                                         

                IMatrix accelerationMatrix = new Matrix(2, 1);

                accelerationMatrix.SetByIndex(-_accXF, 0, 0); // UWAGA KIERUNKI PRZYSPIESZEN NIE ZGODNE X dodatni wskazuje na zachod a Y dodatni na poludnie!!!!!
                accelerationMatrix.SetByIndex(-_accYF, 1, 0);

                IMatrix gpsMatrix = new Matrix(4, 1);

                gpsMatrix.SetByIndex(_gpsPosition.x, 0, 0);
                gpsMatrix.SetByIndex(_gpsPosition.z, 1, 0);

                gpsMatrix.SetByIndex(0, 2, 0);
                gpsMatrix.SetByIndex(0, 3, 0);

                Filter.Predict(accelerationMatrix);

                if ((++_predictionCounter)== PredictionCycles)
                {
                    Filter.Update(gpsMatrix);
                    _predictionCounter = 0;
                }


            }
            if (LogMode)
            {
                using (StreamWriter s = File.Exists(LogPath) ? File.AppendText(LogPath) : File.CreateText(LogPath))
                {
                    _timeCounter += Filter.DeltaTime;
                    s.Write((_timeCounter).ToString() + "|");
                    s.Write(_gpsPosition.x.ToString() + ';' + _gpsPosition.y.ToString() + ';' + _gpsPosition.z.ToString() + "|");

                    s.Write(Filter.Prediction.GetByIndex(0, 0).ToString() + ';' + 0.ToString() + ';' + Filter.Prediction.GetByIndex(1, 0).ToString() + "|");

                    s.Write(_accXF.ToString() + ';' + _accYF.ToString() + ';' + (_worldAccelerations.z * 9.8123).ToString() + "|");

                    s.Write(Filter.Prediction.GetByIndex(2, 0).ToString() + ';' + Filter.Prediction.GetByIndex(3, 0).ToString() + "|");

                    double v = Math.Sqrt(Math.Pow(Filter.Prediction.GetByIndex(2, 0), 2) + Math.Pow(Filter.Prediction.GetByIndex(3, 0), 2));

                    s.Write(v.ToString() + "|");
                    s.Write(0.ToString() + ';' + 0.ToString() + "\n");

                }
            }

            return new Vector3((float)Filter.Prediction.GetByIndex(0, 0), 0, (float)Filter.Prediction.GetByIndex(1, 0));
        }
        
        public bool isStanding()
        {
            double d = Math.Sqrt(_accZF * _accZF);
            //double d = Math.Sqrt(_accXF * _accYF + _accYF * _accYF + _accZF * _accZF);
            if (d < StandingLimit)
            {
                _idleCounter++;
                if (_idleCounter== StandingCycles)
                {
                    return true;
                }
                
            }
            _idleCounter =0;
            return false;
        }
    }
}
