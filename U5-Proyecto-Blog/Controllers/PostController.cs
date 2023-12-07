using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using U5_Proyecto_Blog.Models.Entities;
using U5_Proyecto_Blog.Models.ViewModels;
using U5_Proyecto_Blog.Models.ViewModels.Post;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Controllers;

[Authorize]
public class PostController : Controller
{
    PostRepository _postRepository;
    CategoriaRepository _categoriaRepository;
    Repository<Postcategoria> _postCategoria;

    public PostController(PostRepository postRepository,
        CategoriaRepository categoriaRepository,
        Repository<Postcategoria> postCategoria)
    {
        _postRepository = postRepository;
        _categoriaRepository = categoriaRepository;
        _postCategoria = postCategoria;
    }

    [Route("home")]
    public IActionResult Index()
    {
        var viewModel = new IndexViewModel()
        {
            PostRecomendados = _postRepository.GetAll()
                .OrderBy(p => Guid.NewGuid())
                .Take(3)
                .Select(p => new RecomendadoPostModel
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Imagen = p.Titulo,
                    Fecha = p.FechaPublicacion,
                    Creador = p.IdCreadorNavigation.NombreUsuario
                }),
            UltimosPost = _postRepository.GetAll()
                .OrderByDescending(p => p.FechaPublicacion)
                .Take(5)
                .Select(p => new UltimoPostModel
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Imagen = p.Titulo,
                    Fecha = p.FechaPublicacion,
                    Creador = p.IdCreadorNavigation.NombreUsuario
                }),
            Categorias = _categoriaRepository.GetAll()
                .Select(c => new CategoriaModel
                {
                    Nombre = c.Nombre
                })
        };

        return View(viewModel);
    }

    [Route("post/{id}")]
    public IActionResult Detalles(string id)
    {
        id = id.Replace("-", " ");

        var post = _postRepository.GetByTitulo(id);

        if (post is null) return RedirectToAction(nameof(Index));

        var viewModel = new DetallesViewModel
        {
            Titulo = post.Titulo,
            Contenido = post.Contenido,
            Categorias = post.Postcategoria
                .Select(pc => new CategoriaModel
                {
                    IdCategoria = pc.IdCategoria,
                    Nombre = pc.IdCategoriaNavigation.Nombre
                }),
            Pertenece = post.IdCreadorNavigation.NombreUsuario == User.Identity!.Name
        };

        return View(viewModel);
    }

    [Route("post/buscar/{id}")]
    public IActionResult Buscar(string id)
    {
        id = id.Replace("-", " ");

        var viewModel = new BuscarViewModel
        {
            Resultados = _postRepository.GetAll()
                .Where(p => p.Titulo.Contains(id))
                .Select(p => new PostModel
                {
                    Id = p.Id,
                    Titulo = p.Titulo
                })
        };

        return View(viewModel);
    }

    [Route("crear/post")]
    public IActionResult Create()
    {
        var viewModel = new GuardarPostViewModel()
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

    [HttpPost]
    [Route("crear/post")]
    public IActionResult Create(GuardarPostViewModel viewModel)
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

    [Route("editar/post/{id}")]
    public IActionResult Edit(string id)
    {

        id = id.Replace("-", " ");

        var post = _postRepository.GetByTitulo(id);

        if (post is null) return RedirectToAction(nameof(Index));

        var IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        if (post.IdCreador != IdUsuario) return RedirectToAction(nameof(Index));

        var viewModel = new GuardarPostViewModel
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

    [Route("editar/post")]
    [HttpPost]
    public IActionResult Edit(GuardarPostViewModel viewModel)
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

        if (viewModel.Archivo is not null)
        {
            var fs = System.IO.File.Create($"wwwroot/imagenes/{viewModel.IdPost}.png");
            viewModel.Archivo.CopyTo(fs);
            fs.Close();
        }

        return RedirectToAction(nameof(Index));
    }

    [Route("eliminar/post/{id}")]
    public IActionResult Delete(string id)
    {
        id = id.Replace("-", " ");

        var post = _postRepository.GetByTitulo(id);

        if (post is null) return RedirectToAction(nameof(Index));

        var viewModel = new DeleteViewModel
        {
            IdPost = post.Id,
            Titulo = post.Titulo
        };

        return View(viewModel);
    }

    [HttpPost]
    [Route("eliminar/post")]
    public IActionResult Delete(DeleteViewModel viewModel)
    {
        var entity = _postRepository.GetById(viewModel.IdPost);

        if (entity is null) return RedirectToAction(nameof(Index));

        _postRepository.Delete(entity);

        if (System.IO.File.Exists($"wwwroot/imagenes/{entity.Id}.png"))
            System.IO.File.Delete($"wwwroot/imagenes/{entity.Id}.png");

        return RedirectToAction(nameof(Index));
    }
}
