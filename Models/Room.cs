namespace HotelUColombia.Models
{
    public class Rooms : BaseClass
    {
        /// <summary>
        /// Get or Set Categoria
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Get or set TV
        /// </summary>
        public bool TV { get; set; }

        ///<summary>
        ///Get or set Nevera
        ///</summary>  
        public bool Freezer { get; set; }

        /// <summary>
        /// Get or set Baño
        /// </summary>
        public bool Bathroom { get; set; }

        ///<summary>
        ///Get or set Numero de Camas
        ///</summary>
        public int NumberBeds { get; set; }
    }
}
