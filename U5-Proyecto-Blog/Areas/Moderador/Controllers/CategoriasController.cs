using Microsoft.AspNetCore.Mvc;

using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Areas.Moderador.Controllers;

public class CategoriasController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Categoria categoria)
    {
        return View();
    }

    public IActionResult Modificar(string Id)
    {
        return View();
    }

    [HttpPost]
    public IActionResult Modificar(Categoria categoria)
    {
        return View();
    }

    public IActionResult Eliminar(string Id)
    {
        return View();
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        return View();
    }
}
