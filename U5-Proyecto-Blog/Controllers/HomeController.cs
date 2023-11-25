using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using U5_Proyecto_Blog.Models;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Controllers;

public class HomeController : Controller
{
    UsuarioRepository _usuarioRepository;

    public HomeController(UsuarioRepository repository)
    {
        _usuarioRepository = repository;
    }

    [Route("/")]
    public IActionResult Index()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToRoute(new { action = "Index", controller = "Post" });
        }
        else
        {
            return RedirectToAction(nameof(Login));
        }

    }

    [Route("Login")]
    public IActionResult Login()
    {
        // Mostrar formulario de login
        return View();
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        try
        {
            var usuarioActual = _usuarioRepository.Login(userLogin);

            if (usuarioActual is null)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectas.");
                return View(userLogin);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioActual.NombreUsuario.ToString()),
                //new Claim(ClaimTypes.Role, usuarioActual.TipoUsuario.ToString()),
                //new Claim("Nombre", usuarioActual.Nombre),
                //new Claim("TipoUsuario", usuarioActual.TipoUsuario.ToString()),
                //new Claim("IdDepartamento", usuarioActual.IdDepartamento.ToString()),
                //new Claim("IdDepartamentos", usuarioActual.IdDepartamentos),
                //new Claim("_sym57_", $"{logindata.username}:{logindata.password}")
            };

            ClaimsPrincipal principal = new(new ClaimsIdentity(claims, "login"));

            await HttpContext.SignInAsync(principal, new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(30),
                IsPersistent = true
            });

            return RedirectToRoute(new { action = "Index", controller = "Post" });
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Error");
        }
    }

    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index");
    }

    [Route("ConfirmarEmail/{Id}")]
    public IActionResult ConfirmarEmail(int Id)
    {
        // Confirmar el email del usuario
        return RedirectToAction("Login");
    }
}
