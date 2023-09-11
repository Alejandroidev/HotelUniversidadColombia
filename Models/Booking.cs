namespace HotelUColombia.Models
{
    public class Booking : BaseClass
    {
        /// <summary>
        /// Get or Set IdRoom
        /// </summary>
        public int IdRoom { get; set; }

        /// <summary>
        /// Get or Set IdCliente
        /// </summary>
        public int IdCliente { get; set; }

        /// <summary>
        /// Get or Set PickUpDate
        /// </summary>
        public DateTime PickUpDate { get; set; }

        /// <summary>
        /// Get or Set ReturnDate
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Get or Set CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Get or Set ValorDaily
        /// </summary>
        public double ValorDaily { get; set; }

        /// <summary>
        /// Get or Set IdStatus
        /// </summary>
        public int IdStatus { get; set; }

        /// <summary>
        /// Get or Set IdUsuario
        /// </summary>
        public int IdUsuario { get; set; }

    }
}
