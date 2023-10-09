namespace U2_Actividad2.Models.ViewModels;

public class PaisesViewModel
{
    public IEnumerable<PaisModel> Paises { get; set; } = Enumerable.Empty<PaisModel>();
}

public class PaisModel
{
    public string Nombre { get; set; } = null!;
    public IEnumerable<RazaModel> Razas { get; set; } = Enumerable.Empty<RazaModel>();
}
