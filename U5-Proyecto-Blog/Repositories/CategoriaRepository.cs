using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Repositories;

public class CategoriaRepository : Repository<Categoria>
{
    public CategoriaRepository(BlogsContext context) : base(context) { }
}
