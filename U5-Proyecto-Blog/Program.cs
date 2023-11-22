using U5_Proyecto_Blog.Models.Entities;
using U5_Proyecto_Blog.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<BlogsContext>();
builder.Services.AddTransient<UsuarioRepository>();
builder.Services.AddTransient<PostRepository>();
builder.Services.AddTransient<CategoriaRepository>();
builder.Services.AddTransient<Repository<Postcategoria>>();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
        name: "default",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();
app.Run();
