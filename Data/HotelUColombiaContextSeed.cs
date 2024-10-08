﻿using HotelUColombia.Data;
using HotelUColombia.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Migrations;

namespace HotelUColombia.Data;

public class HotelUColombiaContextSeed
{
    /// <summary>
    /// Creado Por Alejandro Salcedo
    /// </summary>
    /// <param name="generalContext"></param>
    /// <param name="logger"></param>
    /// <param name="retry"></param>
    /// <returns></returns>
    public static async Task SeedAsync(HotelUColombiaContext generalContext,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            #region Seed Booking data
            /// es un proceso asyncronico que nos permite agregar registros a la Base de datos uan vez a sido creada
            // aca agregamos 2 reservas
            if (!await generalContext.Booking.AnyAsync())
            {
                await generalContext.Booking.AddRangeAsync(
                GetPreconfiguredBooking());

                await generalContext.SaveChangesAsync();
            }
            #endregion

            #region Seed Rooms data
            // aca agregamos 3 tipos de habitacions
            if (!await generalContext.Rooms.AnyAsync())
            {
                await generalContext.Rooms.AddRangeAsync(
                GetPreconfiguredRooms());

                await generalContext.SaveChangesAsync();
            }
            #endregion

            #region Seed staus data
            // aca agregamos 3 tipos de habitacions
            if (!await generalContext.StatusBooking.AnyAsync())
            {
                await generalContext.StatusBooking.AddRangeAsync(
                GetPreconfiguredStatus());

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
                    ValorTotal = 120.000,
                    IdUsuario = 1,
                },
                new Booking
                {
                    IdCliente = 2,
                    IdRoom = 2,
                    CreatedDate = DateTime.Now,
                    PickUpDate = DateTime.Now.AddDays(3),
                    ReturnDate = DateTime.Now.AddDays(5),
                    IdStatus = 1,
                    ValorTotal = 160.000,
                    IdUsuario = 1,
                }
            };
    }

    public static IEnumerable<Rooms> GetPreconfiguredRooms()
    {
        return new List<Rooms>
            {
                new Rooms
                {
                    Id = 1,
                    Category="Economica",
                    Bathroom = false,
                    Freezer = false,
                    TV = false,
                    NumberBeds=1,
                    Price = 80000,
                    Image = "images/Deluxe-1.jpg"
                },
                new Rooms
                {
                    Id = 2,
                    Category="Normal",
                    Bathroom = false,
                    Freezer = false,
                    TV = true,
                    NumberBeds=2,
                    Price = 120000,
                    Image = "images/Deluxe-1.jpg"
                },
                new Rooms
                {
                    Id = 3,
                    Category="Deluxe",
                    Bathroom = true,
                    Freezer = true,
                    TV = true,
                    NumberBeds=3,
                    Price = 200000,
                    Image = "images/Deluxe-1.jpg"
                }
            };
    }

    public static IEnumerable<StatusBooking> GetPreconfiguredStatus()
    {
        return new List<StatusBooking>
            {
                new StatusBooking
                {
                    Status = "Reservado",

                },
                new StatusBooking
                {
                    Status = "Cancelado",

                }
            };
    }





}
