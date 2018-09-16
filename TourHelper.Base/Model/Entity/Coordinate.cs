
namespace TourHelper.Base.Model.Entity
{
    public class Coordinate : BaseModel
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        public float VerticalAccuracy { get; set; }
        public float HorizontalAccuracy { get; set; }
    }
}
