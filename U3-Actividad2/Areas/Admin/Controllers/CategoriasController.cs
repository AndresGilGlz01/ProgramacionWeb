using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using U3_Actividad2.Areas.Admin.Models.ViewModels;
using U3_Actividad2.Models.Entities;
using U3_Actividad2.Repositories;

namespace U3_Actividad2.Areas.Admin.Controllers;

[Authorize(Roles = "Administrador")]
[Area("Admin")]
public class CategoriasController : Controller
{
    Repository<Categorias> _repository;

    public CategoriasController(Repository<Categorias> repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var viewModel = new AdminCategoriaViewModel
        {
            Categorias = _repository
                .GetAll()
                .Select(c => new CategoriaModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre ?? "Sin nombre"
                })
        };

        return View(viewModel);
    }

    public IActionResult Agregar() => View();

    [HttpPost]
    public IActionResult Agregar(Categorias categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria.Nombre)) ModelState.AddModelError("", "Escriba el nombre de la categoria.");

        if (!ModelState.IsValid) return View(categoria);

        _repository.Insert(categoria);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Editar(int Id)
    {
        var entity = _repository.GetById(Id);

        if (entity is null) return RedirectToAction(nameof(Index));

        return View(entity);
    }

    [HttpPost]
    public IActionResult Editar(Categorias categoria)
    {
        var entity = _repository.GetById(categoria.Id);

        if (entity is null) return RedirectToAction(nameof(Index));

        if (string.IsNullOrWhiteSpace(categoria.Nombre)) 
            ModelState.AddModelError(string.Empty, "Escriba el nombre de la categoria.");

        if (!ModelState.IsValid) return View(entity);

        entity.Nombre = categoria.Nombre;
        _repository.Update(entity);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Eliminar(int Id)
    {
        var entity = _repository.GetById(Id);

        if (entity is null) return RedirectToAction(nameof(Index));

        return View(entity);
    }

    [HttpPost]
    public IActionResult Eliminar(Categorias categoria)
    {
        var entity = _repository.GetById(categoria.Id);

        if (entity is not null) _repository.Delete(entity);

        // validar que no tenga productos asociados

        return RedirectToAction(nameof(Index));
    }
}
