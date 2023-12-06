using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using U5_Proyecto_Blog.Areas.Administrador.Models;
using U5_Proyecto_Blog.Areas.Administrador.Models.ViewModels.Categorias;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Areas.Administrador.Controllers;

[Authorize(Roles = "Administrador")]
[Area("Administrador")]
public class CategoriasController : Controller
{
    readonly CategoriaRepository _categoriaRepository;

    public CategoriasController(CategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [Route("administrar/categorias")]
    public IActionResult Index()
    {
        var viewModel = new IndexViewModel()
        {
            Categorias = _categoriaRepository
                .GetAll()
                .Select(c => new CategoriaModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                })
        };

        return View(viewModel);
    }

    [Route("administrar/categorias/agregar")]
    public IActionResult Agregar() => View();

    [Route("administrar/categorias/agregar")]
    [HttpPost]
    public IActionResult Agregar(AgregarViewModel viewModel)
    {
        if (string.IsNullOrWhiteSpace(viewModel.Nombre))
            ModelState.AddModelError(nameof(viewModel.Nombre), "El nombre es requerido.");

        if (string.IsNullOrWhiteSpace(viewModel.Descripcion))
            ModelState.AddModelError(nameof(viewModel.Descripcion), "La descripción es requerida.");

        if (_categoriaRepository.ExisteCategoria(viewModel.Nombre))
            ModelState.AddModelError(nameof(viewModel.Nombre), "Ya existe una categoría con ese nombre.");

        if (!ModelState.IsValid)
            return View(viewModel);
        
        return RedirectToAction(nameof(Index));
    }

    [Route("administrar/categorias/editar/{id}")]
    public IActionResult Editar(int id)
    {
        var categoria = _categoriaRepository.GetById(id);

        if (categoria == null) 
            return RedirectToAction(nameof(Index));

        var viewModel = new EditarViewModel()
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion ?? string.Empty,
        };

        return View(viewModel);
    }

    [Route("administrar/categorias/editar")]
    [HttpPost]
    public IActionResult Editar(EditarViewModel viewModel)
    {
        if (string.IsNullOrWhiteSpace(viewModel.Nombre))
            ModelState.AddModelError(nameof(viewModel.Nombre), "El nombre es requerido.");

        if (string.IsNullOrWhiteSpace(viewModel.Descripcion))
            ModelState.AddModelError(nameof(viewModel.Descripcion), "La descripción es requerida.");

        if (!ModelState.IsValid)
            return View(viewModel);

        var categoria = _categoriaRepository.GetById(viewModel.Id);

        if (categoria == null)
            return RedirectToAction(nameof(Index));

        categoria.Nombre = viewModel.Nombre;
        categoria.Descripcion = viewModel.Descripcion;

        _categoriaRepository.Update(categoria);

        return RedirectToAction(nameof(Index));
    }

    [Route("administrar/categorias/eliminar/{id}")]
    public IActionResult Eliminar(int id)
    {
        var categoria = _categoriaRepository.GetById(id);

        if (categoria == null)
            return RedirectToAction(nameof(Index));

        var viewModel = new EliminarViewModel()
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
        };

        return View(viewModel);
    }

    [Route("administrar/categorias/eliminar")]
    [HttpPost]
    public IActionResult Eliminar(EliminarViewModel viewModel)
    {
        var categoria = _categoriaRepository.GetById(viewModel.Id);

        if (categoria == null)
            return RedirectToAction(nameof(Index));

        _categoriaRepository.Delete(categoria);

        return RedirectToAction(nameof(Index));
    }
}
