using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using U3_Actividad2.Models.Entities;
using U3_Actividad2.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddTransient<Repository<Categorias>>();
builder.Services.AddTransient<Repository<Usuarios>>();
builder.Services.AddTransient<ProductosRepository>();
builder.Services.AddDbContext<fruteriashopContext>(opt =>
{
    opt.UseMySQL("server=localhost;password=root;user=root;database=fruteriashop");
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(c =>
{
    c.AccessDeniedPath = "/Home/Denied";
    c.LoginPath = "/Home/Login";
    c.LogoutPath = "/Home/Logout";
    c.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    c.Cookie.Name = "fruteriaCookie369";
});

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapDefaultControllerRoute();
app.Run();
