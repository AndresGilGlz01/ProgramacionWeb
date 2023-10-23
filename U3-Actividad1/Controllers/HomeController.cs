using Microsoft.AspNetCore.Mvc;
using U3_Actividad1.Models.ViewModels;
using U3_Actividad1.Repositories;

namespace VIllancicos.Controllers;

public class HomeController : Controller
{
    private VillancicoRepository _repository;

    public HomeController(VillancicoRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var data = _repository.Get()
            .Select(e => e.Nombre);
        
        return View(data);
    }

    [Route("Villancico/{Id}")]
    public IActionResult Villancico(string? Id)
    {
        Id = Id?.Replace('-', ' ');

        if (Id is null) return RedirectToAction("Index");

        var data = _repository.Get(Id);

        if (data is null) return RedirectToAction("Index");

        var viewModel = new VillancicoViewModel
        {
            Nombre = data.Nombre,
            Compositor = data.Compositor,
            Año = data.Anyo ?? 0,
            Letra = data.Letra,
            Url = data.VideoUrl
        };

        return View(viewModel);
    }
}
