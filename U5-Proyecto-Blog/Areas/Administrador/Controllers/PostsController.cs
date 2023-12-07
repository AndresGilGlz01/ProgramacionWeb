using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using U5_Proyecto_Blog.Areas.Administrador.Models;
using U5_Proyecto_Blog.Areas.Administrador.Models.ViewModels.Posts;
using U5_Proyecto_Blog.Models.Entities;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Areas.Administrador.Controllers;

[Authorize(Roles = "Administrador")]
[Area("Administrador")]
public class PostsController : Controller
{
    readonly Repository<Postcategoria> _postCategoria;
    readonly PostRepository _postRepository;
    readonly CategoriaRepository _categoriaRepository;

    public PostsController(PostRepository postRepository,
        CategoriaRepository categoriaRepository,
        Repository<Postcategoria> postCategoria)
    {
        _postRepository = postRepository;
        _categoriaRepository = categoriaRepository;
        _postCategoria = postCategoria;
    }

    [Route("administrar/posts")]
    public IActionResult Index()
    {
        var viewModel = new IndexViewModel
        {
            Posts = _postRepository
                .GetAll()
                .Select(p => new PostModel
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    CantidadCategorias = p.Postcategoria.Count,
                    Autor = p.IdCreadorNavigation.NombreUsuario,
                    Pertenece = p.IdCreadorNavigation.NombreUsuario == User.Identity!.Name
                })
        };

        return View(viewModel);
    }

    [Route("administrar/posts/agregar")]
    public IActionResult Agregar()
    {
        var viewModel = new AgregarViewModel
        {
            Categorias = _categoriaRepository.GetAll()
                .Select(c => new CategoriaModel
                {
                    IdCategoria = c.Id,
                    Nombre = c.Nombre,
                    Seleccionada = false
                }).ToArray()
        };

        return View(viewModel);
    }

    [Route("administrar/posts/agregar")]
    [HttpPost]
    public IActionResult Agregar(AgregarViewModel viewModel)
    {
        if (viewModel.Archivo is not null)
        {
            if (viewModel.Archivo.ContentType != "image/png")
            {
                ModelState.AddModelError(string.Empty, "Solo se aceptan imagenes jpg");
            }
        }

        if (string.IsNullOrEmpty(viewModel.Titulo)) ModelState.AddModelError(string.Empty, "El título es requerido");

        if (string.IsNullOrEmpty(viewModel.Contenido)) ModelState.AddModelError(string.Empty, "El contenido es requerido");

        if (_postRepository.Exist(viewModel.Titulo)) ModelState.AddModelError(string.Empty, "Titulo de articulo no disponible");

        if (!ModelState.IsValid) return View(viewModel);

        var entity = new Post
        {
            Titulo = viewModel.Titulo,
            Contenido = viewModel.Contenido,
            IdCreador = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
            FechaPublicacion = DateTime.Now,
            FechaActualizacion = DateTime.Now,
            Postcategoria = viewModel.Categorias.Where(c => c.Seleccionada)
                .Select(c => new Postcategoria
                {
                    IdCategoria = c.IdCategoria
                }).ToList()
        };

        _postRepository.Insert(entity);

        if (viewModel.Archivo is null)
        {
            System.IO.File.Copy("wwwroot/imagenes/no-disponible.png", $"wwwroot/imagenes/{entity.Id}.png");
        }
        else
        {
            var fs = new FileStream($"wwwroot/imagenes/{entity.Id}.png", FileMode.Create);
            viewModel.Archivo.CopyTo(fs);
            fs.Close();
        }

        return RedirectToAction(nameof(Index));
    }

    [Route("administrar/posts/editar/{id}")]
    public IActionResult Editar(int id)
    {
        var post = _postRepository.GetById(id);

        if (post is null) return RedirectToAction(nameof(Index));

        var IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (post.IdCreador != IdUsuario) return RedirectToAction(nameof(Index));

        var viewModel = new EditarViewModel
        {
            IdPost = post.Id,
            Titulo = post.Titulo,
            Contenido = post.Contenido,
            Categorias = _categoriaRepository.GetAll()
                .Select(c => new CategoriaModel
                {
                    IdCategoria = c.Id,
                    Nombre = c.Nombre,
                    Seleccionada = post.Postcategoria.Any(pc => pc.IdCategoria == c.Id)
                }).ToArray()
        };

        return View(viewModel);
    }

    [Route("administrar/posts/editar")]
    [HttpPost]
    public IActionResult Editar(EditarViewModel viewModel)
    {
        var entity = _postRepository.GetById(viewModel.IdPost);

        if (entity is null) return RedirectToAction(nameof(Index));

        var IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (entity.IdCreador != IdUsuario) return RedirectToAction(nameof(Index));

        entity.Titulo = viewModel.Titulo;
        entity.Contenido = viewModel.Contenido;
        entity.FechaActualizacion = DateTime.Now;

        _postCategoria.GetAll()
            .Where(pc => pc.IdPost == entity.Id)
            .ToList()
            .ForEach(pc => _postCategoria.Delete(pc));

        entity.Postcategoria = viewModel.Categorias
            .Where(c => c.Seleccionada)
            .Select(c => new Postcategoria
            {
                IdCategoria = c.IdCategoria
            }).ToList();

        if (string.IsNullOrEmpty(viewModel.Titulo)) ModelState.AddModelError(string.Empty, "El título es requerido");

        if (string.IsNullOrEmpty(viewModel.Contenido)) ModelState.AddModelError(string.Empty, "El contenido es requerido");

        if (_postRepository.Exist(viewModel.Titulo))
        {
            if (entity.Titulo != viewModel.Titulo) ModelState.AddModelError(string.Empty, "Titulo de articulo no disponible");
        }

        if (!ModelState.IsValid) return View(viewModel);

        _postRepository.Update(entity);
        return RedirectToAction(nameof(Index));
    }

    [Route("administrar/posts/eliminar/{id}")]
    public IActionResult Eliminar(int id)
    {
        var entity = _postRepository.GetById(id);

        if (entity is null) return RedirectToAction(nameof(Index));

        var viewModel = new EliminarViewModel
        {
            Id = entity.Id,
            Titulo = entity.Titulo
        };

        return View(viewModel);
    }

    [Route("administrar/posts/eliminar")]
    [HttpPost]
    public IActionResult Eliminar(EliminarViewModel viewModel)
    {
        var entity = _postRepository.GetById(viewModel.Id);

        if (entity is null) return RedirectToAction(nameof(Index));

        _postRepository.Delete(entity);

        if (System.IO.File.Exists($"wwwroot/imagenes/{entity.Id}.png"))
        {
            System.IO.File.Delete($"wwwroot/imagenes/{entity.Id}.png");
        }

        return RedirectToAction(nameof(Index));
    }
}
