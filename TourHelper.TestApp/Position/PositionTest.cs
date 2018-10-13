

using System;
using System.Threading;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Devices.Mock;
using UnityEngine;

namespace TourHelper.TestApp.Position
{
    public class PositionTest
    {
        public string DataFilePath { get; set; }
        public string LogPath { get; set; }
        public SignalIntegral Processor { get; set; }

        public PositionTest(string _dataFilePath, string _logFilePath)
        {
            DataFilePath = _dataFilePath;
            LogPath = _logFilePath;

            Processor = new SignalIntegral();

            Processor.LogPath = LogPath;
            Processor.LoginMode = true;
            Processor.Eps = 0.08f;
            Processor.DriftReduction = false;

            Processor.AccelerationIncluded = true;
        }

        public void Test()
        {
            DevicesFromFile data = new DevicesFromFile();

            MockGpsManager gps;
            MockGyroManager gyro;
            MockAccelerometrManager acc;

            Vector3 _acceleration;
            Quaternion _oriantation;
            DateTime _stamp;
            int counter = 0;
            double time = 0;
            data.FilePath = DataFilePath;
            
            data.ReadData();

            gps = new MockGpsManager(data.Position);
            gyro = new MockGyroManager(data.Rotation, data.Accelerations);
            acc = new MockAccelerometrManager(data.Accelerations);



            string[] filePath = Processor.LogPath.Split('\\');


            string fileName = filePath[filePath.Length-1];

            Processor.StartProcess();

            foreach (double s in data.MesurementTime)
            {
                if (counter % 200 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Output file: " + fileName);
                    Console.WriteLine("Data lines: "+counter + "/"+ data.MesurementTime.Count);
                    Console.WriteLine("Time elapsed: " + string.Format("{0:F2}", time)+" s");
                }

                


                _acceleration = gyro.GetFusedAccelerations();
                _oriantation = gyro.GetRotation();
                
                _acceleration = _oriantation * _acceleration;



                Processor.UpdateResult(_acceleration , (float)s);
                counter++;
                time += s;
            }
        }


    }
}
