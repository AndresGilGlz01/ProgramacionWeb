using U3_Ejercicio1.Models.Entities;

namespace U3_Ejercicio1.Repositories;

public class Repository<T> where T : class
{
    public NeatContext Context { get; set; }

    public Repository(NeatContext context)
    {
        Context = context;
    }

    public virtual IEnumerable<T> GetAll() => Context.Set<T>();

    public virtual T? Get(object Id) => Context.Find<T>(Id);

    public virtual void Add(T entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public virtual void Update(T entity)
    {
        Context.Update(entity);
        Context.SaveChanges();
    }

    public virtual void Delete(T entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }
}
