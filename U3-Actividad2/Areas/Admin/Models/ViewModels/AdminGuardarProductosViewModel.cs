using U3_Actividad2.Models.Entities;

namespace U3_Actividad2.Areas.Admin.Models.ViewModels;

public class AdminGuardarProductosViewModel
{
    public Productos Producto { get; set; } = null!;
    public IEnumerable<CategoriaModel> Categorias { get; set; } = null!;
    public IFormFile Archivo { get; set; } = null!;
}
