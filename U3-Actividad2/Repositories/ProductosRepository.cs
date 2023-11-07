using U3_Actividad2.Models.Entities;

namespace U3_Actividad2.Repositories;

public class ProductosRepository : Repository<Productos>
{
    public ProductosRepository(fruteriashopContext context) : base(context) { }

    public IEnumerable<Productos> GetProductosByCategoria(string categoria) =>_context.Productos.Where(p => p.IdCategoriaNavigation!.Nombre == categoria);
}
