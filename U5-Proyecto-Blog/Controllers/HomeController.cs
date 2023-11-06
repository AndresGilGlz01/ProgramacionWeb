using Microsoft.AspNetCore.Mvc;

namespace U5_Proyecto_Blog.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
