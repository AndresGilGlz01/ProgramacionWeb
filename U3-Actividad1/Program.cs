using Microsoft.EntityFrameworkCore;
using U3_Actividad1.Repositories;
using Villancicos.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<villancicosContext>(opt => {
    opt.UseMySql("server=localhost;user=root;password=root;database=villancicos", 
        ServerVersion.AutoDetect("server=localhost;user=root;password=root;database=villancicos"));
}); // Registrar el contexto como servicio para ser inyectado
builder.Services.AddTransient<VillancicoRepository>();

var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
