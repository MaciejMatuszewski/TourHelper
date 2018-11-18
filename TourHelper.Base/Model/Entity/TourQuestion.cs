
namespace TourHelper.Base.Model.Entity
{
    public class TourQuestion : BaseModel
    {
        public int? TourId { get; set; }

        public string Question { get; set; }

        public string Answer1 { get; set; }

        public string Answer2 { get; set; }

        public string Answer3 { get; set; }

        public string Answer4 { get; set; }

        public short CorrectAnswer { get; set; }
    }
}
