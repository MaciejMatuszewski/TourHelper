using TourHelper.Base.Model.Entity;
using TourHelper.Repository;

namespace TourHelper.TestApp
{
    class Program
    {
        static void Main(string[] args)
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
            var a = x.GetByLogin("cycu");
        }
    }
}
