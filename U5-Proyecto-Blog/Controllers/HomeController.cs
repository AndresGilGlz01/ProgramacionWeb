using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Text.RegularExpressions;

using U5_Proyecto_Blog.Helpers;
using U5_Proyecto_Blog.Models;
using U5_Proyecto_Blog.Models.Entities;
using U5_Proyecto_Blog.Models.ViewModels.Home;
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

    [Route("login")]
    public IActionResult Login() => View();

    [HttpPost]
    [Route("login")]
    public IActionResult Login(UserLogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.Contraseña))
                ModelState.AddModelError(string.Empty, "La contraseña es requerida.");

            if (string.IsNullOrEmpty(userLogin.Usuario))
                ModelState.AddModelError(string.Empty, "El nombre de usuario es requerdio.");

            if (!ModelState.IsValid)
                return View(userLogin);

            var usuarioActual = _usuarioRepository.Login(userLogin);

            if (usuarioActual is null)
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectas.");

            if (!ModelState.IsValid)
                return View(userLogin);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuarioActual!.Id.ToString()),
                new(ClaimTypes.Name, usuarioActual!.NombreUsuario),
                new(ClaimTypes.Role, usuarioActual.IdRolNavigation is not null ? usuarioActual.IdRolNavigation.Nombre : "Normal")
            };

            var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Identity));

            return RedirectToAction(nameof(Index));
        }

    [Route("signup")]
    public IActionResult SignUp() => View();

    [HttpPost]
    [Route("signup")]
    public IActionResult SignUp(SignupViewModel viewModel)
    {
        if (string.IsNullOrEmpty(viewModel.Contraseña))
            ModelState.AddModelError(string.Empty, "La contraseña es requerida");

        if (string.IsNullOrEmpty(viewModel.ConfirmarContraseña))
            ModelState.AddModelError(string.Empty, "La confirmación de contraseña es requerida");

        if (viewModel.Contraseña != viewModel.ConfirmarContraseña)
            ModelState.AddModelError(string.Empty, "Las contraseñas no coinciden");

        if (string.IsNullOrEmpty(viewModel.NombreUsuario))
            ModelState.AddModelError(string.Empty, "El nombre de usuario es requerido");
        
        if (string.IsNullOrEmpty(viewModel.Email))
            ModelState.AddModelError(string.Empty, "El email es requerido");

        if (!IsValidEmail(viewModel.Email ?? string.Empty))
            ModelState.AddModelError(string.Empty, "El email no es válido");

        if (!ModelState.IsValid)
            return View(viewModel);

        var entity = new Usuario
        {
            Email = viewModel.Email,
            EmailConfirmed = 1,
            Activo = 1,
            Password = Encriptador.StringToSHA512(viewModel.Contraseña),
            NombreUsuario = viewModel.NombreUsuario,
            IdRol = null
        };

        _usuarioRepository.Insert(entity);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, entity!.Id.ToString()),
            new(ClaimTypes.Name, entity!.NombreUsuario),
            new(ClaimTypes.Role, entity.IdRolNavigation is not null ? entity.IdRolNavigation.Nombre : "Normal"),
        };

        var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Identity));

        return RedirectToAction(nameof(Index));
    }

    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction(nameof(Index));
    }

    [Route("confirmar/{id}")]
    public IActionResult ConfirmarEmail(int id)
    {
        // Confirmar el email del usuario
        return RedirectToAction("Login");
    }

    private bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
}
