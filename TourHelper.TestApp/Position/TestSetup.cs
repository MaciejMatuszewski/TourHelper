using System;
using System.Collections.Generic;
using System.Diagnostics;
using TourHelper.Base.Atrybuty;
using TourHelper.Base.Manager;
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Model.Entity;
using TourHelper.Logic.PositionLogic;
using TourHelper.Manager.Calculators;
using TourHelper.Manager.Calculators.Kalman;

namespace TourHelper.TestApp.Position
{

    class TestSetup
    {
        public static void LocationTest()
        {
            string[] inputList = { "41_27" };//"a", "a1" , "b" , "b1" , "c", "d", "e""39_25_1" , "41_27"
            string script = "LocationExtractor.py";

            foreach (string input in inputList)
            {
                LocationTest test = new LocationTest(
                         $@"D:\Uczelnia\INŻYNIERKA\TESTY\Dane\{input}.txt",
                         $@"D:\Uczelnia\INŻYNIERKA\TESTY\Dane\Wyniki-location\{input}_output.txt");

                KalmanFilter filter = new KalmanFilter();

                Coordinates origin = new Coordinates();
                origin.Latitude = 52.463794708252f;
                origin.Longitude = 16.9220314025879f;

                UTMLocalCoordinates translator = new UTMLocalCoordinates(origin);

                test.Processor = new LocalPosition(null, null, filter, translator);

                test.Processor.Filter.GPSError = 5;
                test.Processor.Filter.AccelerationError = 1;


                test.Processor.StandingLimit = 0.1f;


                double[] a_l = { 1, -0.85408069 };
                double[] b_l = { 0.07295966, 0.07295966 };
      

                test.Processor.AccelerationFilterX = new IIRFilter(a_l, b_l);
                test.Processor.AccelerationFilterY = new IIRFilter(a_l, b_l);
                test.Processor.AccelerationFilterZ = new IIRFilter(a_l, b_l);

                test.Test();

                cmd($@"python D:\Uczelnia\INŻYNIERKA\TESTY\{script} D:\Uczelnia\INŻYNIERKA\TESTY\Dane\Wyniki-location\{input}_output.txt");

            }
        }
        public static void FilterTest()
        {
            string[] inputList = { "39_25_1" };
            string script = "InputFilterExtractor.py";
            string script2 = "AccelerationFFT.py";

            foreach (string input in inputList)
            {
                FilterTest test = new FilterTest(
                         $@"D:\Uczelnia\INŻYNIERKA\TESTY\Dane\{input}.txt",
                         $@"D:\Uczelnia\INŻYNIERKA\TESTY\Dane\Wyniki-filtrowanie\{input}_output.txt");

                /*
                a{1,-0.85408069};b{0.07295966,0.07295966} n=1 f=0.05 low
                a{1,-0.72654253};b{0.13672874,0.13672874} n=1 f=0.1 low
                a{1,-0.50952545};b{0.24523728,0.24523728} n=1 f=0.2 low
                a{1,-0.3249197};b{0.33754015,0.33754015} n=1 f=0.3 low


                a{1,-0.99981077};b{0.99990538,-0.99990538} n=1 f=0.001/16.5 high
                a{1,-0.9999048};b{0.9999524,-0.9999524} n=1 f=0.001/33 high
                a{1,-0.9999524};b{0.9999762,-0.9999762} n=1 f=0.001/66 high
                a{1,-0.9999762};b{0.9999881,-0.9999881} n=1 f0.001/132 high
                */
                /*
                double[] a_l = { 1, -0.72654253 };
                double[] b_l = { 0.13672874, 0.13672874 };*/
                /*
                double[] a_l = { 1, -0.85408069 };
                double[] b_l = { 0.07295966, 0.07295966 };*/

                double[] a_l = { 1, -0.85408069 };
                double[] b_l = { 0.07295966, 0.07295966 };
                double c = 0.03;

                IFilter<double> fX = new IIRFilter(a_l, b_l); //new IIRFilter(a_h, b_h, null));
                IFilter<double> fY = new IIRFilter(a_l, b_l);//new IIRFilter(a_h, b_h, null));
                IFilter<double> fZ = new IIRFilter(a_l, b_l);//new IIRFilter(a_h, b_h, null));

                test.FilterBeforeTransformation = false;

                test.FilterX = fX;
                test.FilterY = fY;
                test.FilterZ = fZ;

                test.Test();

                cmd($@"python D:\Uczelnia\INŻYNIERKA\TESTY\{script} D:\Uczelnia\INŻYNIERKA\TESTY\Dane\Wyniki-filtrowanie\{input}_output.txt");
                cmd($@"python D:\Uczelnia\INŻYNIERKA\TESTY\{script2} D:\Uczelnia\INŻYNIERKA\TESTY\Dane\Wyniki-filtrowanie\{input}_output.txt");
            }
        }

        public static void IntegralTest()
        {
            string[] inputList = { "x" };//"2m-1" , "2m-2", "2m-3", "2m-4" };
            string script = "IntegralExtractor.py";


            foreach (string input in inputList)
            {
                PositionTest test = new PositionTest(
                         $@"D:\Uczelnia\INŻYNIERKA\TESTY\Dane\{input}.txt",
                         $@"D:\Uczelnia\INŻYNIERKA\TESTY\Dane\Wyniki\{input}_output.txt");


                test.Processor.AccelerationIncluded = false;
                test.Processor.DriftReduction = false;
                test.Processor.Eps = 0.01f;

                double[] a_l = { 1.0, -5.27228221828, 11.6214883223, -13.7053868251, 9.11839568032, -3.24443185302, 0.482248506594 };
                double[] b_l = { 4.93950362255e-07, 2.96370217353e-06, 7.40925543383e-06, 9.8790072451e-06, 7.40925543383e-06, 2.96370217353e-06, 4.93950362255e-07 };

                double[] a_h = { 1.0, -2.99981159864, 2.99962321503, -0.999811616388 };
                double[] b_h = { 0.999905803757, -2.99971741127, 2.99971741127, -0.999905803757 };


                test.Processor.FilterX = new IIRFilter(a_l, b_l);
                test.Processor.FilterY = new IIRFilter(a_l, b_l);
                test.Processor.FilterZ = new IIRFilter(a_l, b_l);




                test.Processor.RegressionX = new LinearRegression(30);
                test.Processor.RegressionY = new LinearRegression(30);
                test.Processor.RegressionZ = new LinearRegression(30);

                test.Test();

                cmd($@"python D:\Uczelnia\INŻYNIERKA\TESTY\{script} D:\Uczelnia\INŻYNIERKA\TESTY\Dane\Wyniki\{input}_output.txt");
            }
        }

        static Process cmd(string command)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();
            p.StandardInput.WriteLine(command);
            p.StandardInput.Flush();
            p.StandardInput.Close();
            p.WaitForExit();

            return p;
        }


    }
}
