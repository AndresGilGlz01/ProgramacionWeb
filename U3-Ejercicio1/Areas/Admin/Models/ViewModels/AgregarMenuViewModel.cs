namespace U3_Ejercicio1.Areas.Admin.Models.ViewModels;

public class AgregarMenuViewModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public decimal Precio { get; set; }
    public string Descripcion { get; set; } = null!;
    public int IdClasificacion { get; set; }
    public IEnumerable<ClasificacionModel> Clasificaciones { get; set; } = Enumerable.Empty<ClasificacionModel>();
    public IFormFile? Archivo { get; set; }
}
