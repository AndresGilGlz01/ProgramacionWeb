namespace U5_Proyecto_Blog.Areas.Administrador.Models.ViewModels.Usuarios;

public class EditarViewModel
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int RolId { get; set; }
    public IEnumerable<RolModel> Roles { get; set; } = Enumerable.Empty<RolModel>();
}
