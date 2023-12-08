using Microsoft.EntityFrameworkCore;

using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Repositories;

public class CategoriaRepository : Repository<Categoria>
{
    public CategoriaRepository(BlogContext context) : base(context) { }

    public override IEnumerable<Categoria> GetAll() => Context.Categoria
        .Include(c => c.Postcategoria)
        .ThenInclude(c => c.IdPostNavigation)
        .OrderBy(c => c.Nombre);

    public bool ExisteCategoria(string nombre) => Context.Categoria.Any(c => c.Nombre.ToLower() == nombre.ToLower());

    public bool ExisteCategoria(string nombre, int id) => Context.Categoria.Any(c => c.Nombre.ToLower() == nombre.ToLower() && c.Id != id);

    public Categoria? GetByName(string id) => Context.Categoria
        .Include(c => c.Postcategoria)
        .ThenInclude(c => c.IdPostNavigation)
        .FirstOrDefault(c => c.Nombre.ToLower() == id.ToLower());
}
