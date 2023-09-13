namespace HotelUColombia.Models
{
    public class Rooms : BaseClass
    {
        /// <summary>
        /// Get or Set Categoria
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Get or set TV
        /// </summary>
        public bool TV { get; set; }

        ///<summary>
        ///Get or set Nevera
        ///</summary>  
        public bool Nevera { get; set; }

        /// <summary>
        /// Get or set Baño
        /// </summary>
        public bool Baño { get; set; }

        ///<summary>
        ///Get or set Numero de Camas
        ///</summary>
        public int NumeroDeCamas { get; set; }

    }
}
