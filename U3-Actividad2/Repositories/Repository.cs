using U3_Actividad2.Models.Entities;

namespace U3_Actividad2.Repositories;

public class Repository<T> where T : class
{
    public fruteriashopContext _context;

    public Repository(fruteriashopContext context)
    {
        _context = context;
    }

    public virtual IEnumerable<T> GetAll() => _context.Set<T>();

    public virtual T? GetById(object id) => _context.Find<T>(id);

    public virtual void Insert(T entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public virtual void Update(T entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }

    public virtual void Delete(T entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }
}
