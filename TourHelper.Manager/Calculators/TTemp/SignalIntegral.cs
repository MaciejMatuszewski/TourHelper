using System;
using System.IO;
using TourHelper.Base.Manager.Calculators;
using UnityEngine;

namespace TourHelper.Manager.Calculators
{
    public class SignalIntegral : IIntegralCalculator
    {
        public DateTime LastUpdateTimeStamp { get; set; }
        public float Eps { get; set; }
        public string LogPath { get; set; }
        public bool LoginMode { get; set; }
        public bool DriftReduction { get; set; }
        public bool AccelerationIncluded { get; set; }

        public IFilter<double> FilterX { get; set; }
        public IFilter<double> FilterY { get; set; }
        public IFilter<double> FilterZ { get; set; }

        public IFilter<double> FilterVX { get; set; }
        public IFilter<double> FilterVY { get; set; }
        public IFilter<double> FilterVZ { get; set; }

        public LinearRegression RegressionX { get; set; }
        public LinearRegression RegressionY { get; set; }
        public LinearRegression RegressionZ { get; set; }

        public double TimeOut { get; set; }

        private Vector3 integralResult;
        private double globalTime = 0;

        private double worldlTime = 0;
        private double regresionUpdateTime = 0;

        private Vector3 integralResultDrifted;
        private Vector3 drift;


        private Vector3 _initialVelocity;

        private Vector3 _position;//do logowania


        public double maxX = 0;
        public double maxY=0;


        public Vector3 InitialVelocity
        {
            get => _initialVelocity;
            set
            {
                _initialVelocity = value;
                ResetIntegral();
            }
        }

        public SignalIntegral()
        {
            _position = new Vector3();
            InitialVelocity = new Vector3();
            RegressionX = new LinearRegression(2);
            RegressionY = new LinearRegression(2);
            RegressionZ = new LinearRegression(2);

            ResetIntegral();
            LogPath = "IntegratorLog.txt";

        }

        public Vector3 GetResult()
        {
            return integralResult;
        }

        private void ResetIntegral()
        {
            globalTime = 0;
            integralResult = InitialVelocity;
            drift = new Vector3();
            integralResultDrifted = new Vector3();
            LastUpdateTimeStamp = DateTime.Now;

        }


        public void StartProcess()
        {
            ResetIntegral();
            worldlTime = 0;
            RegressionX.Reset();
            RegressionY.Reset();
            RegressionZ.Reset();

        }

        public void UpdateResult(Vector3 signal, float dt)//DateTime stamp)
        {
            //var dt = stamp.Subtract(LastUpdateTimeStamp);



            if (!(FilterX == null))
            {
                signal.x = (float)FilterX.GetValue(signal.x);
            }
            if (!(FilterY == null))
            {
                signal.y = (float)(FilterY.GetValue(signal.y));
            }
            if (!(FilterZ == null))
            {
                signal.z = (float)(FilterZ.GetValue(signal.z));
            }

            signal *= 9.8123f;
            worldlTime += dt;//dt.TotalSeconds;

            if (DriftReduction && ((worldlTime - regresionUpdateTime) > TimeOut))
            {
                RegressionX.Update(worldlTime, signal.x * dt);//.TotalSeconds);
                RegressionY.Update(worldlTime, signal.y * dt);//.TotalSeconds);
                RegressionZ.Update(worldlTime, signal.z * dt);//.TotalSeconds);
                regresionUpdateTime = worldlTime;
            }

            if (isStanding(signal))
            {


                InitialVelocity = new Vector3();
            }
            else
            {


                globalTime += dt;//.TotalSeconds;



                if (!(FilterVX == null))
                {
                    integralResultDrifted.x += (float)FilterVX.GetValue(signal.x * dt);
                }
                else
                {
                    integralResultDrifted.x += signal.x * dt;
                }
                if (!(FilterVY == null))
                {
                    integralResultDrifted.y += (float)(FilterVY.GetValue(signal.y * dt));
                }
                else
                {
                    integralResultDrifted.y += signal.y * dt;
                }
                if (!(FilterVZ == null))
                {
                    integralResultDrifted.z += (float)(FilterVZ.GetValue(signal.z * dt));
                }
                else
                {
                    integralResultDrifted.z += signal.z * dt;
                }


                if (DriftReduction)
                {

                    integralResult.x = integralResultDrifted.x - ((float)(dt * RegressionX.GetTrendValue(dt)));
                    integralResult.y = integralResultDrifted.y - ((float)(dt * RegressionY.GetTrendValue(dt)));
                    integralResult.z = integralResultDrifted.z - ((float)(dt * RegressionZ.GetTrendValue(dt)));

                }
                else
                {

                    integralResult = integralResultDrifted;
                }

                _position += integralResult * dt;

                if (AccelerationIncluded)
                {
                    _position += (float)0.5 * signal * (float)(dt * dt);
                }

                if (maxX< Math.Abs(_position.x))
                {
                    maxX = Math.Abs(_position.x);
                }
                if (maxY < Math.Abs(_position.y))
                {
                    maxY = Math.Abs(_position.y);
                }
                //LastUpdateTimeStamp = stamp;

            }

            if (LoginMode)
            {
                logToFile(signal);
            }

        }

        public bool isStanding(Vector3 signal)
        {
            if (Math.Sqrt(signal.x * signal.x + signal.y * signal.y + signal.z * signal.z) < Eps)
            {
                return true;
            }

            return false;

        }




        public void logToFile(Vector3 signal)
        {
            using (StreamWriter s = File.Exists(LogPath) ? File.AppendText(LogPath) : File.CreateText(LogPath))
            {

                s.Write(worldlTime.ToString() + "|");
                s.Write(integralResult.x.ToString() + ';' + integralResult.y.ToString() + ';' + integralResult.z.ToString() + "|");
                s.Write(drift.x.ToString() + ';' + drift.y.ToString() + ';' + drift.z.ToString() + "|");
                s.Write(_position.x.ToString() + ';' + _position.y.ToString() + ';' + _position.z.ToString() + "|");


                s.Write(signal.x.ToString() + ';' + signal.y.ToString() + ';' + signal.z.ToString() + "|");

                s.Write(Math.Sqrt(signal.x * signal.x + signal.y * signal.y + signal.z * signal.z).ToString() + "|");
                s.Write(integralResultDrifted.x.ToString() + ';' + integralResultDrifted.y.ToString() + ';' + integralResultDrifted.z.ToString() + "|");

                s.Write("\n");
                s.Close();
            }
        }

    }
}
