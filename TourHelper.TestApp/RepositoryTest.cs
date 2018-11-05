using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourHelper.Base.Model.Entity;
using TourHelper.Repository;

namespace TourHelper.TestApp
{
    public static class RepositoryTest
    {
        public static void PerformTest()
        {
            var userSessionRepository = new UserSessionRepository();
            userSessionRepository.Insert(new UserSession());
            userSessionRepository.Insert(new UserSession {
                LogIn = DateTime.Now,
                LastSeen = DateTime.Now.AddSeconds(-5)
            });
            var all = userSessionRepository.GetAll();
            var logged = userSessionRepository.GetLogInUsers();
            var logged2 = userSessionRepository.GetLogInUsers(4);
            var logged3 = userSessionRepository.GetLogInUsers(7);

            var userRepository = new UserRepository();
            userRepository.Insert(new User {
                Login = "test",
                Password = "test",
                Permission = 1,
                UserProfileId = 1
            });
            var all2 = userRepository.GetAll();

            var userTourRepository = new UserTourRepository();
            userTourRepository.Insert(new UserTour());
            var all3 = userTourRepository.GetAll();

            var userTourPointRepository = new UserTourPointRepository();
            userTourPointRepository.Insert(new UserTourPoint());
            var all4 = userTourPointRepository.GetAll();
        }
    }
}
