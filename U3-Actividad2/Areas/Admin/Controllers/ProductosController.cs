﻿using Microsoft.AspNetCore.Mvc;

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
            System.IO.File.Copy("wwwroot/img_frutas/0.jpg", $"wwwroot/img_frutas/{viewModel.Producto.Id}.jpg");   
        }
        else
        {
            var fs = new FileStream($"wwwroot/img_frutas/{viewModel.Producto.Id}.jpg", FileMode.Create);
            viewModel.Archivo.CopyTo(fs);
            fs.Close();
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Editar(int Id)
    {
        var producto = productosRepository.GetById(Id);

        if (producto is null) return RedirectToAction(nameof(Index));

        var viewModel = new AdminGuardarProductosViewModel()
        {
            Categorias = categoriasRepository.GetAll()
                .OrderBy(c => c.Nombre)
                .Select(c => new CategoriaModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre ?? "Sin nombre"
                }),
            Producto = producto
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(AdminGuardarProductosViewModel viewModel)
    {
        if (viewModel.Archivo is not null)
        {
            if (viewModel.Archivo.ContentType != "image/jpeg")
            {
                ModelState.AddModelError(string.Empty, "Solo se aceptan imagenes jpg");
            }

            if (viewModel.Archivo.Length > 500 * 1024)
            {
                ModelState.AddModelError(string.Empty, "La imagen debe pesar menos de 500kb");
            }
        }

        if (!ModelState.IsValid) return View(viewModel);

        var producto = productosRepository.GetById(viewModel.Producto.Id);

        if (producto is null) return RedirectToAction(nameof(Index));

        producto.Nombre = viewModel.Producto.Nombre;
        producto.Precio = viewModel.Producto.Precio;
        producto.IdCategoria = viewModel.Producto.IdCategoria;
        producto.UnidadMedida = viewModel.Producto.UnidadMedida;
        producto.Descripcion = viewModel.Producto.Descripcion;

        productosRepository.Update(producto);

        if (viewModel.Archivo is null)
        {
            System.IO.File.Copy("wwwroot/img_frutas/0.jpg", $"wwwroot/img_frutas/{viewModel.Producto.Id}.jpg");
        }
        else
        {
            var fs = new FileStream($"wwwroot/img_frutas/{viewModel.Producto.Id}.jpg", FileMode.Create);
            viewModel.Archivo.CopyTo(fs);
            fs.Close();
        }

        return RedirectToAction(nameof(Index));
    }
}
