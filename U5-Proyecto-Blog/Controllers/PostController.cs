using Microsoft.AspNetCore.Mvc;
using U5_Proyecto_Blog.Models.Entities;
using U5_Proyecto_Blog.Models.ViewModels.Post;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Controllers;

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
                })
        };

        return View(viewModel);
    }

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
    public IActionResult Create(GuardarPostViewModel vm)
    {
        if (string.IsNullOrEmpty(vm.Titulo)) ModelState.AddModelError(string.Empty, "El título es requerido");

        if (string.IsNullOrEmpty(vm.Contenido)) ModelState.AddModelError(string.Empty, "El contenido es requerido");

        if (_postRepository.Exist(vm.Titulo)) ModelState.AddModelError(string.Empty, "Titulo de articulo no disponible");

        if (!ModelState.IsValid) return View(vm);
        
        var entity = new Post
        {
            Titulo = vm.Titulo,
            Contenido = vm.Contenido,
            IdCreador = 1,
            FechaPublicacion = DateTime.Now,
            FechaActualizacion = DateTime.Now,
            Postcategoria = vm.Categorias.Where(c => c.Seleccionada)
                .Select(c => new Postcategoria
                {
                    IdCategoria = c.IdCategoria
                }).ToList()
        };

        _postRepository.Insert(entity);
            
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(string Id)
    {
        Id = Id.Replace("-", " ");

        var post = _postRepository.GetByTitulo(Id);

        if (post is null) return RedirectToAction(nameof(Index));

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

    [HttpPost]
    public IActionResult Edit(GuardarPostViewModel viewModel)
    {
        var entity = _postRepository.GetById(viewModel.IdPost);

        if (entity is null) return RedirectToAction(nameof(Index));

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

    public IActionResult Delete(int id)
    {
        return View();
    }

    [HttpPost]
    public IActionResult Delete(Post post)
    {
        return View();
    }
}
