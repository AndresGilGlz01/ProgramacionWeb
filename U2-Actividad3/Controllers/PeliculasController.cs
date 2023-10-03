using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U2_Actividad3.Models.Entities;
using U2_Actividad3.Models.ViewModels;

namespace U2_Actividad3.Controllers;

public class PeliculasController : Controller
{
    public IActionResult Index()
    {
        var context = new PixarContext();

        var peliculas = context.Pelicula
            .OrderBy(x => x.Nombre)
            .Select(x => new IndexPeliculasViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre ?? "Sin titulo"
            });

        return View(peliculas);
    }

    public IActionResult Detalles(string Id)
    {
        Id = Id.Replace("-", " ");

        var context = new PixarContext();

        var vm = context.Pelicula
            .Where(x => x.Nombre == Id)
            .Include(x => x.Apariciones)
            .Select(x => new DetallesPeliculasViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre ?? "Sin titulo",
                NombreOriginal = x.NombreOriginal ?? "Sin titulo",
                Fecha = x.FechaEstreno,
                Descripcion = x.Descripción ?? "Sin descripcion",
                Personajes = x.Apariciones.Select(x => new PersonajeModel
                {
                    Id = x.IdPersonajeNavigation.Id,
                    Nombre = x.IdPersonajeNavigation.Nombre ?? "Sin nombre"
                })
            })
            .FirstOrDefault();

        return View(vm);
    }
}
