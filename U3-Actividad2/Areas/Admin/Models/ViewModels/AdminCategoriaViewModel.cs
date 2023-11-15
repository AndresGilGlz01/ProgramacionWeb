namespace U3_Actividad2.Areas.Admin.Models.ViewModels;

public class AdminCategoriaViewModel
{
    public IEnumerable<CategoriaModel> Categorias { get; set; } = Enumerable.Empty<CategoriaModel>();
}

public class CategoriaModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}
