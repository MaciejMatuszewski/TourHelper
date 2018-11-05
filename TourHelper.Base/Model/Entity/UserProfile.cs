namespace TourHelper.Base.Model.Entity
{
    public class UserProfile : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }
    }
}
