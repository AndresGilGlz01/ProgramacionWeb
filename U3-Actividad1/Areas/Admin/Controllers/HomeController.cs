using Microsoft.AspNetCore.Mvc;

namespace U3_Actividad1;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
