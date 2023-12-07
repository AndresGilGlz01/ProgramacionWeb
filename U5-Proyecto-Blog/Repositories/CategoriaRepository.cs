using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Repositories;

public class CategoriaRepository : Repository<Categoria>
{
    public CategoriaRepository(BlogContext context) : base(context) { }

    public bool ExisteCategoria(string nombre) => Context.Categoria.Any(c => c.Nombre.ToLower() == nombre.ToLower());

    public bool ExisteCategoria(string nombre, int id) => Context.Categoria.Any(c => c.Nombre.ToLower() == nombre.ToLower() && c.Id != id);
}
