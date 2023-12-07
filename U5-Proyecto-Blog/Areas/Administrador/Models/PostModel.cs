namespace U5_Proyecto_Blog.Areas.Administrador.Models;

public class PostModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Autor { get; set; } = null!;
    public int CantidadCategorias { get; set; }
    public bool Pertenece { get; set; }
}
