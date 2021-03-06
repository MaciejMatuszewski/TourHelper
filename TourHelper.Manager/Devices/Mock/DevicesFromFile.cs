﻿
using System;
using System.Collections.Generic;
using System.IO;
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Manager.Devices.Mock
{
    public class DevicesFromFile
    {
        public string FilePath { get; set; }

        public List<double> MesurementTime { get; set; }
        public List<Vector3> Accelerations { get; set; }
        public List<Quaternion> Rotation { get; set; }
        public List<Coordinate> Position { get; set; }


        public DevicesFromFile()
        {
            Position = new List<Coordinate>();
            Accelerations = new List<Vector3>();
            Rotation = new List<Quaternion>();
            MesurementTime = new List<double>();
        }

        public void ReadData()
        {
            using (StreamReader s = new StreamReader(FilePath))
            {
                string line;
                string[] splitedLine;

                string[] time;
                string[] accelerations;
                string[] rotations;
                string[] positions;

                while ((line = s.ReadLine()) != null)
                {
                    splitedLine = line.Split('|');


                    time= splitedLine[0].Split(';');

                    MesurementTime.Add(Convert.ToDouble(time[0]));

                    accelerations = splitedLine[1].Split(';');

                    Accelerations.Add(new Vector3(
                        (float)Convert.ToDouble(accelerations[0]),
                        (float)Convert.ToDouble(accelerations[1]),
                        (float)Convert.ToDouble(accelerations[2])
                        ));



                    rotations = splitedLine[2].Split(';');

                    Rotation.Add(new Quaternion()
                    {
                        x = (float)Convert.ToDouble(rotations[0]),
                        y = (float)Convert.ToDouble(rotations[1]),
                        z = (float)Convert.ToDouble(rotations[2]),
                        w = (float)Convert.ToDouble(rotations[3]),
                    });

                    
                    positions = splitedLine[3].Split(';');


                    Position.Add(new Coordinate() {
                        Latitude =Convert.ToDouble(positions[0]),
                        Longitude = Convert.ToDouble(positions[1])
                    });




                }
               
            }

        }

    }
}
