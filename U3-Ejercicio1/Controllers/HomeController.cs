using Microsoft.AspNetCore.Mvc;
using U3_Ejercicio1.Models.ViewModels;
using U3_Ejercicio1.Repositories;

namespace U3_Ejercicio1.Controllers;

public class HomeController(MenuRepository menuRepository) : Controller
{
    MenuRepository _menuRepository = menuRepository;

    public IActionResult Index() => View();

    [Route("menu")]
    public IActionResult Menu()
    {
        var viewModel = new MenuViewModel()
        {
            Clasificaciones = _menuRepository.GetAll()
                    .GroupBy(m => m.IdClasificacionNavigation)
                    .Select(c => new ClasificacionModel
                    {
                        Nombre = c.Key.Nombre,
                        Hamburguesas = c.Select(m => new HamburguesaModel
                        {
                            Id = m.Id,
                            Descripcion = m.Descripción,
                            Nombre = m.Nombre,
                            Precio = m.PrecioPromocion is null ? (decimal)m.Precio : (decimal)m.PrecioPromocion
                        }).ToList()
                    }).ToList()
        };

        viewModel.Clasificaciones.First()
                .Hamburguesas.First()
                .Seleccionado = true;

        viewModel.Hamburguesa = viewModel.Clasificaciones
            .SelectMany(c => c.Hamburguesas)
            .First(h => h.Seleccionado);

        return View(viewModel);
    }

    [Route("menu/{Id}")]
    public IActionResult Menu(string Id)
    {
        Id = Id.Replace('-', ' ');

        var menu = _menuRepository.GetByName(Id);

        if (menu is null) return RedirectToAction(nameof(Index));

        var viewModel = new MenuViewModel()
        {
            Clasificaciones = _menuRepository.GetAll()
                .GroupBy(m => m.IdClasificacionNavigation)
                .Select(c => new ClasificacionModel
                {
                    Nombre = c.Key.Nombre,
                    Hamburguesas = c.Select(m => new HamburguesaModel
                    {
                        Id = m.Id,
                        Descripcion = m.Descripción,
                        Nombre = m.Nombre,
                        Precio = m.PrecioPromocion is null ? (decimal)m.Precio : (decimal)m.PrecioPromocion,
                        Seleccionado = m.Id == menu.Id
                    })
                })
        };

        viewModel.Hamburguesa = viewModel.Clasificaciones
            .SelectMany(c => c.Hamburguesas)
            .First(h => h.Seleccionado);

        return View(viewModel);
    }

    [Route("promociones")]
    public IActionResult Promociones()
    {
        var data = _menuRepository.GetAll().Where(p => p.PrecioPromocion is not null).ToList();

        if (data is null || !data.Any()) return RedirectToAction(nameof(Index));

        var promocion = data.First();

        var viewModel = new PromocionesViewModel()
        {
            Id = promocion.Id,
            Nombre = promocion.Nombre,
            Descripcion = promocion.Descripción,
            PrecioOriginal = (decimal)promocion.Precio,
            PrecioNuevo = (decimal)promocion.PrecioPromocion!,
            AnteriorPromocion = data.ElementAtOrDefault(data.IndexOf(promocion) - 1)?.Nombre ?? promocion.Nombre,
            SiguientePromocion = data.ElementAtOrDefault(data.IndexOf(promocion) + 1)?.Nombre ?? promocion.Nombre
        };

        return View(viewModel);
    }

    [Route("promociones/{Id}")]
    public IActionResult Promociones(string Id)
    {
        Id = Id.Replace('-', ' ');

        var data = _menuRepository.GetAll().Where(p => p.PrecioPromocion is not null).ToList();

        if (data is null || !data.Any()) return RedirectToAction(nameof(Index));

        var promocion = data.FirstOrDefault(p => p.Nombre.ToLower() == Id);

        if (promocion is null) return RedirectToAction(nameof(Index));

        var viewModel = new PromocionesViewModel()
        {
            Id = promocion.Id,
            Nombre = promocion.Nombre,
            Descripcion = promocion.Descripción,
            PrecioOriginal = (decimal)promocion.Precio,
            PrecioNuevo = (decimal)promocion.PrecioPromocion!,
            AnteriorPromocion = data.ElementAtOrDefault(data.IndexOf(promocion) - 1)?.Nombre ?? promocion.Nombre,
            SiguientePromocion = data.ElementAtOrDefault(data.IndexOf(promocion) + 1)?.Nombre ?? promocion.Nombre
        };

        return View(viewModel);
    }
}
