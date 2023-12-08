namespace U5_Proyecto_Blog.Models.ViewModels.Categorias;

public class CategoriaViewModel
{
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public IEnumerable<PostModel> Posts { get; set; } = Enumerable.Empty<PostModel>();
}
