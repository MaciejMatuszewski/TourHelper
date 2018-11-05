using System;

namespace TourHelper.Base.Model.Entity
{
    public class UserSession : BaseModel
    {
        public int? UserId { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public DateTime? LastSeen { get; set; }

        public DateTime? LogIn { get; set; }

        public DateTime? LogOut { get; set; }
    }
}
