namespace U2_Actividad3.Models.ViewModels;

public class DetallesPeliculasViewModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string NombreOriginal { get; set; } = null!;
    public DateTime? Fecha { get; set; }
    public string Descripcion { get; set; } = null!;
    public IEnumerable<PersonajeModel> Personajes { get; set; } = Enumerable.Empty<PersonajeModel>();
}

public class PersonajeModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}
