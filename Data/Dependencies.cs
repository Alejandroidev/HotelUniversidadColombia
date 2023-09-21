using Microsoft.EntityFrameworkCore;

namespace HotelUColombia.Data;

/// <summary>
/// Dependencias, Configuracion de conexion a BD
/// </summary>
public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    => services.AddDbContext<HotelUColombiaContext>(c => c.UseNpgsql(configuration.GetConnectionString("HotelUColombiaContext")));

}