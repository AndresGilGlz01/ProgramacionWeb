using Microsoft.EntityFrameworkCore;

using U3_Ejercicio1.Models.Entities;

namespace U3_Ejercicio1.Repositories;

public class ClasificacionRepository : Repository<Clasificacion> 
{
    public ClasificacionRepository(NeatContext context) : base(context) { }

    public override IEnumerable<Clasificacion> GetAll() => Context.Clasificacion
        .Include(c => c.Menu)
        .OrderBy(c => c.Nombre);
}
