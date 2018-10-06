using System;
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Manager.Devices;
using UnityEngine;

namespace TourHelper.Manager.Calculators
{
    public class IMUFilter : IDeviceReadingsFilter
    {
        public float Beta { get; set; }
        public float Sampling { get; set; }
        public IGyroManager Gyroscope { get; set; }
        public IAccelerometerManager Accelometer { get; set; }

        private volatile float q0 = 1.0f, q1 = 0.0f, q2 = 0.0f, q3 = 0.0f;

        public Quaternion GetOrientation()
        {
            
            return new Quaternion(q1,q2,q3, q0);
        }
        public double[,] GetRotationMatrix()
        {
            double[,] val=new double[3,3];

            val[0, 0] = 1-2*q1*q1-2*q2*q2;
            val[1, 0] = 2*q0*q1+2*q2*q3;
            val[2, 0] = 2 * q0 * q2 - 2 * q1 * q3;

            val[0, 1] = 2 * q0 * q1 - 2 * q2 * q3;
            val[1, 1] = 1-2 * q0 * q0 - 2 * q2 * q2;
            val[2, 1] = 2 * q1 * q2 + 2 * q0 * q3;

            val[0, 2] = 2 * q0 * q2 + 2 * q1 * q3;
            val[1, 2] = 2 * q1 * q2 - 2 * q0 * q3;
            val[2, 2] = 1-2 * q0 * q0 - 2 * q1 * q1;

            return val;
        }
        public void UpdateFilter()
        {
            float recipNorm;
            float s0, s1, s2, s3;
            float qDot1, qDot2, qDot3, qDot4;
            float _2q0, _2q1, _2q2, _2q3, _4q0, _4q1, _4q2, _8q1, _8q2, q0q0, q1q1, q2q2, q3q3;

            Vector3 aR=Accelometer.GetAcceleration().normalized;
            Vector3 gR = Gyroscope.GetRotationRate();



            // Rate of change of quaternion from gyroscope
            qDot1 = 0.5f * (-q1 * gR.x - q2 * gR.y - q3 * gR.z);
            qDot2 = 0.5f * (q0 * gR.x + q2 * gR.z - q3 * gR.y);
            qDot3 = 0.5f * (q0 * gR.y - q1 * gR.z + q3 * gR.x);
            qDot4 = 0.5f * (q0 * gR.z + q1 * gR.y - q2 * gR.x);

            // Compute feedback only if accelerometer measurement valid (avoids NaN in accelerometer normalisation)
            if (!((aR.x == 0.0f) && (aR.y == 0.0f) && (aR.z == 0.0f)))
            {


                // Auxiliary variables to avoid repeated arithmetic
                _2q0 = 2.0f * q0;
                _2q1 = 2.0f * q1;
                _2q2 = 2.0f * q2;
                _2q3 = 2.0f * q3;
                _4q0 = 4.0f * q0;
                _4q1 = 4.0f * q1;
                _4q2 = 4.0f * q2;
                _8q1 = 8.0f * q1;
                _8q2 = 8.0f * q2;
                q0q0 = q0 * q0;
                q1q1 = q1 * q1;
                q2q2 = q2 * q2;
                q3q3 = q3 * q3;

                // Gradient decent algorithm corrective step
                s0 = _4q0 * q2q2 + _2q2 * aR.x + _4q0 * q1q1 - _2q1 * aR.y;
                s1 = _4q1 * q3q3 - _2q3 * aR.x + 4.0f * q0q0 * q1 - _2q0 * aR.y - _4q1 + _8q1 * q1q1 + _8q1 * q2q2 + _4q1 * aR.z;
                s2 = 4.0f * q0q0 * q2 + _2q0 * aR.x + _4q2 * q3q3 - _2q3 * aR.y - _4q2 + _8q2 * q1q1 + _8q2 * q2q2 + _4q2 * aR.z;
                s3 = 4.0f * q1q1 * q3 - _2q1 * aR.x + 4.0f * q2q2 * q3 - _2q2 * aR.y;

                recipNorm = 1/Mathf.Sqrt(s0 * s0 + s1 * s1 + s2 * s2 + s3 * s3); // normalise step magnitude
                s0 *= recipNorm;
                s1 *= recipNorm;
                s2 *= recipNorm;
                s3 *= recipNorm;

                // Apply feedback step
                qDot1 -= Beta * s0;
                qDot2 -= Beta * s1;
                qDot3 -= Beta * s2;
                qDot4 -= Beta * s3;
            }

            // Integrate rate of change of quaternion to yield quaternion
            q0 += qDot1 * (1.0f / Sampling);
            q1 += qDot2 * (1.0f / Sampling);
            q2 += qDot3 * (1.0f / Sampling);
            q3 += qDot4 * (1.0f / Sampling);

            // Normalise quaternion
            recipNorm = 1 / Mathf.Sqrt(q0 * q0 + q1 * q1 + q2 * q2 + q3 * q3);
            q0 *= recipNorm;
            q1 *= recipNorm;
            q2 *= recipNorm;
            q3 *= recipNorm;
        }
    }
}
