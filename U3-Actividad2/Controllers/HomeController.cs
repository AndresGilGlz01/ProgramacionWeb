using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using U3_Actividad2.Helpers;
using U3_Actividad2.Models.Entities;
using U3_Actividad2.Models.ViewModels;
using U3_Actividad2.Repositories;

namespace FruitStore.Controllers;

public class HomeController : Controller
{
    ProductosRepository ProductosRepository;
    Repository<Usuarios> UsuariosRepository;

    public HomeController(ProductosRepository productosRepository,
        Repository<Usuarios> usuariosRepository)
    {
        UsuariosRepository = usuariosRepository;
        ProductosRepository = productosRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(LoginViewModel viewModel)
    {
        if (string.IsNullOrEmpty(viewModel.Email)) ModelState.AddModelError(string.Empty, "El campo Email es obligatorio");
        if (string.IsNullOrEmpty(viewModel.Contraseña)) ModelState.AddModelError(string.Empty, "El campo Contraseña es obligatorio");

        if (!ModelState.IsValid) return View(viewModel);

        var user = UsuariosRepository
            .GetAll()
            .FirstOrDefault(u => u.Correo == viewModel.Email && Encriptador.StringToSHA512(viewModel.Contraseña) == u.Contrasena);

        if (user is null) ModelState.AddModelError(string.Empty, "Datos incorrectos");

        if (!ModelState.IsValid) return View(viewModel);

        var claims = new List<Claim>
        {
            new("Id", user!.Id.ToString()),
            new(ClaimTypes.Name, user.Nombre),
            new(ClaimTypes.Role, user.Rol == 1 ? "Administrador" : "Supervisor"),
        };

        var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Identity));

        return RedirectToAction("Index", "Home", new { area = "Admin"});
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }

    public IActionResult Productos(string Id)
    {
        Id = Id.Replace("-", "");

        var viewModel = new ProductosViewModel()
        {
            Categoria = Id,
            Productos = ProductosRepository.GetProductosByCategoria(Id)
                .Select(p => new ProductoModel()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio.Value
                })
        };

        return View(viewModel);
    }

    public IActionResult Ver(string Id)
    {
        Id = Id.Replace("-", " ");

        var producto = ProductosRepository.GetProductosByCategoria(Id);

        if (producto == null) return RedirectToAction("Index");

        //var viewModel = new VerProductoViewModel()
        //{
        //    Id = producto.id,
        //    Nombre = producto.Nombre ?? string.Empty,
        //    Categoria = producto.IdCategoriaNavigation!.Nombre ?? string.Empty,
        //    Precio = producto.Precio != null ? producto.Precio.Value : 0m,
        //    UnidadMedida = producto.UnidadMedida ?? string.Empty,
        //    Descripcion = producto.Descripcion ?? string.Empty
        //};

        return View();
    }
}
