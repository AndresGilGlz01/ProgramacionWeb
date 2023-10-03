using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U2_Actividad1.Models.Entites;
using U2_Actividad1.Models.ViewModels;

namespace U2_Actividad1.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var context = new AnimalesContext();

        var vm = context.Clase
            .Select(x => new IndexViewModel()
            {
                Id = x.Id,
                Clase = x.Nombre ?? "Sin nombre",
                Description = x.Descripcion
            }).ToList();

        return View(vm);
    }
}
