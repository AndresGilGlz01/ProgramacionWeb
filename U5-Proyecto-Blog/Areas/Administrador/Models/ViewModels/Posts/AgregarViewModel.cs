namespace U5_Proyecto_Blog.Areas.Administrador.Models.ViewModels.Posts;

public class AgregarViewModel
{
    public string Titulo { get; set; } = null!;
    public CategoriaModel[] Categorias { get; set; } = null!;
    public string Contenido { get; set; } = null!;
    public IFormFile? Archivo { get; set; }
}
