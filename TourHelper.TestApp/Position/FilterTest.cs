using System;
using System.IO;
using TourHelper.Base.Atrybuty;
using TourHelper.Base.Manager.Calculators;
using TourHelper.Manager.Devices.Mock;
using UnityEngine;

namespace TourHelper.TestApp.Position
{
 
    public class FilterTest
    {

        public string DataFilePath { get; set; }
        public string LogPath { get; set; }
        public bool FilterBeforeTransformation { get; set; }
    
        public IFilter<double> FilterX { get; set; }
        public IFilter<double> FilterY { get; set; }
        public IFilter<double> FilterZ { get; set; }


      
        public FilterTest(string _dataFilePath, string _logFilePath)
        {
            DataFilePath = _dataFilePath;
            LogPath = _logFilePath;


        }

        public void Test()
        {
            if (FilterX == null || FilterY == null || FilterZ == null)
            {

                Console.WriteLine("Filter are not set proprly... The end");
                return;
            }
            DevicesFromFile data = new DevicesFromFile();

            MockGyroManager gyro;
            MockAccelerometrManager acc;
            int counter = 0;
            Vector3 _acceleration;
            Vector3 _accelerationF=new Vector3();
            Quaternion _oriantation;
            
            data.FilePath = DataFilePath;

            data.ReadData();
            double worldTime = 0;
            acc = new MockAccelerometrManager(data.Accelerations);
            gyro = new MockGyroManager(data.Rotation, data.Accelerations);



            string[] filePath = LogPath.Split('\\');

            string fileName = filePath[filePath.Length - 1];



            foreach (double s in data.MesurementTime)
            {
                if (counter % 200 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Output file: " + fileName);


                }
                _acceleration = gyro.GetFusedAccelerations();
                _oriantation = gyro.GetRotation();

                if (FilterBeforeTransformation)
                {
                    _accelerationF.x=(float)FilterX.GetValue(_acceleration.x);
                    _accelerationF.y = (float)FilterY.GetValue(_acceleration.y);
                    _accelerationF.z = (float)FilterZ.GetValue(_acceleration.z);
                    _accelerationF = _oriantation * _accelerationF;

                }
                else
                {
                    _acceleration = _oriantation * _acceleration;
                    
                    _accelerationF.x = (float)FilterX.GetValue(_acceleration.x);
                    _accelerationF.y = (float)FilterY.GetValue(_acceleration.y);
                    _accelerationF.z = (float)FilterZ.GetValue(_acceleration.z);
                }


                _acceleration *= 9.8123f;


                _accelerationF *= 9.8123f;
                worldTime += s;
                logToFile(_acceleration, _accelerationF, worldTime);
            }
        }

        public void logToFile(Vector3 _accUnFiltered, Vector3 _accFiltered ,double worldTime)
        {
            using (StreamWriter s = File.Exists(LogPath) ? File.AppendText(LogPath) : File.CreateText(LogPath))
            {

                s.Write(worldTime.ToString() + "|");
                s.Write(_accUnFiltered.x.ToString() +';'+ _accUnFiltered.y.ToString()+';'+ _accUnFiltered.z.ToString() +"|");
                s.Write(_accFiltered.x.ToString() + ';' + _accFiltered.y.ToString() + ';' + _accFiltered.z.ToString() + "|");
                s.Write("\n");
                s.Close();
            }
        }
    }
}
