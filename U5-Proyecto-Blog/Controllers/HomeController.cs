using Microsoft.AspNetCore.Mvc;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Controllers;

public class HomeController : Controller
{
    UsuarioRepository _repository;

    public HomeController(UsuarioRepository repository)
    {
        _repository = repository;
    }

    [Route("/")]
    public IActionResult Index()
    {
        // Verificar si se encuentra logeado
        return View();
    }

    [Route("/Login")]
    public IActionResult Login()
    {
        // Mostrar formulario de login
        return View();
    }

    [Route("/Logout")]
    public IActionResult Logout()
    {
        // Cerrar sesión, y volver al Login
        return RedirectToAction("Login");
    }
}
