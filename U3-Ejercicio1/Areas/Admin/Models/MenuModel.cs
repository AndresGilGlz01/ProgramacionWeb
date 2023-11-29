namespace U3_Ejercicio1.Areas.Admin.Models;

public class MenuModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public decimal PrecioOriginal { get; set; }
    public decimal? PrecioNuevo { get; set; }
    public string Descripcion { get; set; } = null!;
}
