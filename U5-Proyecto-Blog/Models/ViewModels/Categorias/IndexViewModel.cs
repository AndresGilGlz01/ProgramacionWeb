namespace U5_Proyecto_Blog.Models.ViewModels.Categorias;

public class IndexViewModel
{
    public string CategoriaSeleccionada { get; set; } = string.Empty;
    public IEnumerable<string> CategoriasDisponibles { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<CategoriaModel> Categorias { get; set; } = Enumerable.Empty<CategoriaModel>();
}
