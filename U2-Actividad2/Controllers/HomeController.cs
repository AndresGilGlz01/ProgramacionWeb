using Microsoft.AspNetCore.Mvc;

namespace U2_Actividad2.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
