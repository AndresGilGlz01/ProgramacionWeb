namespace U2_Actividad2.Models.ViewModels;

public class IndexViewModel
{
    public IEnumerable<string> Iniciales { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<RazaModel> Razas { get; set; } = Enumerable.Empty<RazaModel>();
}

public class RazaModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}
