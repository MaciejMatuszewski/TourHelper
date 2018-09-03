using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TourHelper.Base.Model.Entity
{
    public class Coordinates:BaseModel
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        public float VerticalAccuracy { get; set; }
        public float HorizontalAccuracy { get; set; }
    }
}
