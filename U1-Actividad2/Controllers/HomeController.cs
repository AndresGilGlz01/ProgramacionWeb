using Microsoft.AspNetCore.Mvc;
using U1_Actividad2.Models.ViewModels;

namespace U1_Actividad2.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(IndexViewModel vm)
    {
        if (vm.conversionOrigen == "MXN" && vm.conversionDestino == "USD")
        {
            vm.Resultado = (vm.Cantidad / 18m).ToString($"$0.00 USD");
        }
        else if (vm.conversionOrigen == "USD" && vm.conversionDestino == "MXN")
        {
            vm.Resultado = (vm.Cantidad * 18m).ToString($"$0.00 MXN");
        }

        if (vm.conversionOrigen == "MXN" && vm.conversionDestino == "MXN")
        {
            vm.Resultado = vm.Cantidad.ToString($"$0.00 MXN");
        }
        else if (vm.conversionOrigen == "USD" && vm.conversionDestino == "USD")
        {
            vm.Resultado = vm.Cantidad.ToString($"$0.00 USD");
        }

        return View(vm);
    }
}
