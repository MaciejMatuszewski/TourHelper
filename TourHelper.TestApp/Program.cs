using System;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Calculators.MatrixTools;
using TourHelper.Repository;
using UnityEngine;

namespace TourHelper.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Quaternion q = new Quaternion(-0.3592106f, 0.8980265f, 0.1796053f, 0.1796053f);
            double[,] m = GetRotationMatrix(q);
            printQuaternion(q);

            printMatrix(m);


            System.Console.ReadKey();
        }



        static public double[,] GetRotationMatrix(Quaternion q)
        {
            double[,] val = new double[3, 3];

            val[0, 0] = q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z;
            val[1, 0] = 2 * q.x * q.y + 2 * q.z * q.w;
            val[2, 0] = 2 * q.x * q.z - 2 * q.y * q.w;

            val[0, 1] = 2 * q.x * q.y - 2 * q.z * q.w;
            val[1, 1] = q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z;
            val[2, 1] = 2 * q.y * q.z + 2 * q.x * q.w;

            val[0, 2] = 2 * q.x * q.z + 2 * q.y * q.w;
            val[1, 2] = 2 * q.y * q.z - 2 * q.x * q.w;
            val[2, 2] = q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z;



            return val;
        }

        static void printMatrix(double[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    System.Console.Write(m[i,j].ToString() + ' ');
                }
                System.Console.WriteLine('\n');
            }
        }

        static void printQuaternion(Quaternion q)
        {
            System.Console.WriteLine("-----------------------------------------------------------");
            System.Console.WriteLine( q.x.ToString()+' ' + q.y.ToString() + ' ' + q.z.ToString() + ' '+ q.w.ToString() );
            System.Console.WriteLine("-----------------------------------------------------------");
        }


        public void vectorTest()
        {
            int a = 1;
            double[] output;
            TMConverter conv=new TMConverter();
            Coordinates c = new Coordinates();
            c.Latitude = 52.657570f;
            c.Longitude = 1.717922f;
           
            output = conv.ConvertCoordinates(c);

            System.Console.WriteLine("X:"+output[0].ToString()+"\nY:" + output[1].ToString() + "\nZ:");

            Vector3 offset = new Vector3(0.5f, 1);

            Vector3 vec = new Vector3(1, 1);

            Vector3 ddd = vec - offset;

        }


        public void translationTest()
        {
            UTMLocalCoordinates translate;
            Coordinates origin;

            origin = new Coordinates();
            origin.Latitude = 52.463645f;
            origin.Longitude = 16.921922f;

            Coordinates[] c = new Coordinates[2];
            
            //-----------------
            Coordinates price1 = new Coordinates();
            price1.Latitude = 52.463812f;
            price1.Longitude = 16.921077f;
            c[0] = price1;
            //52.463812, 16.921077
            //-------------------------------
            Coordinates price2 = new Coordinates();
            price2.Latitude = 52.463554f;
            price2.Longitude = 16.921101f;
            c[1] = price2;
            //52.463554, 16.921101
            //-----------------

            translate = new UTMLocalCoordinates(origin);

            foreach (Coordinates i in c)
            {
                Vector3 o = translate.GetCoordinates(origin);
                Vector3 v = translate.GetCoordinates(i);

            }
        }

        public static void dbTest()
        {
            var x = new UserRepository();
            var y = new UserProfileRepository();
            var z = y.Insert(new UserProfile
            {
                Age = 33,
                Email = "kixar@wp.pl",
                FirstName = "H",
                LastName = "D"
            });
            x.Insert(new User
            {
                Login = "cycu",
                Password = "123",
                UserProfileId = z.Id
            });
            //var a = x.GetByLogin("cycu");
        }
    }
}
