using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Repositories;

public class UsuarioRepository : Repository<Usuario>
{
    public UsuarioRepository(BlogsContext context) : base(context) { }
}
