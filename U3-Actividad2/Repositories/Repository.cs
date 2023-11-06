using U3_Actividad2.Models.Entities;

namespace U3_Actividad2.Repositories;

public class Repository<T> where T : class
{
    private fruteriashopContext _context;

    public Repository(fruteriashopContext context)
    {
        _context = context;
    }

    public virtual IEnumerable<T> GetAll() => _context.Set<T>();

    public virtual T? GetById(object id) => _context.Find<T>(id);

    
}
