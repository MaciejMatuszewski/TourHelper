namespace TourHelper.Base.Model.Entity
{
    public class TourPoint : BaseModel
    {
        public int? TourId { get; set; }

        public int? CoordinateId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
