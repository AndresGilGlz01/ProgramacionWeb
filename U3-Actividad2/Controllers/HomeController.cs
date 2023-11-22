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
