using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using U5_Proyecto_Blog.Areas.Administrador.Models;
using U5_Proyecto_Blog.Areas.Administrador.Models.ViewModels.Usuarios;
using U5_Proyecto_Blog.Helpers;
using U5_Proyecto_Blog.Models.Entities;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Areas.Administrador.Controllers;

[Authorize(Roles = "Administrador")]
[Area("Administrador")]
public class UsuariosController : Controller
{
    readonly Repository<Rol> _rolRepository;
    readonly UsuarioRepository _usuarioRepository;

    public UsuariosController(UsuarioRepository usuarioRepository,
        Repository<Rol> rolRepository)
    {
        _usuarioRepository = usuarioRepository;
        _rolRepository = rolRepository;
    }

    [Route("administrar/usuarios")]
    public IActionResult Index()
    {
        var viewModel = new IndexViewModel()
        {
            Usuarios = _usuarioRepository
                .GetAll()
                .Where(u => u.NombreUsuario != User.Identity!.Name)
                .Select(u => new UsuarioModel
                {
                    Id = u.Id,
                    Nombre = u.NombreUsuario,
                    Email = u.Email,
                    Rol = u.IdRolNavigation is null ? "Normal" : u.IdRolNavigation.Nombre,
                })
        };

        return View(viewModel);
    }

    [Route("administrar/usuarios/agregar")]
    public IActionResult Agregar()
    {
        var viewModel = new AgregarViewModel
        {
            Roles = _rolRepository
                .GetAll()
                .Select(r => new RolModel
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                })
        };

        return View(viewModel);
    }

    [Route("administrar/usuarios/agregar")]
    [HttpPost]
    public IActionResult Agregar(AgregarViewModel viewModel)
    {
        if (string.IsNullOrWhiteSpace(viewModel.NombreUsuario))
        {
            ModelState.AddModelError(string.Empty, "El nombre es requerido.");
        }
        else
        {
            if (_usuarioRepository.ExisteUsuario(viewModel.NombreUsuario))
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con ese nombre.");
        }

        if (string.IsNullOrWhiteSpace(viewModel.Email))
        {
            ModelState.AddModelError(string.Empty, "El email es requerido.");
        }
        else
        {
            if (_usuarioRepository.ExisteEmail(viewModel.Email))
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con ese email.");
        }

        if (string.IsNullOrWhiteSpace(viewModel.Password))
            ModelState.AddModelError(string.Empty, "La contraseña es requerida.");

        if (string.IsNullOrWhiteSpace(viewModel.ConfirmarPassword))
            ModelState.AddModelError(string.Empty, "La confirmación de contraseña es requerida.");

        if (viewModel.Password != viewModel.ConfirmarPassword)
            ModelState.AddModelError(string.Empty, "Las contraseñas no coinciden.");

        viewModel.Roles = _rolRepository
            .GetAll()
            .Select(r => new RolModel
            {
                Id = r.Id,
                Nombre = r.Nombre,
            });

        if (!ModelState.IsValid)
            return View(viewModel);

        var entity = new Usuario
        {
            NombreUsuario = viewModel.NombreUsuario,
            Email = viewModel.Email,
            Password = Encriptador.StringToSHA512(viewModel.Password),
            IdRol = viewModel.RolId == 0 ? null : viewModel.RolId,
            Activo = 1,
            EmailConfirmed = 1,
        };

        _usuarioRepository.Insert(entity);

        return RedirectToAction(nameof(Index));
    }

    [Route("administrar/usuarios/editar/{id}")]
    public IActionResult Editar(int id)
    {
        var entity = _usuarioRepository.GetById(id);

        if (entity == null) return RedirectToAction(nameof(Index));

        var viewModel = new EditarViewModel
        {
            Id = entity.Id,
            NombreUsuario = entity.NombreUsuario,
            Email = entity.Email,
            RolId = entity.IdRolNavigation is null ? 0 : entity.IdRolNavigation.Id,
            Roles = _rolRepository
                .GetAll()
                .Select(r => new RolModel
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                })
        };

        return View(viewModel);
    }

    [Route("administrar/usuarios/editar")]
    [HttpPost]
    public IActionResult Editar(EditarViewModel viewModel)
    {
        if (string.IsNullOrWhiteSpace(viewModel.NombreUsuario))
        {
            ModelState.AddModelError(string.Empty, "El nombre es requerido.");
        }
        else
        {
            if (_usuarioRepository.ExisteUsuario(viewModel.NombreUsuario, viewModel.Id))
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con ese nombre.");
        }

        if (string.IsNullOrWhiteSpace(viewModel.Email))
        {
            ModelState.AddModelError(string.Empty, "El email es requerido.");
        }
        else
        {
            if (_usuarioRepository.ExisteEmail(viewModel.Email, viewModel.Id))
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con ese email.");
        }

        viewModel.Roles = _rolRepository
            .GetAll()
            .Select(r => new RolModel
            {
                Id = r.Id,
                Nombre = r.Nombre,
            });

        if (!ModelState.IsValid)
            return View(viewModel);

        var entity = _usuarioRepository.GetById(viewModel.Id);

        if (entity == null) return RedirectToAction(nameof(Index));

        entity.NombreUsuario = viewModel.NombreUsuario;
        entity.Email = viewModel.Email;
        entity.IdRol = viewModel.RolId == 0 ? null : viewModel.RolId;

        _usuarioRepository.Update(entity);

        return RedirectToAction(nameof(Index));
    }

    [Route("administrar/usuarios/eliminar/{id}")]
    public IActionResult Eliminar(int id)
    {
        var entity = _usuarioRepository.GetById(id);

        if (entity == null) return RedirectToAction(nameof(Index));

        var viewModel = new EliminarViewModel
        {
            Id = entity.Id,
            NombreUsuario = entity.NombreUsuario,
        };

        return View(viewModel);
    }

    [Route("administrar/usuarios/eliminar")]
    [HttpPost]
    public IActionResult Eliminar(EliminarViewModel viewModel)
    {
        var entity = _usuarioRepository.GetById(viewModel.Id);

        if (entity == null) return RedirectToAction(nameof(Index));

        _usuarioRepository.Delete(entity);

        return RedirectToAction(nameof(Index));
    }
}
