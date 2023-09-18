using HotelUColombia.Data;
using HotelUColombia.Models;

namespace HotelUColombia.Helper
{
    public class DisponibilidadHelper
    {
        private readonly HotelUColombiaContext _context;
        public DisponibilidadHelper(HotelUColombiaContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Calculate Total Price Booking
        /// </summary>
        /// <param name="pickUp">pick up booking</param>
        /// <param name="returnday">return day booking</param>
        /// <param name="priceDialy">price daily room</param>
        /// <returns>total price booking</returns>
        public static double GetTotalPrice(DateTime pickUp, DateTime returnday, double priceDialy) 
        {
            TimeSpan daysBooking = pickUp - returnday;
            int days = daysBooking.Days;
            return days * priceDialy;
        }

    }
}
