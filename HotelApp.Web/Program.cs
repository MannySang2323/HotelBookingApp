using HotelLibrary.DataAccess;
using HotelLibrary.Databases;

namespace HotelApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Register the service - Setup concrete classes to use via DI //
            // - What data access class and database class to use
            builder.Services.AddTransient<IDataBaseAccess, SqlDataBaseAccess>();
            builder.Services.AddTransient<IHotelDataAccess, HotelSqlDataAccess>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}