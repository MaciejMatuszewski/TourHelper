
namespace TourHelper.Base.Model.Entity
{
    public class Coordinates:BaseModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double VerticalAccuracy { get; set; }
        public double HorizontalAccuracy { get; set; }
    }
}
