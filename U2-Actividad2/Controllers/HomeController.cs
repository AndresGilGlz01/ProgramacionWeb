using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U2_Actividad2.Models.Entities;
using U2_Actividad2.Models.ViewModels;

namespace U2_Actividad2.Controllers;

public class HomeController : Controller
{
    private readonly PerrosContext _context = new();

    [Route("/")]
    [Route("[controller]/Letra/{Id?}")]
    public IActionResult Index(string? Id)
    {
        if (string.IsNullOrEmpty(Id))
        {
            var viewModel = new IndexViewModel
            {
                Iniciales = _context.Razas
                    .Select(r => r.Nombre.Substring(0, 1))
                    .Distinct()
                    .OrderBy(r => r),
                Razas = _context.Razas
                .OrderBy(r => r.Nombre)
                .Select(r => new RazaModel
                {
                    Id = (int)r.Id,
                    Nombre = r.Nombre
                })
            };
            return View(viewModel);
        }
        else
        {
            var viewModel = new IndexViewModel
            {
                Iniciales = _context.Razas
                    .Select(r => r.Nombre.Substring(0, 1))
                    .Distinct()
                    .OrderBy(r => r),
                Razas = _context.Razas
                    .Where(r => r.Nombre.StartsWith(Id))
                    .Select(r => new RazaModel
                    {
                        Id = (int)r.Id,
                        Nombre = r.Nombre
                    })
            };
            return View(viewModel);
        }
    }

    [Route("Raza/{nombre_raza}")]
    public IActionResult Detalles(string nombre_raza)
    {
        nombre_raza = nombre_raza.Replace('-', ' ');

        var raza = _context.Razas.Any(r => r.Nombre == nombre_raza);

        if (raza is false) return RedirectToAction("Index");

        var viewModel = _context.Razas
            .Where(r => r.Nombre == nombre_raza)
            .Include(r => r.Estadisticasraza)
            .Include(r => r.Caracteristicasfisicas)
            .Include(r => r.IdPaisNavigation)
            .Select(r => new DetallesViewModel
            {
                Id = (int)r.Id,
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                OtrosNombres = r.OtrosNombres,
                PaisOrigen = r.IdPaisNavigation.Nombre ?? "País desconocido",
                PesoMinimo = r.PesoMin,
                PesoMaximo = r.PesoMax,
                AlturaMinima = r.AlturaMin,
                AlturaMaxima = r.AlturaMax,
                EsperanzaVida = (int)r.EsperanzaVida,
                NivelEnergia = (int)r.Estadisticasraza.NivelEnergia,
                FacilidadEntrenamiento = (int)r.Estadisticasraza.FacilidadEntrenamiento,
                EjericioObligatorio = (int)r.Estadisticasraza.EjercicioObligatorio,
                AmistadDesconocidos = (int)r.Estadisticasraza.AmistadDesconocidos,
                AmistadPerros = (int)r.Estadisticasraza.AmistadPerros,
                NecesitadCepillado = (int)r.Estadisticasraza.NecesidadCepillado,
                Patas = r.Caracteristicasfisicas.Patas,
                Cola = r.Caracteristicasfisicas.Cola,
                Hocico = r.Caracteristicasfisicas.Hocico,
                Pelo = r.Caracteristicasfisicas.Pelo,
                Color = r.Caracteristicasfisicas.Color,
                OtrasRazas = _context.Razas
                    .Where(r => r.Nombre != nombre_raza)
                    .Select(r => new RazaModel
                    {
                        Id = (int)r.Id,
                        Nombre = r.Nombre
                    })
                    .Take(4)
                    .ToList()
            })
            .FirstOrDefault();

        return View(viewModel);
    }

    [Route("Paises")]
    public IActionResult Paises()
    {
        var viewModel = new PaisesViewModel
        {
            Paises = _context.Paises
            .OrderBy(p => p.Nombre)
            .Select(p => new PaisModel
            {
                Nombre = p.Nombre,
                Razas = p.Razas
                    .OrderBy(r => r.Nombre)
                    .Select(r => new RazaModel
                    {
                        Id = (int)r.Id,
                        Nombre = r.Nombre
                    })
            })
        };

        return View(viewModel);
    }
}
