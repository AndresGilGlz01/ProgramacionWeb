using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace U5_Proyecto_Blog.Controllers;

[Authorize]
public class CategoriasController : Controller
{
    [Route("categorias")]
    public IActionResult Index()
    {
        return View();
    }
}
