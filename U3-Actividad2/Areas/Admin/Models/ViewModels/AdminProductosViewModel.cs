namespace U3_Actividad2.Areas.Admin.Models.ViewModels;

public class AdminProductosViewModel
{
    public int SelectedCategoriaId { get; set; }
    public IEnumerable<CategoriaModel> Categorias { get; set; } = null!;
    public IEnumerable<ProductoModel> Productos { get; set; } = null!;
}

public class ProductoModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
}
