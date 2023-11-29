using Microsoft.AspNetCore.Mvc;

using U3_Ejercicio1.Areas.Admin.Models.ViewModels;
using U3_Ejercicio1.Models.Entities;
using U3_Ejercicio1.Repositories;

namespace U3_Ejercicio1.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController(
    ClasificacionRepository clasificacionRepository,
    MenuRepository menuRepository) : Controller
{
    ClasificacionRepository _clasificacionRepository = clasificacionRepository;
    MenuRepository _menuRepository = menuRepository;

    public IActionResult Index() => View();

    [Route("admin/menu")]
    public IActionResult Menu()
    {
        var viewModel = new MenuViewModel()
        {
            Clasificaciones = _clasificacionRepository
                .GetAll()
                .Select(c => new ClasificacionModel()
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Menus = c.Menu.Select(m => new MenuModel()
                    {
                        Id = m.Id,
                        Nombre = m.Nombre,
                        Descripcion = m.Descripción,
                        PrecioOriginal = (decimal)m.Precio,
                        PrecioNuevo = (decimal?)m.PrecioPromocion
                    })
                })
        };

        return View(viewModel);
    }

    [Route("admin/menu/agregar")]
    public IActionResult AgregarMenu()
    {
        var viewModel = new AgregarMenuViewModel()
        {
            Clasificaciones = _clasificacionRepository
                .GetAll()
                .Select(c => new ClasificacionModel()
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
        };

        return View(viewModel);
    }

    [HttpPost]
    [Route("admin/menu/agregar")]
    public IActionResult AgregarMenu(AgregarMenuViewModel viewModel)
    {
        if (string.IsNullOrEmpty(viewModel.Nombre)) ModelState.AddModelError(string.Empty, "El nombre es requerido");

        if (string.IsNullOrEmpty(viewModel.Descripcion)) ModelState.AddModelError(string.Empty, "La descripción es requerida");

        if (viewModel.Precio == 0) ModelState.AddModelError(string.Empty, "El precio no puede ser $0");

        if (viewModel?.Archivo?.Length > 500 * 1024) ModelState.AddModelError(string.Empty, "La imagen debe pesar menos de 500kb");

        if (!ModelState.IsValid) return View(viewModel);

        var entity = new Menu()
        {
            Nombre = viewModel.Nombre,
            Descripción = viewModel.Descripcion,
            Precio = (double)viewModel.Precio,
            PrecioPromocion = null,
            IdClasificacion = viewModel.IdClasificacion
        };

        _menuRepository.Add(entity);

        if (viewModel.Archivo is null)
        {
            System.IO.File.Copy("wwwroot/images/burger.png", $"wwwroot/hamburguesas/{entity.Id}.png");
        }
        else
        {
            var fs = new FileStream($"wwwroot/hamburguesas/{entity.Id}.png", FileMode.Create);
            viewModel.Archivo.CopyTo(fs);
            fs.Close();
        }

        return RedirectToAction(nameof(Menu));
    }

    [Route("admin/menu/editar/{Id}")]
    public IActionResult EditarMenu(string Id)
    {
        Id = Id.Replace("-", " ");

        var entity = _menuRepository.GetByName(Id);

        if (entity is null) return RedirectToAction(nameof(Menu));

        var viewModel = new EditarMenuViewModel
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Descripcion = entity.Descripción,
            Precio = (decimal)entity.Precio,
            IdClasificacion = entity.IdClasificacion,
            Clasificaciones = _clasificacionRepository
                .GetAll()
                .Select(c => new ClasificacionModel()
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
        };

        return View(viewModel);
    }

    [Route("admin/menu/editar")]
    public IActionResult EditarMenu(EditarMenuViewModel viewModel)
    {
        if (string.IsNullOrEmpty(viewModel.Nombre)) ModelState.AddModelError(string.Empty, "El nombre es requerido");

        if (string.IsNullOrEmpty(viewModel.Descripcion)) ModelState.AddModelError(string.Empty, "La descripción es requerida");

        if (viewModel.Precio == 0) ModelState.AddModelError(string.Empty, "El precio no puede ser $0");

        if (viewModel?.Archivo?.Length > 500 * 1024) ModelState.AddModelError(string.Empty, "La imagen debe pesar menos de 500kb");

        if (!ModelState.IsValid) return View(viewModel);

        var entity = _menuRepository.Get(viewModel.Id);

        return RedirectToAction(nameof(Menu));
    }
}
