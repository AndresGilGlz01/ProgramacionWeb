namespace U5_Proyecto_Blog.Models.ViewModels.Home;

public class SignupViewModel
{
    public string NombreUsuario { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Contraseña { get; set; } = null!;
    public string ConfirmarContraseña { get; set; } = null!;
}
