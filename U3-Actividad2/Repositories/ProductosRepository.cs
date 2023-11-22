using Microsoft.EntityFrameworkCore;
using U3_Actividad2.Models.Entities;

namespace U3_Actividad2.Repositories;

public class ProductosRepository : Repository<Productos>
{
    public ProductosRepository(fruteriashopContext context) : base(context) { }

    public override IEnumerable<Productos> GetAll() => _context.Productos
            .Include(c => c.IdCategoriaNavigation)
            .OrderBy(c => c.Nombre);

    public IEnumerable<Productos> GetProductosByCategoria(string categoria) => _context.Productos
        .Where(p => p.IdCategoriaNavigation!.Nombre == categoria);

    public Productos? GetProductosByNombre(string nombre) => _context.Productos
        .Include(c => c.IdCategoriaNavigation)
        .FirstOrDefault(p => p.Nombre.Contains(nombre));

    public IEnumerable<Productos> GetProductosByCategoria(int id) => _context.Productos
        .Include(c => c.IdCategoriaNavigation)
        .Where(c => c.IdCategoria == id)
        .OrderBy(c => c.Nombre);
}
