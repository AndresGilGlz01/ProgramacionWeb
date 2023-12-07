using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using U5_Proyecto_Blog.Models.Entities;
using U5_Proyecto_Blog.Repositories;
using U5_Proyecto_Blog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddDbContext<BlogContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), c => c.CommandTimeout(180));
});
builder.Services.AddTransient<UsuarioRepository>();
builder.Services.AddTransient<PostRepository>();
builder.Services.AddTransient<CategoriaRepository>();
builder.Services.AddTransient<Repository<Postcategoria>>();
builder.Services.AddTransient<Repository<Rol>>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "BlogCk";
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.AccessDeniedPath = "/AccessDenied";
});

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
        name: "default",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();
app.Run();
