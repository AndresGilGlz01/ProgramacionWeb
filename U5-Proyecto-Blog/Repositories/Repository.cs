using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Repositories;

public class Repository<T> where T : class
{
    public BlogContext Context { get; set; }

    public Repository(BlogContext context)
    {
        Context = context;
    }

    public virtual IEnumerable<T> GetAll() => Context.Set<T>();

    public virtual T? GetById(object id) => Context.Find<T>(id);

    public virtual void Insert(T entity)
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
