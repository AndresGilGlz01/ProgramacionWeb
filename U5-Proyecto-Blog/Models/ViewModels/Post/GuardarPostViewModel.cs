namespace U5_Proyecto_Blog.Models.ViewModels.Post;

public class GuardarPostViewModel
{
    public int IdPost { get; set; }
    public string Titulo { get; set; } = null!;
    public CategoriaModel[] Categorias { get; set; } = null!;
    public string Contenido { get; set; } = null!;
    public IFormFile? Archivo { get; set; }
}

public class CategoriaModel
{
    public int IdCategoria { get; set; }
    public string Nombre { get; set; } = null!;
    public bool Seleccionada { get; set; }
}
