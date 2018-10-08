

using System;
using TourHelper.Base.Manager.Calculators.Kalman;
using TourHelper.Base.Manager.Calculators.MatrixTools;
using TourHelper.Manager.Calculators.MatrixTools;

namespace TourHelper.Manager.Calculators.Kalman
{
    public class KalmanFilter : IKalman
    {
        public IMatrix Prediction { get; private set; }
        public IMatrix ProcessCovariance { get; private set; }
        private double accelerationError;
        private double gPSError;
        private double deltaTime;
        private IMatrix _origin;
        public double DeltaTime
        {
            get
            { return deltaTime; }
            set
            {
                deltaTime = value;
                UpdateBaseMatrix();
            }
        }
        public double AccelerationError
        {
            get
            { return accelerationError; }
            set
            {
                accelerationError = value;
                UpdateQMatrix();
            }
        }
        public double GPSError
        {
            get
            { return gPSError; }
            set
            {
                gPSError = value;
                UpdateRMatrix();
            }
        }

        public IMatrix KalmanGain { get; private set; }
        public IMatrix InitialPosition { get=> _origin; set=> ResetPosition(value); }

        private IMatrix a, b, q, h, r, identity;

        public KalmanFilter()
        {
            _origin = new Matrix(4,1);
            DeltaTime = 0.1;
            //----------------Inicializacja macierzy jednostkowej-------------------------
            double[,] iTemp = new double[,] {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            identity = new Matrix(iTemp.GetLength(0), iTemp.GetLength(1));
            identity.SetAll(iTemp);

            //----------------Inicializacja macierzy A i B---------------------

            UpdateBaseMatrix();

            //---------------Inicializacja macierzy H--------------------------
            double[,] htemp = new double[,] {
                { 1,0,0,0},
                { 0,1,0,0},
                { 0,0,1,0},
                { 0,0,0,1}
            };

            h = new Matrix(htemp.GetLength(0), htemp.GetLength(1));
            h.SetAll(htemp);
            //---------------Inicializacja predykcji--------------------------

            SetPrediction();

            //---------------Inicializacja szumu procesu--------------------------

            UpdateQMatrix();

            //---------------Inicializacja szumu pomiaru--------------------------

            UpdateRMatrix();

            //---------------Inicializacja macierzy kowariancji--------------------------
            ResetCovariance();

        }

        public void Predict(IMatrix accelerations)
        {

            Prediction = a.Multiply(Prediction).Add(b.Multiply(accelerations));

            ProcessCovariance = a.Multiply(ProcessCovariance).Multiply(a.Transpose()).Add(q).Diagonal();
        }

        public void Update(IMatrix gpsMesurements)
        {


            KalmanGain = ProcessCovariance.Multiply(h.Transpose()).
                DivideDiagonal(h.Multiply(ProcessCovariance.Multiply(h.Transpose())).Add(r));

            gpsMesurements = h.Multiply(gpsMesurements);

            Prediction = Prediction.Add(KalmanGain.Multiply(gpsMesurements.Sub(h.Multiply(Prediction))));

            ProcessCovariance = identity.Sub(KalmanGain.Multiply(h)).Multiply(ProcessCovariance);
        }

        private void UpdateQMatrix()
        {
            double err_s = AccelerationError * DeltaTime;// position error due to accelereation
            double err_v = 0.5 * AccelerationError * DeltaTime * DeltaTime;//speed error due to acceleration

            double[,] qTemp = new double[,] {
                {err_s*err_s,err_s*err_v,0,0 },
                {0,err_s*err_s,0,err_s*err_v },
                {0,0,err_v*err_v,0 },
                {0,0,0,err_v*err_v },
            };

            q = new Matrix(qTemp.GetLength(0), qTemp.GetLength(1));
            q.SetAll(qTemp); //process noise variance
        }

        private void UpdateRMatrix()
        {
            double err_vGPS = GPSError / DeltaTime;

            double[,] rTemp = new double[,] {
                {GPSError*GPSError,0,0,0 },
                {0,GPSError*GPSError,0,0 },
                {0,0,err_vGPS*err_vGPS,0 },
                {0,0,0,err_vGPS*err_vGPS },
            };


            r = new Matrix(rTemp.GetLength(0), rTemp.GetLength(1));
            r.SetAll(rTemp); //mesurement variance
        }

        private void UpdateBaseMatrix()
        {
            //----------------Inicializacja macierzy A-------------------------
            double[,] aTemp = new double[,] {
                { 1, 0, DeltaTime, 0 },
                { 0, 1, 0, DeltaTime },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            a = new Matrix(aTemp.GetLength(0), aTemp.GetLength(1));
            a.SetAll(aTemp);

            //---------------Inicializacja macierzy B--------------------------

            double[,] bTemp = new double[,] {
                { 0.5*DeltaTime*DeltaTime, 0},
                { 0, 0.5*DeltaTime*DeltaTime},
                { DeltaTime, 0 },
                { 0, DeltaTime }
            };

            b = new Matrix(bTemp.GetLength(0), bTemp.GetLength(1));
            b.SetAll(bTemp);
        }

        private void ResetCovariance()
        {
            double[,] p0 = new double[,] {
                {1,0,0,0 },
                {0,1,0,0 },
                {0,0,1,0 },
                {0,0,0,1 },
            };


            ProcessCovariance = new Matrix(p0.GetLength(0), p0.GetLength(1));
            ProcessCovariance.SetAll(p0);
        }

        private void SetPrediction()
        {
            double[,] pTemp = new double[,] { { InitialPosition.GetByIndex(0,0) }, { InitialPosition.GetByIndex(1, 0) }, { 0 }, { 0 } };

            Prediction = new Matrix(pTemp.GetLength(0), pTemp.GetLength(1));
            Prediction.SetAll(pTemp);
        }
        private void ResetPosition(IMatrix o)
        {
            _origin = o;
            Prediction.SetByIndex(InitialPosition.GetByIndex(0, 0), 0,0);
            Prediction.SetByIndex(InitialPosition.GetByIndex(1, 0), 1, 0);
        }
    }
}
