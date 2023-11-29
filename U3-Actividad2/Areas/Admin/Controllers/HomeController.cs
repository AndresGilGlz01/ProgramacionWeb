using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace U3_Actividad2.Areas.Admin.Controllers;

[Authorize(Roles = "Administrador, Supervisor")]
[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
