using Microsoft.AspNetCore.Mvc;

using U3_Actividad2.Areas.Admin.Models.ViewModels;
using U3_Actividad2.Models.Entities;
using U3_Actividad2.Repositories;

namespace U3_Actividad2.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductosController : Controller
{
    ProductosRepository productosRepository;
    Repository<Categorias> categoriasRepository;

    public ProductosController(
        ProductosRepository productosRepository,
        Repository<Categorias> categoriasRepository)
    {
        this.productosRepository = productosRepository;
        this.categoriasRepository = categoriasRepository;
    }

    [HttpGet, HttpPost]
    public IActionResult Index(AdminProductosViewModel viewModel)
    {
        viewModel.Categorias = categoriasRepository.GetAll()
            .OrderBy(c => c.Nombre)
            .Select(c => new CategoriaModel
            {
                Id = c.Id,
                Nombre = c.Nombre ?? "Sin nombre"
            });


        if (viewModel.SelectedCategoriaId == 0)
        {
            viewModel.Productos = productosRepository.GetAll()
                .Select(p => new ProductoModel
                {
                    Id = p.Id,
                    Nombre = p.Nombre ?? "Sin nombre",
                    Categoria = p.IdCategoriaNavigation?.Nombre ?? "Sin categoria"
                });

        }
        else
        {
            viewModel.Productos = productosRepository.GetProductosByCategoria(viewModel.SelectedCategoriaId)
                .Select(p => new ProductoModel
                {
                    Id = p.Id,
                    Nombre = p.Nombre ?? "Sin nombre",
                    Categoria = p.IdCategoriaNavigation?.Nombre ?? "Sin categoria"
                });
        }
        
        return View(viewModel);
    }

    public IActionResult Agregar()
    {
        var viewModel = new AdminGuardarProductosViewModel()
        {
            Categorias = categoriasRepository.GetAll()
                .OrderBy(c => c.Nombre)
                .Select(c => new CategoriaModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre ?? "Sin nombre"
                }),
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Agregar(AdminGuardarProductosViewModel viewModel)
    {
        if (viewModel.Archivo is not null)
        {
            if(viewModel.Archivo.ContentType != "image/jpeg")
            {
                ModelState.AddModelError(string.Empty, "Solo se aceptan imagenes jpg");
            }

            if(viewModel.Archivo.Length > 500 * 1024)
            {
                ModelState.AddModelError(string.Empty, "La imagen debe pesar menos de 500kb");
            }
        }

        if (!ModelState.IsValid) return View(viewModel);

        productosRepository.Insert(viewModel.Producto);

        if (viewModel.Archivo is null)
        {
            
        }

        return RedirectToAction(nameof(Index));
    }
}
