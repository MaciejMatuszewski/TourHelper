
using TourHelper.Base.Model.Entity;
using TourHelper.Logic;
using TourHelper.Manager;
using TourHelper.Repository;
using TourHelper.TestApp.Position;

namespace TourHelper.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //---------------------------------------------
            //var p = new InfoPointsInRangeManager();
            //var origin = new Coordinate() { Latitude=52.463749,Longitude= 16.921098 };

            // var pp=p.GetPointsInRange(origin, 0.2, 0.2);
            //---------------------------------------------
            /*
            var repo = new TourPointRepository();
            var p = repo.GetByCoordinateID(3);
            */

            //-----------------------------------------------
            

            var repo = new CoordinateRepository();
            var p = repo.GetByTourPointID(2);


            //-----------------------------------------------
            //var correpo = new CoordinateRepository();

            //var points = correpo.GetByTourID(3);
            //-----------------------------------------------

            //RepositoryTest.PerformTest();
            // var _test = new GameSceneTest();
            //_test.test();

            //TestSetup.LocationTest();
            //TestSetup.FilterTest();
        }

        public static void dbTest()
        {
            var x = new UserRepository();
            var y = new UserProfileRepository();
            var z = y.Insert(new UserProfile
            {
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
