namespace U3_Ejercicio1.Areas.Admin.Models.ViewModels;

public class MenuViewModel
{
    public IEnumerable<ClasificacionModel> Clasificaciones { get; set; } = Enumerable.Empty<ClasificacionModel>();
}
