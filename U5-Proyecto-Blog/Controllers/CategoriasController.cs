using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;

using U5_Proyecto_Blog.Models;
using U5_Proyecto_Blog.Models.ViewModels.Categorias;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Controllers;

[Authorize]
public class CategoriasController : Controller
{
    readonly CategoriaRepository _categoriaRepository;

    public CategoriasController(CategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [Route("categorias")]
    public IActionResult Index()
    {
        var viewModel = new IndexViewModel()
        {
            CategoriaSeleccionada = "Todas",
            CategoriasDisponibles = _categoriaRepository
                .GetAll()
                .Select(c => c.Nombre),
            Categorias = _categoriaRepository
                .GetAll()
                .Select(c => new CategoriaModel
                {
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion ?? "Sin descripcion",
                    Posts = c.Postcategoria
                        .Select(pc => new PostModel
                        {
                            Id = pc.IdPostNavigation.Id,
                            Titulo = pc.IdPostNavigation.Titulo
                        }),
                })
        };

        return View(viewModel);
    }

    [Route("categoria/{id}")]
    public IActionResult Categoria(string id)
    {
        id = id.Replace("-", " ");

        var categoria = _categoriaRepository.GetByName(id);

        if (categoria is null) return RedirectToAction(nameof(Index));

        var viewModel = new CategoriaViewModel
        {
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion ?? "Sin descripcion",
            Posts = categoria.Postcategoria
                .Select(pc => new PostModel
                {
                    Id = pc.IdPostNavigation.Id,
                    Titulo = pc.IdPostNavigation.Titulo
                }),
        };

        return View(viewModel);
    }
}
