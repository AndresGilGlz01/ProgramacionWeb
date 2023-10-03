using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U2_Actividad1.Models.Entites;
using U2_Actividad1.Models.ViewModels;

namespace U2_Actividad1.Controllers;

public class EspeciesController : Controller
{
    public IActionResult Index()
    {
        var context = new AnimalesContext();

        var vm = context.Clase
            .Include(x => x.Especies)
            .Select(x => new EspeciesViewModel()
            {
                IdClase = x.Id,
                Clase = x.Nombre ?? "Sin nombre",
                Especies = x.Especies.Select(y => new Especie()
                {
                    IdEspecie = y.Id,
                    Nombre = y.Especie
                })
            }).ToList();

        return View(vm);
    }

    public IActionResult Especie(string Id)
    {
        Id = Id.Replace("-", " ");

        var context = new AnimalesContext();
        var vm = context.Especies
            .Where(x => x.Especie == Id)
            .Include(x => x.IdClaseNavigation)
            .Select(x => new EspecieViewModel()
            {
                Id = x.Id,
                Peso = x.Peso,
                Tamaño = x.Tamaño,
                Nombre = x.Especie,
                Habitat = x.Habitat ?? "Sin habitat registrada",
                Clase = x.IdClaseNavigation!.Nombre ?? "Sin clase",
                Descripcion = x.Observaciones ?? "Sin observaciones registradas"
            }).FirstOrDefault();

        return View(vm);
    }

    public IActionResult Clase(string Id)
    {
        var context = new AnimalesContext();
        var vm = context.Clase
            .Where(x => x.Nombre == Id)
            .Include(x => x.Especies)
            .Select(x => new EspeciesViewModel()
            {
                IdClase = x.Id,
                Clase = x.Nombre ?? "Sin nombre",
                Especies = x.Especies.Select(y => new Especie()
                {
                    IdEspecie = y.Id,
                    Nombre = y.Especie
                })
            }).FirstOrDefault();

        return View(vm);
    }
}
