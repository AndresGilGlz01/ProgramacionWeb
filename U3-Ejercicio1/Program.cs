using Microsoft.EntityFrameworkCore;

using U3_Ejercicio1.Models.Entities;
using U3_Ejercicio1.Repositories;

namespace U3_Ejercicio1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddMvc();
        builder.Services.AddDbContext<NeatContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.Parse("8.0.34"));
        });
        builder.Services.AddTransient<MenuRepository>();
        builder.Services.AddTransient<ClasificacionRepository>();

        var app = builder.Build();

        app.UseStaticFiles();
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
        app.MapDefaultControllerRoute();
        app.Run();
    }
}
