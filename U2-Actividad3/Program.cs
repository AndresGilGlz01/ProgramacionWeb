namespace U2_Actividad3;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddMvc();
        
        var app = builder.Build();

        app.UseStaticFiles();
        app.MapDefaultControllerRoute();
        app.Run();
    }
}
