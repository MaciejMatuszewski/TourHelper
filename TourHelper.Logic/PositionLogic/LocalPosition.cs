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
        public const float GRAVITY= 9.8123f;
        public string LogPath { get; set; }
        public bool LogMode { get; set; }

        public IGpsManager Gps { get; set; }
        public IGyroManager Gyro { get; set; }
        public IKalman Filter { get; set; }
        public IPositionTranslation Translator { get;set; }
        public IFilter<double> AccelerationFilterX { get; set; }
        public IFilter<double> AccelerationFilterY { get; set; }
        public IFilter<double> AccelerationFilterZ { get; set; }
        public float StandingLimit {get;set;}

        private Coordinates _lastGpsReading;

        private Vector3 _lastAccReading;
        private Quaternion _lastOrientation;
        //private DateTime _timeStamp, _bufforedTimeStamp;
        //private Vector3 _bufforedGpsPosition, _gpsVelocity;
        private Vector3 _gpsPosition, _worldAccelerations;
        //private TimeSpan _gpsTimeDiff;
       // private MeanFilter _velocityX;
       // private MeanFilter _velocityZ;

        private double _accXF, _accYF, _accZF;
        private double _timeCounter = 0;
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

           // _gpsVelocity = new Vector3();

            
            _lastGpsReading = _translator.Origin;//Gps.GetCoordinates();
            Origin = _translator.Origin;
            //_timeStamp = DateTime.Now;
            //a{1,-0.85408069};b{0.07295966,0.07295966} n=1 f=0.05 low
            /*
            double[] a_l = { 1.0, -2.05583537842, 1.5087573739, -0.382291854655 };
            double[] b_l = { 0.00882876760229, 0.0264863028069, 0.0264863028069, 0.00882876760229 };
            */
            double[] a_l = { 1, -0.85408069 };
            double[] b_l = { 0.07295966, 0.07295966 };
            //_velocityX = new MeanFilter(15);
            //_velocityZ = new MeanFilter(15);
            AccelerationFilterX = new IIRFilter(a_l,b_l);
            AccelerationFilterY = new IIRFilter(a_l, b_l);
            AccelerationFilterZ = new IIRFilter(a_l, b_l);

            StandingLimit = 0.05f;
        }

        public Vector3 GetPosition()
        {
            Filter.DeltaTime = Time.deltaTime;
            _lastGpsReading = Gps.GetCoordinates();
            //_bufforedTimeStamp = DateTime.Now;

            _lastAccReading = Gyro.GetFusedAccelerations();
            _lastOrientation = Gyro.GetRotation();

            /*
            _bufforedGpsPosition = Translator.GetCoordinates(_bufforedGpsReading);
            _gpsTimeDiff = _bufforedTimeStamp - _timeStamp;*/


            Filter.GPSError = _lastGpsReading.VerticalAccuracy;

            /*
            if (_gpsTimeDiff.Milliseconds > 0)
            {
                _gpsVelocity.x = (float)_velocityX.GetValue((_bufforedGpsPosition.x - _gpsPosition.x) / Filter.DeltaTime);//((double)_gpsTimeDiff.Milliseconds/1000));
                _gpsVelocity.z = (float)_velocityZ.GetValue((_bufforedGpsPosition.z - _gpsPosition.z) / Filter.DeltaTime); //((double)_gpsTimeDiff.Milliseconds / 1000));
            }*/

            //_lastGpsReading = _bufforedGpsReading;
            _gpsPosition = Translator.GetCoordinates(_lastGpsReading);//Pozycja x,y,z w ukladzie lokalnym; ruch odbywa się w plaszczyznie xz
            //_timeStamp = _bufforedTimeStamp;


            _worldAccelerations = _lastOrientation * _lastAccReading;//obrot przyspieszen do kierunkow globalnych


            IMatrix accelerationMatrix = new Matrix(2, 1);

            _accXF = AccelerationFilterX.GetValue(_worldAccelerations.x) * GRAVITY;
            _accYF = AccelerationFilterY.GetValue(_worldAccelerations.y) * GRAVITY;
            _accZF = AccelerationFilterZ.GetValue(_worldAccelerations.z) * GRAVITY;

            accelerationMatrix.SetByIndex(-_accXF, 0, 0); // UWAGA KIERUNKI PRZYSPIESZEN NIE ZGODNE X dodatni wskazuje na zachod a Y dodatni na poludnie!!!!!
            accelerationMatrix.SetByIndex(-_accYF, 1, 0);

            IMatrix gpsMatrix = new Matrix(4, 1);

            gpsMatrix.SetByIndex(_gpsPosition.x, 0, 0);
            gpsMatrix.SetByIndex(_gpsPosition.z, 1, 0);


            gpsMatrix.SetByIndex(0, 2, 0);
            gpsMatrix.SetByIndex(0, 3, 0);


            if (isStanding())
            {
                Filter.ResetVelocity();
                //_velocityX.SetZero();
                //_velocityZ.SetZero();
            }

            Filter.Predict(accelerationMatrix);

            Filter.Update(gpsMatrix);


            if (LogMode)
            {
                using (StreamWriter s = File.Exists(LogPath) ? File.AppendText(LogPath) : File.CreateText(LogPath))
                {
                    //Debug.Log("I am in");
                    _timeCounter += Filter.DeltaTime;
                    s.Write((_timeCounter).ToString() + "|");
                    s.Write(_gpsPosition.x.ToString() + ';' + _gpsPosition.y.ToString() + ';' + _gpsPosition.z.ToString() + "|");

                    s.Write(Filter.Prediction.GetByIndex(0, 0).ToString() + ';' + 0.ToString() + ';' + Filter.Prediction.GetByIndex(1, 0).ToString() + "|");

                    //s.Write(_lastAccReading.x.ToString() + ';' + _lastAccReading.y.ToString() + ';' + _lastAccReading.z.ToString() + "|");
                    //s.Write(_lastOrientation.x.ToString() + ';' + _lastOrientation.y.ToString() + ';' + _lastOrientation.z.ToString() + ';' + _lastOrientation.w.ToString() + "|");
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
            //double d = Math.Sqrt(_accZF * _accZF);
            double d = Math.Sqrt(_accXF * _accYF + _accYF * _accYF + _accZF * _accZF);
            if (d < StandingLimit)
            {
                return true;
            }
            return false;
        }
    }
}
