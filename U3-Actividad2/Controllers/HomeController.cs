using Microsoft.AspNetCore.Mvc;
using U3_Actividad2.Models.ViewModels;
using U3_Actividad2.Repositories;

namespace FruitStore.Controllers;
public class HomeController : Controller
{
   ProductosRepository ProductosRepository;

    public HomeController(ProductosRepository productosRepository)
    {
        ProductosRepository = productosRepository;
    }

    public IActionResult Index()
    {
        return View();
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

    public IActionResult Ver()
    {
        return View();
    }

}
