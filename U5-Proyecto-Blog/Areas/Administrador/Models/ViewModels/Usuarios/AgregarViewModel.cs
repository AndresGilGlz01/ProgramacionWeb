namespace U5_Proyecto_Blog.Areas.Administrador.Models.ViewModels.Usuarios;

public class AgregarViewModel
{
    public string NombreUsuario { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmarPassword { get; set; } = null!;
    public int RolId { get; set; }
    public IEnumerable<RolModel> Roles { get; set; } = null!;
}
