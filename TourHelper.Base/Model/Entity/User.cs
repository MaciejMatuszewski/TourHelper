namespace TourHelper.Base.Model.Entity
{
    public class User : BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public int UserProfileId { get; set; }

        public int Permission { get; set; }
    }
}
