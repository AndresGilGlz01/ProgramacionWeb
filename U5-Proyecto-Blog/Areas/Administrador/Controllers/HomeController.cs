using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Areas.Administrador.Controllers;

[Authorize(Roles = "Administrador")]
[Area("Administrador")]
public class HomeController : Controller
{
    UsuarioRepository _usuarioRepository;
    CategoriaRepository _categoriaRepository;

    public HomeController(UsuarioRepository usuarioRepository,
        CategoriaRepository categoriaRepository)
    {
        _usuarioRepository = usuarioRepository;
        _categoriaRepository = categoriaRepository;
    }
}
