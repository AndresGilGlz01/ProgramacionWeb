namespace U5_Proyecto_Blog.Areas.Administrador.Models;

public class CategoriaModel
{
    public int IdCategoria { get; set; }
    public string Nombre { get; set; } = null!;
    public bool Seleccionada { get; set; }
}
