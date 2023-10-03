using Microsoft.AspNetCore.Mvc;

namespace U2_Actividad3.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
