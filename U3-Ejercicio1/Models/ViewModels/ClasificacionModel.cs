namespace U3_Ejercicio1.Models.ViewModels;

public class ClasificacionModel
{
    public string Nombre { get; set; } = null!;
    public IEnumerable<HamburguesaModel> Hamburguesas { get; set; } = Enumerable.Empty<HamburguesaModel>();
}
