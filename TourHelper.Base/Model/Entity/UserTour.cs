using System;

namespace TourHelper.Base.Model.Entity
{
    public class UserTour : BaseModel
    {
        public int? UserId { get; set; }

        public int? TourId { get; set; }

        public DateTime? TourStarted { get; set; }

        public DateTime? TourEnded { get; set; }

        public double? DistanceTraveled { get; set; }

        public int? CoinsCollected { get; set; }

        public int? TourPointsCount { get; set; }

        public int? TourPointsReached { get; set; }

        public int? Score { get; set; }
    }
}
