namespace HotelUColombia.Models
{
    public class Rooms : BaseClass
    {
        public string Category { get; set; }
        public bool TV { get; set; }
        public bool Bathroom { get; set; }
        public bool Freezer { get; set; }
        public int NumberBeds { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }

    }
}
