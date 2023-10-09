namespace U2_Actividad2.Models.ViewModels;

public class DetallesViewModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string? OtrosNombres { get; set; }
    public string PaisOrigen { get; set; } = null!;
    public float PesoMinimo { get; set; }
    public float PesoMaximo { get; set; }
    public float AlturaMinima { get; set; }
    public float AlturaMaxima { get; set; }
    public int EsperanzaVida { get; set; }
    public int NivelEnergia { get; set; }
    public int FacilidadEntrenamiento { get; set; }
    public int EjericioObligatorio { get; set; }
    public int AmistadDesconocidos { get; set; }
    public int AmistadPerros { get; set; }
    public int NecesitadCepillado { get; set; }
    public string? Patas { get; set; }
    public string Cola { get; set; } = null!;
    public string Hocico { get; set; } = null!;
    public string Pelo { get; set; } = null!;
    public string Color { get; set; } = null!;
    public IEnumerable<RazaModel> OtrasRazas { get; set; } = Enumerable.Empty<RazaModel>();
}
