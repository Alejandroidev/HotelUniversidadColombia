using HotelUColombia.Data;
using HotelUColombia.Models;
using Microsoft.EntityFrameworkCore;

namespace It270.MedicalManagement.Accounting.Infrastructure.Data;

public class HotelUColombiaContextSeed
{
    public static async Task SeedAsync(HotelUColombiaContext generalContext,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            #region Seed TypeSubLocation data
            if (!await generalContext.Booking.AnyAsync())
            {
                await generalContext.Booking.AddRangeAsync(
                GetPreconfiguredBooking());

                await generalContext.SaveChangesAsync();
            }
            #endregion

        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(ex.Message);
            await SeedAsync(generalContext, logger, retryForAvailability);
            throw;
        }


    }

    public static IEnumerable<Booking> GetPreconfiguredBooking()
    {
        return new List<Booking>
            {
                new Booking
                {
                    IdCliente = 1,
                    IdRoom = 1,
                    CreatedDate = DateTime.Now,
                    PickUpDate = DateTime.Now.AddDays(1),
                    ReturnDate = DateTime.Now.AddDays(3),
                    IdStatus = 1,
                    ValorDaily = 120.000,
                    IdUsuario = 1,
                }
            };
    }




}