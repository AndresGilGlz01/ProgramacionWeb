using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using U5_Proyecto_Blog.Repositories;

namespace U5_Proyecto_Blog.Controllers;

[Authorize]
public class UsuarioController : Controller
{
    UsuarioRepository _usuarioRepository;

    public UsuarioController(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [Route("usuarios/")]
    public IActionResult GetAll()
    {
        var usuarios = _usuarioRepository.GetAll();

        return Ok(usuarios);
    }

    [Route("usuario/{Id}")]
    public IActionResult GetById(int Id)
    {
        var usuario = _usuarioRepository.GetById(Id);

        if (usuario is null) return NotFound();

        return Ok(usuario);
    }
}
