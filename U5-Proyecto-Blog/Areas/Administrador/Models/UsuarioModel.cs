namespace U5_Proyecto_Blog.Areas.Administrador.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Rol{ get; set; } = null!;
}
