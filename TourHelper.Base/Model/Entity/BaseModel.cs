using System;

namespace TourHelper.Base.Model.Entity
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
