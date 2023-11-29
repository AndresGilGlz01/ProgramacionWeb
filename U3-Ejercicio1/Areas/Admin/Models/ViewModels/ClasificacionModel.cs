namespace U3_Ejercicio1.Areas.Admin.Models.ViewModels;

public class ClasificacionModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public IEnumerable<MenuModel> Menus { get; set; } = Enumerable.Empty<MenuModel>();
}
