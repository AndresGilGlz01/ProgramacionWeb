using Microsoft.EntityFrameworkCore;
using U3_Actividad2.Models.Entities;
using U3_Actividad2.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddTransient<Repository<Categorias>>();
builder.Services.AddTransient<ProductosRepository>();
builder.Services.AddDbContext<fruteriashopContext>(opt =>
{
    opt.UseMySQL("server=localhost;password=root;user=root;database=fruteriashop");
});

var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapDefaultControllerRoute();
app.Run();
