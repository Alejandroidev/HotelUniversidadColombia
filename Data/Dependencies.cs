using System;
using System.Configuration;
using HotelUColombia.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace It270.MedicalManagement.Accounting.Infrastructure.Data;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services) 
    => services.AddDbContext<HotelUColombiaContext>(c =>c.UseNpgsql(configuration.GetConnectionString("HotelUColombiaContext")));
    
}