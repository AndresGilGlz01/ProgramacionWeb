using Microsoft.EntityFrameworkCore;

using U5_Proyecto_Blog.Helpers;
using U5_Proyecto_Blog.Models;
using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Repositories;

public class UsuarioRepository : Repository<Usuario>
{
    public UsuarioRepository(BlogContext context) : base(context) { }

    public override IEnumerable<Usuario> GetAll() => Context.Usuario
        .Include(u => u.IdRolNavigation)
        .OrderBy(u => u.NombreUsuario)
        .AsEnumerable();

    public Usuario? GetById(int id) => Context.Usuario
        .Include(u => u.IdRolNavigation)
        .FirstOrDefault(u => u.Id == id);

    public Usuario? Login(UserLogin userLogin) => Context.Usuario
        .Include(u => u.IdRolNavigation)
        .FirstOrDefault(u => u.NombreUsuario == userLogin.Usuario && u.Password == Encriptador.StringToSHA512(userLogin.Contraseña));

    public bool ExisteUsuario(string nombreUsuario) => Context.Usuario.Any(u => u.NombreUsuario.ToLower() == nombreUsuario.ToLower());

    public bool ExisteUsuario(string nombreUsuario, int id) => Context.Usuario.Any(u => u.NombreUsuario.ToLower() == nombreUsuario.ToLower() && u.Id != id);

    public bool ExisteEmail(string email) => Context.Usuario.Any(u => u.Email.ToLower() == email.ToLower());

    public bool ExisteEmail(string email, int id) => Context.Usuario.Any(u => u.Email.ToLower() == email.ToLower() && u.Id != id);
}
