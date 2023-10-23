var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(); // Agregar MVC al proyecto 

var app = builder.Build();
app.UseStaticFiles(); // Habilitar el uso de archivos estáticos
app.MapDefaultControllerRoute(); // Habilitar el uso de MVC y el ruteo por defecto
app.Run();
