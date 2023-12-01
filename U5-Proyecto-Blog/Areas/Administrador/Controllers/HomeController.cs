using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using U5_Proyecto_Blog.Areas.Administrador.Models;
using U5_Proyecto_Blog.Areas.Administrador.Models.ViewModels;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Areas.Administrador.Controllers;

[Authorize(Roles = "Administrador")]
[Area("Administrador")]
public class HomeController : Controller
{
    UsuarioRepository _usuarioRepository;

    public HomeController(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index() => View();

    [Route("administrar/usuarios")]
    public IActionResult Usuarios()
    {
        var viewModel = new UsuariosViewModel
        {
            Usuarios = _usuarioRepository
                .GetAll()
                .Select(u => new UsuarioModel
                {
                    Id = u.Id,
                    Nombre = u.NombreUsuario,
                    Email = u.Email,
                    Rol = u.IdRolNavigation is not null ? u.IdRolNavigation.Nombre : "Normal"
                })
        };

        return View(viewModel);
    }
}
