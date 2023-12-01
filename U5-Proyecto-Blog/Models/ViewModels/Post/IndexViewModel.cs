namespace U5_Proyecto_Blog.Models.ViewModels.Post;

public class IndexViewModel
{
    public IEnumerable<RecomendadoPostModel> PostRecomendados { get; set; } = Enumerable.Empty<RecomendadoPostModel>();
    public IEnumerable<UltimoPostModel> UltimosPost { get; set; } = Enumerable.Empty<UltimoPostModel>();
    public IEnumerable<CategoriaModel> Categorias { get; set; } = Enumerable.Empty<CategoriaModel>();
}

public class RecomendadoPostModel
{
    public int Id { get; set; }
    public string Imagen { get; set; } = null!;
    public string Titulo { get; set; } = null!;
    public string Creador { get; set; } = null!;
    public DateTime Fecha { get; set; }
}

public class UltimoPostModel
{
    public int Id { get; set; }
    public string Imagen { get; set; } = null!;
    public string Titulo { get; set; } = null!;
    public string Creador { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateTime Fecha { get; set; }
}
