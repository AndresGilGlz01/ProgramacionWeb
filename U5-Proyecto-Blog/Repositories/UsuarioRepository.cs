using U5_Proyecto_Blog.Models;
using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Repositories;

public class UsuarioRepository : Repository<Usuario>
{
    public UsuarioRepository(BlogsContext context) : base(context) { }

    public Usuario? Login(UserLogin userLogin) => Context.Usuario.FirstOrDefault(u => 
            u.NombreUsuario == userLogin.Usuario && 
            u.Password == userLogin.Contraseña);
}
