using Microsoft.AspNetCore.Mvc;
using U1_Actividad1.Models.ViewModels;

namespace U1_Actividad1.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        var indexViewModel = new IndexViewModel()
        {
            Nombre = "Andres Gil Gonzalez Ortiz",
            NombreMateria = "Programacion Web",
            Periodo = "AGODIC2023-07",
            Semestre = "7mo"
        };

        return View(indexViewModel);
    }

    public IActionResult MiPerfil()
    {
        var miperfilViewModel = new MiPerfilViewModel()
        {
            NombreCompleto = "Andres Gil Gonzalez Ortiz",
            NumeroControl = "201G0257"
        };

        return View(miperfilViewModel);
    }
}
