namespace U3_Ejercicio1.Models.ViewModels;

public class PromocionesViewModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public decimal PrecioOriginal { get; set; }
    public decimal PrecioNuevo { get; set; }
    public string SiguientePromocion { get; set; } = null!;
    public string AnteriorPromocion { get; set; } = null!;
}
