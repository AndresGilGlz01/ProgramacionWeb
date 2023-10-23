using Microsoft.AspNetCore.Mvc;
using U2_Actividad4.Models.ViewModels;
using U2_Actividad4.Models.Entities;

namespace U2_Actividad4.Controllers;

public class HomeController : Controller
{
    MapaCurricularContext _context = new();

    public IActionResult Index()
    {
        var viewModel = new IndexViewModel
        {
            Carreras = _context.Carreras
                .OrderBy(c => c.Nombre)
                .Select(c => new CarreraModel
                {
                    Plan = c.Plan,
                    Nombre = c.Nombre
                })
        };

        return View(viewModel);
    }

    [Route("Info/{nombre_carrera}")]
    public IActionResult Info(string nombre_carrera)
    {
        nombre_carrera = nombre_carrera.Replace("-", " ");

        var existe = _context.Carreras.Any(c => c.Nombre.ToLower() == nombre_carrera);

        if (existe is false) return RedirectToAction("Index");

        var viewModel = _context.Carreras
            .Where(c => c.Nombre.ToLower() == nombre_carrera)
            .Select(c => new InfoViewModel
            {
                Nombre = c.Nombre,
                Descripcion = c.Descripcion ?? "Sin descripción.",
                Plan = c.Plan,
                Especialidad = c.Especialidad,
                Id = c.Id
            })
            .First();

        return View(viewModel);
    }

    [Route("Mapa/{nombre_carrera}")]
    public IActionResult Mapa(string nombre_carrera)
    {
        nombre_carrera = nombre_carrera.Replace("-", " ");

        var existe = _context.Carreras.Any(c => c.Nombre.ToLower() == nombre_carrera);

        if (existe is false) return RedirectToAction("Index");

        var viewModel = _context.Carreras
            .Where(c => c.Nombre.ToLower() == nombre_carrera)
            .Select(c => new MapaViewModel
            {
                NombreCarrera = c.Nombre,
                Plan = c.Plan,
                TotalCreditos = _context.Materias
                    .Where(m => m.IdCarreraNavigation.Nombre == c.Nombre)
                    .Sum(m => m.Creditos),
                Semestres = _context.Materias
                    .Where(m => m.IdCarreraNavigation.Nombre == c.Nombre)
                    .GroupBy(m => m.Semestre)
                    .OrderBy(s => s.Key)
                    .Select(s => new SemestreModel
                    {
                        Numero = s.Key,
                        Materias = s.Select(m => new MateriaModel
                        {
                            Clave = m.Clave,
                            Nombre = m.Nombre,
                            HorasTeoricas = m.HorasTeoricas,
                            HorasPracticas = m.HorasPracticas,
                            Creditos = m.Creditos
                        })
                    })
                    .ToList()
            })
            .First();

        return View(viewModel);
    }
}
