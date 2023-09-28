using Microsoft.AspNetCore.Mvc;
using U1_Actividad2.Models.ViewModels;

namespace U1_Actividad2.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(IndexViewModel vm)
    {
        if (vm.Conversion == null)
        {
            vm.Conversion = "MX";
            return View(vm);
        }

        if (vm.Conversion == "MX")
        {
            vm.Resultado = vm.Cantidad / 18m;
        }
        else if (vm.Conversion == "USD") {
            vm.Resultado = vm.Cantidad * 18m;
        }

        return View(vm);
    }
}
