namespace HotelUColombia.Models
{
    /// <summary>
    /// Creada por Leidy Katherine Jacobo Perea y Luis Camilo Rodríguez Pimentel
    /// </summary>
    public class Rooms : BaseClass
    {
        /// <summary>
        /// Get or Set Category
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Get or Set TV 
        /// </summary>
        public bool TV { get; set; }
        /// <summary>
        /// Get or Set Bathroom 
        /// </summary>
        public bool Bathroom { get; set; }
        /// <summary>
        /// Get or Set Freezer
        /// </summary>
        public bool Freezer { get; set; }
        /// <summary>
        /// Get or Set NumberBeds
        /// </summary>
        public int NumberBeds { get; set; }
        /// <summary>
        /// Get or Set Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Get or Set Price
        /// </summary>
        public float Price { get; set; }

    }
}
