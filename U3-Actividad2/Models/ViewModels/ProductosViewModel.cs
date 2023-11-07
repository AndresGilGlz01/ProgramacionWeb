namespace U3_Actividad2.Models.ViewModels;

public class ProductosViewModel
{
    public string Categoria { get; set; } = null!;
    public IEnumerable<ProductoModel> Productos = Enumerable.Empty<ProductoModel>();
}

public class ProductoModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public decimal Precio { get; set; }
}
