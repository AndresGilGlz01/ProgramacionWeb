namespace U2_Actividad4.Models.ViewModels;

public class IndexViewModel
{
    public IEnumerable<CarreraModel> Carreras = Enumerable.Empty<CarreraModel>();
}

public class CarreraModel
{
    public string Nombre { get; set; } = null!;
    public string Plan { get; set; } = null!;
}
