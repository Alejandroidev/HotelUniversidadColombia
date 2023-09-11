using HotelUColombia.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
    public class HotelUColombiaContext : DbContext
    {
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Rooms> Room { get; set; }
        public DbSet<StatusBooking> StatusBooking { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
