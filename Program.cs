using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HotelUColombia.Data;
using It270.MedicalManagement.Accounting.Infrastructure.Data;

namespace HotelUColombia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Dependencies.ConfigureServices(builder.Configuration, builder.Services);

            builder.Services.AddDbContext<HotelUColombiaContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("HotelUColombiaContext") ?? throw new InvalidOperationException("Connection string 'HotelUColombiaContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
           
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}