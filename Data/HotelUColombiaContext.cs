using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelUColombia.Models;

namespace HotelUColombia.Data
{
    public class HotelUColombiaContext : DbContext
    {
        public HotelUColombiaContext (DbContextOptions<HotelUColombiaContext> options)
            : base(options)
        {
        }

        public DbSet<HotelUColombia.Models.Booking> Booking { get; set; } = default!;
    }
}
