using Microsoft.EntityFrameworkCore;
using U3_Ejercicio1.Models.Entities;

namespace U3_Ejercicio1.Repositories;

public class MenuRepository(NeatContext context) : Repository<Menu>(context)
{
    public override IEnumerable<Menu> GetAll() => Context.Menu
        .Include(x => x.IdClasificacionNavigation)
        .OrderBy(x => x.IdClasificacionNavigation.Nombre)
        .ThenBy(x => x.Nombre);

    public Menu? GetByName(string name) => Context.Menu
        .Include(x => x.IdClasificacionNavigation)
        .FirstOrDefault(x => x.Nombre == name);
}
