using Microsoft.EntityFrameworkCore;

using U3_Actividad2.Models.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<fruteriashopContext>(opt =>
{
    opt.UseMySQL("server=localhost;password=root;user=root;database=fruteriashop");
});

var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
