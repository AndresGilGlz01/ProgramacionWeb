namespace U1_Actividad2.Models.ViewModels;

public class IndexViewModel
{
    public decimal Cantidad { get; set; }
    public string conversionOrigen { get; set; } = null!;
    public string conversionDestino { get; set; } = null!;
    public string Resultado { get; set; } = null!;
}
