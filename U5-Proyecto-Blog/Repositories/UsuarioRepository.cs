using Microsoft.EntityFrameworkCore;

using U5_Proyecto_Blog.Helpers;
using U5_Proyecto_Blog.Models;
using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Repositories;

public class UsuarioRepository : Repository<Usuario>
{
    public UsuarioRepository(BlogContext context) : base(context) { }

    public Usuario? Login(UserLogin userLogin) => Context.Usuario
        .Include(u => u.IdRolNavigation)
        .FirstOrDefault(u => u.NombreUsuario == userLogin.Usuario && u.Password == Encriptador.StringToSHA512(userLogin.Contraseña));

    public bool ExisteUsuario(string nombreUsuario) => Context.Usuario.Any(u => u.NombreUsuario.ToLower() == nombreUsuario.ToLower());

    public bool ExisteEmail(string email) => Context.Usuario.Any(u => u.Email.ToLower() == email.ToLower());
}
