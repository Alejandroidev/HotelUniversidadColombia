namespace HotelUColombia.Models
{
    public class QuickSearch : BaseClass
    {
        public DateTime pickUp { get; set; }
        public DateTime pickDown { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public int IdTypeRoom { get; set; }
    }
}
