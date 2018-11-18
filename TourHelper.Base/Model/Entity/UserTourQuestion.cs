
namespace TourHelper.Base.Model.Entity
{
    public class UserTourQuestion : BaseModel
    {
        public int? UserTourId { get; set; }

        public int? TourQuestionId { get; set; }

        public short Answer { get; set; }
    }
}
