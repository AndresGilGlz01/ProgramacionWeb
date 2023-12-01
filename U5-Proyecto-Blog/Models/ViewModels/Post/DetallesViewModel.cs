namespace U5_Proyecto_Blog.Models.ViewModels.Post;

public class DetallesViewModel
{
    public string Titulo { get; set; } = null!;
    public string Contenido { get; set; } = null!;
    public IEnumerable<CategoriaModel> Categorias { get; set; } = null!;
}
