using System.Diagnostics;
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using TourHelper.Repository;
using TourHelper.TestApp.Position;
using UnityEngine;


namespace TourHelper.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestSetup.FilterTest();
            //TestSetup.IntegralTest();
            //TestSetup.LocationTest();
           

            CoordinatesCalculatorTest t = new CoordinatesCalculatorTest();

            t.test();
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
