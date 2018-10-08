namespace TourHelper.Base.Model.Entity
{
    public class User : BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public int UserProfileId { get; set; }

        void test()
        {
            var x = typeof(User).GetProperties();
        }
    }
}
