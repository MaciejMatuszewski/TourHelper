using System;
using TourHelper.Logic.PositionLogic;
using TourHelper.Manager.Devices.Mock;
using UnityEngine;

namespace TourHelper.TestApp.Position
{
    public class LocationTest
    {

        public string DataFilePath { get; set; }
        public string LogPath { get; set; }
        public LocalPosition Processor { get; set; }
        private DevicesFromFile _data;

        private  MockGpsManager gps;
        private  MockGyroManager gyro;


        public LocationTest(string _dataFilePath, string _logFilePath)
        {
            DataFilePath = _dataFilePath;
            LogPath = _logFilePath;


        }

        public void Test()
        {

            _data = new DevicesFromFile();

            _data.FilePath = DataFilePath;

            _data.ReadData();
            gps = new MockGpsManager(_data.Position);
            gyro = new MockGyroManager(_data.Rotation, _data.Accelerations);

            Processor.Gps = gps;
            Processor.Gyro = gyro;
            Processor.LogPath = LogPath;
            Processor.LogMode = true;

            string[] filePath = Processor.LogPath.Split('\\');
            string fileName = filePath[filePath.Length - 1];
            int counter = 0;


            foreach (double s in _data.MesurementTime)
            {
                if (counter % 200 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Output file: " + fileName);
                    Console.WriteLine("Data lines: " + counter + "/" + _data.MesurementTime.Count);

                }

                Processor.Filter.DeltaTime = s;
                Processor.GetPosition();


                counter++;

            }
        }


    }
}
