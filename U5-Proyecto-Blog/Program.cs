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
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddTransient<UsuarioRepository>();
builder.Services.AddTransient<PostRepository>();
builder.Services.AddTransient<CategoriaRepository>();
builder.Services.AddTransient<Repository<Postcategoria>>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.Cookie.Name = "BlogCk";
    opt.LoginPath = "/Login";
    opt.LogoutPath = "/Logout";
    opt.ExpireTimeSpan = TimeSpan.FromDays(30);
    opt.AccessDeniedPath = "/Home/";
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
