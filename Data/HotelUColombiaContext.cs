﻿using HotelUColombia.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HotelUColombia.Data
{
    public class HotelUColombiaContext : DbContext
    {

        public HotelUColombiaContext(DbContextOptions<HotelUColombiaContext> options) : base(options)
        {
            // Patch for Postgres DateTime variables
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            Database.EnsureCreated();
        }

        public DbSet<Booking> Booking { get; set; } = default!;
        public DbSet<Client> Client { get; set; } = default!;
        public DbSet<Rooms> Rooms { get; set; } = default!;
        public DbSet<StatusBooking> StatusBooking { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<QuickSearch> QuickSearch { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
