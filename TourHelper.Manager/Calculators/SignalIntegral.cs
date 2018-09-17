using System;
using TourHelper.Base.Manager.Calculators;


namespace TourHelper.Manager.Calculators
{
    public class SignalIntegral : IIntegralCalculator
    {
        public DateTime LastUpdateTimeStamp { get; set; }
        private double integralResult=0;
        private double globalTime = 0;
        private double lastReading = 0;
        public SignalIntegral()
        {
            LastUpdateTimeStamp = DateTime.Now;
        }

        public double GetResult()
        {
            return integralResult;
        }

        public void ResetIntegral()
        {
            globalTime = 0;
            integralResult = 0;
            lastReading = 0;
            LastUpdateTimeStamp = DateTime.Now;
        }

        public void UpdateResult(double signal, DateTime stamp)
        {

            TimeSpan dt = stamp.Subtract(LastUpdateTimeStamp);
            integralResult += 0.5*(signal+lastReading) * dt.TotalSeconds;
            globalTime += dt.TotalSeconds;
            LastUpdateTimeStamp = stamp;
            lastReading = signal;
        }

    }
}
