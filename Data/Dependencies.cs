using Microsoft.EntityFrameworkCore;

namespace HotelUColombia.Data;
public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    => services.AddDbContext<HotelUColombiaContext>(c => c.UseNpgsql(configuration.GetConnectionString("HotelUColombiaContext")));

}