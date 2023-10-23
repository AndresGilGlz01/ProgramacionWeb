namespace Villancicos.Models;

public partial class Villancico
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Compositor { get; set; } = null!;
    public int? Anyo { get; set; }
    public string Letra { get; set; } = null!;
    public string VideoUrl { get; set; } = null!;
}
