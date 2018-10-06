using TourHelper.Base.Logic.PositionLogic;
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Manager.Calculators.Kalman;
using TourHelper.Base.Manager.Calculators.MatrixTools;
using TourHelper.Base.Manager.Devices;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators.MatrixTools;
using UnityEngine;

namespace TourHelper.Logic.PositionLogic
{
    public class LocalPosition : IPositionUpdate
    {
        public IGpsManager Gps { get ; set ; }
        public IAccelerometerManager Accelerometer { get ; set; }
        public IKalman Filter { get; set; }
       // private Coordinates _origin { get; set; }
        public IPositionTranslation Translator { get; set ; }
        public IGyroManager Gyro { get; set; }

        public Coordinates Origin { get
            {
                
                return Translator.Origin;
            }
            set
            {
                Filter.Origin = new Matrix(2,1);
                Filter.Origin.SetByIndex(Translator.GetCoordinates(value).x, 0, 0);
                Filter.Origin.SetByIndex(Translator.GetCoordinates(value).z, 0, 0);
                Translator.Origin = value;
            }
        }

        public LocalPosition(IGpsManager _gps, IAccelerometerManager _accelerometer,
            IKalman _filter, IPositionTranslation _translator, IGyroManager _gyro)
        {
            
            Gps = _gps;
            Accelerometer = _accelerometer;
            Filter = _filter;
            Translator = _translator;
            Gyro = _gyro;
            Origin = _translator.Origin;
        }

        public Vector3 GetPosition()
        {
            Vector3 gpsPosition = Translator.GetCoordinates(Gps.GetCoordinates());//Pozycja x,y,z w ukladzie lokalnym; ruch odbywa się w plaszczyznie xz
            Vector3 accelerations = Accelerometer.GetAcceleration();
            Quaternion orientation = Gyro.GetRotation();

            Vector3 worldAccelerations = orientation * accelerations;//obrot przyspieszen do kierunkow globalnych

            IMatrix accelerationMatrix= new Matrix(2, 1);

            accelerationMatrix.SetByIndex(worldAccelerations.x, 0, 0); // nalezy przeprowadzic testy na jakie kierunki rzutowane sa przyspieszenia
            accelerationMatrix.SetByIndex(worldAccelerations.y, 1, 0);

            IMatrix gpsMatrix = new Matrix(4, 1);

            gpsMatrix.SetByIndex(gpsPosition.x,0,0);
            gpsMatrix.SetByIndex(gpsPosition.x, 1, 0);

            Filter.Predict(accelerationMatrix);

            Filter.Update(gpsMatrix);

            return new Vector3((float)Filter.Prediction.GetByIndex(0,0),0, (float)Filter.Prediction.GetByIndex(1, 0));
        }
    }
}
