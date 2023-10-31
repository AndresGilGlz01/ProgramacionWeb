namespace U3_Actividad1.Areas.Admin.Models;

public class AdminIndexViewModel
{
    public IEnumerable<VillancicoModel> Villancicos = Enumerable.Empty<VillancicoModel>();
}

public class VillancicoModel 
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}
