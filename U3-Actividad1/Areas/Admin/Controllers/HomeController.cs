using Microsoft.AspNetCore.Mvc;
using U3_Actividad1.Areas.Admin.Models;
using U3_Actividad1.Repositories;

namespace U3_Actividad1;

[Area("Admin")]
public class HomeController : Controller
{
    private VillancicoRepository _repository;

    public HomeController(VillancicoRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var viewModels = new AdminIndexViewModel()
        {
            Villancicos = _repository.Get()
                .Select(v => new VillancicoModel()
                {
                    Id = v.Id,
                    Nombre = v.Nombre
                })
        };

        return View(viewModels);
    }
}
