using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using U5_Proyecto_Blog.Areas.Administrador.Models;
using U5_Proyecto_Blog.Areas.Administrador.Models.ViewModels.Usuarios;
using U5_Proyecto_Blog.Models.Entities;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Areas.Administrador.Controllers;

[Authorize(Roles = "Administrador")]
[Area("Administrador")]
public class UsuariosController : Controller
{
    readonly UsuarioRepository _usuarioRepository;

    public UsuariosController(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [Route("administrar/usuarios")]
    public IActionResult Index()
    {
        var viewModel = new IndexViewModel()
        {
            Usuarios = _usuarioRepository
                .GetAll()
                .Select(u => new UsuarioModel
                {
                    Id = u.Id,
                    Nombre = u.NombreUsuario,
                    Email = u.Email,
                })
        };

        return View(viewModel);
    }

    [Route("administrar/usuarios/agregar")]
    public IActionResult Agregar() => View();

    [Route("administrar/usuarios/agregar")]
    [HttpPost]
    public IActionResult Agregar(AgregarViewModel viewModel)
    {
        if (string.IsNullOrWhiteSpace(viewModel.NombreUsuario))
            ModelState.AddModelError(string.Empty, "El nombre es requerido.");

        if (string.IsNullOrWhiteSpace(viewModel.Email))
            ModelState.AddModelError(string.Empty, "El email es requerido.");

        if (string.IsNullOrWhiteSpace(viewModel.Password))
            ModelState.AddModelError(string.Empty, "La contraseña es requerida.");

        if (_usuarioRepository.ExisteUsuario(viewModel.NombreUsuario))
            ModelState.AddModelError(string.Empty, "Ya existe un usuario con ese nombre.");

        if (_usuarioRepository.ExisteEmail(viewModel.Email))
            ModelState.AddModelError(string.Empty, "Ya existe un usuario con ese email.");

        if (!ModelState.IsValid)
            return View(viewModel);

        var entity = new Usuario
        {
            NombreUsuario = viewModel.NombreUsuario,
            Email = viewModel.Email,
            Password = viewModel.Password,
            IdRol = viewModel.RolId,
            Activo = 1,
            EmailConfirmed = 1,
        };

        _usuarioRepository.Insert(entity);
        
        return RedirectToAction(nameof(Index));
    }
}
