namespace U3_Ejercicio1.Models.ViewModels;

public class HamburguesaModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public bool Seleccionado { get; set; }
    public decimal Precio { get; set; }
}
