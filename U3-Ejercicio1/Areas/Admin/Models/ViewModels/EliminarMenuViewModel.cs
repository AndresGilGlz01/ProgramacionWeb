using U3_Ejercicio1.Models.Entities;

namespace U3_Ejercicio1.Areas.Admin.Models.ViewModels;

public class EliminarMenuViewModel
{
    public int IdMenu { get; set; }
    public string Nombre { get; set; } = null!;
}
